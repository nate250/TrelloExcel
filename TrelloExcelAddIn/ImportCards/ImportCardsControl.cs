﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrelloNet;

namespace TrelloExcelAddIn
{
    public partial class ImportCardsControl : UserControl, IImportCardsView
    {
        public ImportCardsControl()
        {
            InitializeComponent();

            for (var i = 0; i < FieldsToIncludeListBox.Items.Count; i++)
            {
                if (FieldsToIncludeListBox.Items[i].ToString().Contains("Tasks")) continue;
                FieldsToIncludeListBox.SetItemChecked(i, true);
            }

            StatusLabel.Text = "";

            BoardComboBox.SelectedIndexChanged += (sender, args) => BoardWasSelected(this, null);
            ListsBox.ItemCheck += (sender, args) => BeginInvoke((MethodInvoker)(() => ListItemCheckedChanged(this, null)));
            ImportCardsButton.Click += (sender, args) => ImportCardsButtonWasClicked(this, null);
            UpdateCardsButton.Click += (sender, args) => UpdateCardsButtonWasClicked(this, null);
            RefreshButton.Click += (sender, args) => RefreshButtonWasClicked(this, null);
        }

        public event EventHandler BoardWasSelected;
        public event EventHandler ListItemCheckedChanged;
        public event EventHandler ImportCardsButtonWasClicked;
        public event EventHandler UpdateCardsButtonWasClicked;
        public event EventHandler RefreshButtonWasClicked;

        public void ShowStatusMessage(string message)
        {
            StatusLabel.Text = message;
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void DisplayBoards(IEnumerable<BoardViewModel> boards)
        {
            var boardViewModels = boards.ToList();

            BoardComboBox.DataSource = boardViewModels;

            if (!boardViewModels.Any())
                BoardComboBox.Text = "";
        }

        public bool EnableSelectionOfBoards
        {
            get { return BoardComboBox.Enabled; }
            set { BoardComboBox.Enabled = value; }
        }

        public IBoardId SelectedBoard
        {
            get { return (IBoardId)BoardComboBox.SelectedValue; }            
        }

        public bool EnableSelectionOfLists
        {
            get { return ListsBox.Enabled; }
            set { ListsBox.Enabled = value; }
        }

        public bool EnableImport
        {
            get { return ImportCardsButton.Enabled; }
            set { ImportCardsButton.Enabled = value; }
        }

        public bool EnableUpdate
        {
            get { return UpdateCardsButton.Enabled; }
            set { UpdateCardsButton.Enabled = value; }
        }

        public IEnumerable<List> CheckedLists
        {
            get { return ListsBox.CheckedItems.Cast<List>(); }
        }

        public IEnumerable<string> FieldsToInclude
        {
            get { return FieldsToIncludeListBox.CheckedItems.Cast<string>(); }
        }

        public void DisplayLists(IEnumerable<List> lists)
        {
            ListsBox.DataSource = null;         
            ListsBox.DataSource = lists;

            for (var i = 0; i < ListsBox.Items.Count; i++ )
                ListsBox.SetItemChecked(i, true);
        }

        private void FieldsToIncludeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
