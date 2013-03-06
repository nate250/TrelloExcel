using System;
using System.Collections.Generic;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public interface IImportCardsView
    {
        event EventHandler BoardWasSelected;

        void DisplayBoards(IEnumerable<BoardViewModel> boards);
        bool EnableSelectionOfBoards { get; set; }
        IBoardId SelectedBoard { get; }
        bool EnableSelectionOfLists { get; set; }
        bool EnableSelectionOfLabels { get; set; }
        bool EnableImport { get; set; }
        bool EnableUpdate { get; set; }
        IEnumerable<List> CheckedLists { get; }
        IEnumerable<KeyValuePair<Color, string>> CheckedLabels { get; }
        IEnumerable<string> FieldsToInclude { get; }
        void DisplayLists(IEnumerable<List> lists);
        void DisplayLabels(Dictionary<Color, string> labels);
        event EventHandler ListItemCheckedChanged;
        event EventHandler LabelItemCheckedChanged;
        event EventHandler ImportCardsButtonWasClicked;
        event EventHandler UpdateCardsButtonWasClicked;
        void ShowStatusMessage(string message);
        void ShowErrorMessage(string message);
        event EventHandler RefreshButtonWasClicked;
    }
}