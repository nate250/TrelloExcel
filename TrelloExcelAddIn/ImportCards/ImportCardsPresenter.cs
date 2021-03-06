﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public class ImportCardsPresenter
    {
        private readonly IImportCardsView view;
        private readonly IMessageBus messageBus;
        private readonly TrelloHelper trelloHelper;
        private readonly ITrello trello;
        private readonly TaskScheduler taskScheduler;

        public ImportCardsPresenter(IImportCardsView view, IMessageBus messageBus, ITrello trello, TaskScheduler taskScheduler)
        {
            this.view = view;
            this.messageBus = messageBus;
            this.trello = trello;
            this.taskScheduler = taskScheduler;
            trelloHelper = new TrelloHelper(trello);

            SetupMessageEventHandlers();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            view.BoardWasSelected += BoardWasSelected;
            view.ListItemCheckedChanged += ListItemCheckedChanged;
            view.LabelItemCheckedChanged += LabelItemCheckedChanged;
            view.ImportCardsButtonWasClicked += ImportCardsButtonWasClicked;
            view.UpdateCardsButtonWasClicked += UpdateCardsButtonWasClicked;
            view.RefreshButtonWasClicked += RefreshButtonWasClicked;
        }

        private void RefreshButtonWasClicked(object sender, EventArgs eventArgs)
        {
            FetchAndDisplayBoards();
        }

        private void UpdateCardsButtonWasClicked(object sender, EventArgs eventArgs)
        {
            CardImportWork(sender, eventArgs, true);
        }

        private void ImportCardsButtonWasClicked(object sender, EventArgs eventArgs)
        {
            CardImportWork(sender, eventArgs, false);
        }

        private void CardImportWork(object sender, EventArgs eventArgs, bool update)
        {
            view.ShowStatusMessage((update ? "Updating" : "Importing") + " cards...");
            view.EnableImport = false;
            view.EnableUpdate = false;
            view.EnableSelectionOfBoards = false;
            view.EnableSelectionOfLists = false;
            view.EnableSelectionOfLabels = false;

            trello.Async.Cards.ForBoard(view.SelectedBoard, BoardCardFilter.Open)
                .ContinueWith(t =>
                {
                    if (t.Exception != null)
                    {
                        HandleException(t.Exception);
                        return;
                    }

                    List<Card.Label> checkedLabels = new List<Card.Label>();

                    foreach (KeyValuePair<Color, string> kvp in view.CheckedLabels)
                    {
                        Card.Label label = new Card.Label();
                        label.Color = kvp.Key;
                        label.Name = kvp.Value;
                        checkedLabels.Add(label);
                    }

                    // We should only import cards in lists the user selected
                    var cardsToImport = GetCardsForSelectedLists(t.Result, view.FieldsToInclude, checkedLabels);

                    // Create a range based on the current selection. Rows = number of cards, Columns = 4 (to fit name, desc, list and due date)
                    var numberOfRows = cardsToImport.GetUpperBound(0) + 1;
                    var numberOfColumns = view.FieldsToInclude.Count();
                    var rangeThatFitsAllCards = ResizeToFitAllCards(Globals.ThisAddIn.Application.ActiveWindow.RangeSelection, numberOfRows, numberOfColumns);

                    // Store the address of this range for later user
                    var addressToFirstCell = rangeThatFitsAllCards.AddressLocal;

                    if (!update)
                    {
                        // Kind of copy/paste this range
                        InsertRange(rangeThatFitsAllCards);

                        // The rangeThatFitsAllCards was change after the InsertRange call, so create a new range based on addressToFirstCell
                        rangeThatFitsAllCards = ResizeToFitAllCards(Globals.ThisAddIn.Application.ActiveSheet.Range(addressToFirstCell), numberOfRows, numberOfColumns);
                    }

                    // Set the values of the cells to the cards name, desc and due date
                    UpdateRangeWithCardsToImport(rangeThatFitsAllCards, cardsToImport);

                    view.ShowStatusMessage(string.Format("{0} card(s) imported!", numberOfRows - 1));
                    view.EnableImport = true;
                    view.EnableUpdate = true;
                    view.EnableSelectionOfBoards = true;
                    view.EnableSelectionOfLists = true;
                    view.EnableSelectionOfLabels = true;
                }, taskScheduler);
        }

        private void UpdateRangeWithCardsToImport(Range rangeThatFitsAllCards, string[,] cardsToImport)
        {
            rangeThatFitsAllCards.Value2 = cardsToImport;
            rangeThatFitsAllCards.Select();
            rangeThatFitsAllCards.Columns.AutoFit();
        }

        private static void InsertRange(Range rangeThatFitsAllCards)
        {
            rangeThatFitsAllCards.Copy();
            Globals.ThisAddIn.Application.CutCopyMode = XlCutCopyMode.xlCopy;
            rangeThatFitsAllCards.Insert(XlInsertShiftDirection.xlShiftDown);
        }

        private static Range ResizeToFitAllCards(Range rangeSelection, int numberOfRows, int numberOfColumns)
        {
            return rangeSelection.Resize[numberOfRows, numberOfColumns];
        }

        private bool IntersectLabelLists(List<Card.Label> a, List<Card.Label> b)
        {
            var intersection = from la in a
                               join lb in b on la.Color equals lb.Color
                               select true;

            if (intersection.Count<bool>() == 0)
                return false;

            foreach (bool found in intersection)
                if (!found) return false;

            return true;
        }

        private string[,] GetCardsForSelectedLists(IEnumerable<Card> allCards, IEnumerable<string> fieldsToInclude, List<Card.Label> labelFilter)
        {
            var cards = allCards.Where(c => view.CheckedLists.Select(cl => cl.Id).Contains(c.IdList)).ToList();
            IEnumerable<string[]> cardsToImportWithListName;

            if (labelFilter.Count < Enum.GetNames(typeof(TrelloNet.Color)).Length)
            {
                cardsToImportWithListName = from c in cards
                                            join l in view.CheckedLists on c.IdList equals l.Id into gj
                                            where IntersectLabelLists(c.Labels,labelFilter)
                                            select CreateStringArrayFromCard(c, gj, fieldsToInclude);
            }
            else
            {
                cardsToImportWithListName = from c in cards
                                            join l in view.CheckedLists on c.IdList equals l.Id into gj
                                            select CreateStringArrayFromCard(c, gj, fieldsToInclude);
            }

            return new[] { fieldsToInclude.ToArray() }.Union(cardsToImportWithListName).ToArray().ToMultidimensionalArray();
        }

        private static string[] CreateStringArrayFromCard(Card card, IEnumerable<List> lists, IEnumerable<string> fieldsToInclude)
        {
            var list = new List<string>();
            Match match = Regex.Match(card.Name, @"(.*)?\[(([0-9]+)/)?([0-9]+)\](.*)?");

            if (fieldsToInclude.Contains("Name"))
            {
                if (match.Success)
                {
                    list.Add(match.Groups[1].Value.Trim() + match.Groups[5].Value);
                }
                else
                {
                    list.Add(card.Name);
                }
            }
            if (fieldsToInclude.Contains("Description"))
                list.Add(card.Desc);
            if (fieldsToInclude.Contains("Due Date"))
                list.Add(card.Due.ToString());
            if (fieldsToInclude.Contains("List"))
                list.Add(lists.FirstOrDefault() != null ? lists.FirstOrDefault().Name : null);

            int est = -1, log = -1, taskEst = -1, taskLog = -1;

		    if (match.Success)
		    {
                est = int.Parse(match.Groups[4].Value);
            }
            if (match.Success)
            {
                if (match.Groups[3].Value.Trim() != "")
                    log = int.Parse(match.Groups[3].Value);
            }
            string relTasks = "", allTasks = "";
            int i = 0;

            foreach (TrelloNet.Card.Checklist cl in card.Checklists)
            {
                Match clMatch = Regex.Match(cl.Name, @"\{(.*?)\}");
                bool relevant = false;

                if (!clMatch.Success || clMatch.Groups[1].Value == (lists.FirstOrDefault() != null ? lists.FirstOrDefault().Name : null))
                    relevant = true;

                foreach (TrelloNet.Card.CheckItem ci in cl.CheckItems)
                {
                    Match ciMatch = Regex.Match(ci.Name, @"(.*)?\[(([0-9]+)/)?([0-9]+)\](.*)?");

                    if (ciMatch.Success)
                    {
                        if (taskEst < 0) taskEst = 0;
                        if (taskLog < 0) taskLog = 0;
                        taskEst += int.Parse(ciMatch.Groups[4].Value);
                        if (ci.Checked)
                            taskLog += int.Parse(ciMatch.Groups[4].Value);
                        else if (ciMatch.Groups[3].Value.Trim() != "")
                            taskLog += int.Parse(ciMatch.Groups[3].Value);
                    }
                    string ciName = ciMatch.Groups[0].Value.Trim() + ciMatch.Groups[5].Value;
                    if (relevant)
                        relTasks += (i++ > 0 ? ",\r\n" : "") + ciName;
                    allTasks += (i++ > 0 ? ",\r\n" : "") + ciName;
                }
            }

            if (fieldsToInclude.Contains("Estimates"))
            {
                if (taskEst >= 0)
                    list.Add(taskEst.ToString());
                else if (est >= 0)
                    list.Add(est.ToString());
                else
                    list.Add("");
            }
            if (fieldsToInclude.Contains("Time Log"))
            {
                if (taskLog >= 0)
                    list.Add(taskLog.ToString());
                else if (log >= 0)
                    list.Add(log.ToString());
                else
                    list.Add("");
            }
            if (fieldsToInclude.Contains("Labels"))
            {
                var labelStrings = from l in card.Labels
                                   select l.Name + "(" + l.Color.ToString() + ")";
                string labelString = "";
                foreach (string l in labelStrings)
                {
                    labelString += (labelString.Length > 0?",\r\n":"") + l;
                }

                list.Add(labelString);
            }
            if (fieldsToInclude.Contains("Tasks (Relevant)"))
            {
                list.Add(relTasks);
            }
            if (fieldsToInclude.Contains("Tasks (All)"))
            {
                list.Add(allTasks);
            }

            return list.ToArray();
        }

        private void LabelItemCheckedChanged(object sender, EventArgs eventArgs)
        {
            view.EnableUpdate = view.EnableImport = view.CheckedLabels.Any();
        }

        private void ListItemCheckedChanged(object sender, EventArgs eventArgs)
        {
            view.EnableUpdate = view.EnableImport = view.CheckedLists.Any();
        }

        private void BoardWasSelected(object sender, EventArgs e)
        {
            trello.Async.Lists.ForBoard(view.SelectedBoard)
                .ContinueWith(t =>
                {
                    if (t.Exception == null)
                    {
                        view.DisplayLists(t.Result);
                        view.EnableSelectionOfLists = true;
                        view.EnableImport = false;
                        view.EnableUpdate = false;
                    }
                    else
                    {
                        HandleException(t.Exception);
                    }
                }, taskScheduler);
            trello.Async.Boards.WithId(view.SelectedBoard.GetBoardId())
                .ContinueWith(t =>
                {
                    if (t.Exception == null)
                    {
                        view.DisplayLabels(t.Result.LabelNames);
                        view.EnableSelectionOfLabels = true;
                    }
                    else
                    {
                        HandleException(t.Exception);
                    }
                }, taskScheduler);
        }

        private void SetupMessageEventHandlers()
        {
            messageBus.Subscribe<TrelloWasAuthorizedEvent>(_ => FetchAndDisplayBoards());
            messageBus.Subscribe<TrelloWasUnauthorizedEvent>(_ => HandleTrelloWasUnauthorized());
        }

        private void HandleTrelloWasUnauthorized()
        {
            DisableStuff();
            view.DisplayBoards(Enumerable.Empty<BoardViewModel>());
            view.DisplayLists(Enumerable.Empty<List>());
            view.DisplayLabels(new Dictionary<Color, string>());
            view.ShowStatusMessage("");
        }

        private void DisableStuff()
        {
            view.EnableSelectionOfLists = false;
            //view.EnableSelectionOfLabels = false;
            view.EnableSelectionOfBoards = false;
            view.EnableImport = false;
            view.EnableUpdate = false;
        }

        private void FetchAndDisplayBoards()
        {
            DisableStuff();
            Task.Factory.StartNew(() => trelloHelper.FetchBoardViewModelsForMe())
            .ContinueWith(t =>
            {
                if (t.Exception == null)
                {
                    view.DisplayBoards(t.Result);
                    view.EnableSelectionOfBoards = true;
                }
                else
                {
                    HandleException(t.Exception);
                }
            }, taskScheduler);
        }

        private void HandleException(AggregateException exception)
        {          
            if (exception.InnerException is TrelloUnauthorizedException)
                messageBus.Publish(new TrelloWasUnauthorizedEvent(exception.InnerException.Message));
            else
                view.ShowErrorMessage(exception.InnerException.Message);
        }
    }
}