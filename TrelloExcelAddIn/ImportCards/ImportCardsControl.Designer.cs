﻿namespace TrelloExcelAddIn
{
    partial class ImportCardsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChooseListsGroupBox = new System.Windows.Forms.GroupBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.ListsLabel = new System.Windows.Forms.Label();
            this.ListsBox = new System.Windows.Forms.CheckedListBox();
            this.BoardsLabel = new System.Windows.Forms.Label();
            this.BoardComboBox = new System.Windows.Forms.ComboBox();
            this.UpdateCardsButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.ImportCardsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FieldsToIncludeListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelsBox = new TrelloExcelAddIn.ColorCheckedListBox();
            this.ChooseListsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseListsGroupBox
            // 
            this.ChooseListsGroupBox.Controls.Add(this.RefreshButton);
            this.ChooseListsGroupBox.Controls.Add(this.ListsLabel);
            this.ChooseListsGroupBox.Controls.Add(this.ListsBox);
            this.ChooseListsGroupBox.Controls.Add(this.BoardsLabel);
            this.ChooseListsGroupBox.Controls.Add(this.BoardComboBox);
            this.ChooseListsGroupBox.Location = new System.Drawing.Point(5, 203);
            this.ChooseListsGroupBox.Name = "ChooseListsGroupBox";
            this.ChooseListsGroupBox.Size = new System.Drawing.Size(256, 147);
            this.ChooseListsGroupBox.TabIndex = 0;
            this.ChooseListsGroupBox.TabStop = false;
            this.ChooseListsGroupBox.Text = "3. Choose from which board and lists to import";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Image = global::TrelloExcelAddIn.Properties.Resources.refresh_sign_24_241;
            this.RefreshButton.Location = new System.Drawing.Point(6, 65);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(34, 32);
            this.RefreshButton.TabIndex = 16;
            this.RefreshButton.UseVisualStyleBackColor = true;
            // 
            // ListsLabel
            // 
            this.ListsLabel.AutoSize = true;
            this.ListsLabel.Location = new System.Drawing.Point(6, 49);
            this.ListsLabel.Name = "ListsLabel";
            this.ListsLabel.Size = new System.Drawing.Size(28, 13);
            this.ListsLabel.TabIndex = 13;
            this.ListsLabel.Text = "Lists";
            this.ListsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ListsBox
            // 
            this.ListsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListsBox.CheckOnClick = true;
            this.ListsBox.Enabled = false;
            this.ListsBox.FormattingEnabled = true;
            this.ListsBox.Location = new System.Drawing.Point(44, 49);
            this.ListsBox.Name = "ListsBox";
            this.ListsBox.Size = new System.Drawing.Size(194, 92);
            this.ListsBox.TabIndex = 12;
            // 
            // BoardsLabel
            // 
            this.BoardsLabel.AutoSize = true;
            this.BoardsLabel.Location = new System.Drawing.Point(6, 25);
            this.BoardsLabel.Name = "BoardsLabel";
            this.BoardsLabel.Size = new System.Drawing.Size(35, 13);
            this.BoardsLabel.TabIndex = 11;
            this.BoardsLabel.Text = "Board";
            this.BoardsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BoardComboBox
            // 
            this.BoardComboBox.Enabled = false;
            this.BoardComboBox.FormattingEnabled = true;
            this.BoardComboBox.Location = new System.Drawing.Point(44, 22);
            this.BoardComboBox.Name = "BoardComboBox";
            this.BoardComboBox.Size = new System.Drawing.Size(194, 21);
            this.BoardComboBox.TabIndex = 10;
            // 
            // UpdateCardsButton
            // 
            this.UpdateCardsButton.Enabled = false;
            this.UpdateCardsButton.Location = new System.Drawing.Point(111, 490);
            this.UpdateCardsButton.Name = "UpdateCardsButton";
            this.UpdateCardsButton.Size = new System.Drawing.Size(94, 22);
            this.UpdateCardsButton.TabIndex = 17;
            this.UpdateCardsButton.Text = "5. Update cards";
            this.UpdateCardsButton.UseVisualStyleBackColor = true;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(11, 455);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(43, 13);
            this.StatusLabel.TabIndex = 15;
            this.StatusLabel.Text = "[Status]";
            // 
            // ImportCardsButton
            // 
            this.ImportCardsButton.Enabled = false;
            this.ImportCardsButton.Location = new System.Drawing.Point(11, 490);
            this.ImportCardsButton.Name = "ImportCardsButton";
            this.ImportCardsButton.Size = new System.Drawing.Size(94, 22);
            this.ImportCardsButton.TabIndex = 14;
            this.ImportCardsButton.Text = "5. Import cards";
            this.ImportCardsButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 58);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1. Select where to import rows";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(4);
            this.label3.Size = new System.Drawing.Size(250, 39);
            this.label3.TabIndex = 12;
            this.label3.Text = "Select a cell. Imported cards will be inserted as rows below it.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.FieldsToIncludeListBox);
            this.groupBox2.Location = new System.Drawing.Point(5, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(256, 125);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. Choose which fields to include";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fields";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FieldsToIncludeListBox
            // 
            this.FieldsToIncludeListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FieldsToIncludeListBox.CheckOnClick = true;
            this.FieldsToIncludeListBox.FormattingEnabled = true;
            this.FieldsToIncludeListBox.Items.AddRange(new object[] {
            "Name",
            "Description",
            "Due Date",
            "List",
            "Estimates",
            "Time Log",
            "Labels",
            "Tasks (Relevant)",
            "Tasks (All)"});
            this.FieldsToIncludeListBox.Location = new System.Drawing.Point(44, 23);
            this.FieldsToIncludeListBox.Name = "FieldsToIncludeListBox";
            this.FieldsToIncludeListBox.Size = new System.Drawing.Size(194, 92);
            this.FieldsToIncludeListBox.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LabelsBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(5, 350);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 102);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "4. Choose a label filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Labels";
            // 
            // LabelsBox
            // 
            this.LabelsBox.CheckOnClick = true;
            this.LabelsBox.ColorSource = "Key";
            this.LabelsBox.FormattingEnabled = true;
            this.LabelsBox.Location = new System.Drawing.Point(44, 16);
            this.LabelsBox.Name = "LabelsBox";
            this.LabelsBox.Size = new System.Drawing.Size(194, 79);
            this.LabelsBox.TabIndex = 2;
            // 
            // ImportCardsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.UpdateCardsButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ImportCardsButton);
            this.Controls.Add(this.ChooseListsGroupBox);
            this.Name = "ImportCardsControl";
            this.Size = new System.Drawing.Size(273, 515);
            this.ChooseListsGroupBox.ResumeLayout(false);
            this.ChooseListsGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox ChooseListsGroupBox;
        private System.Windows.Forms.Label BoardsLabel;
        private System.Windows.Forms.ComboBox BoardComboBox;
        private System.Windows.Forms.Label ListsLabel;
        private System.Windows.Forms.CheckedListBox ListsBox;
        private System.Windows.Forms.Button ImportCardsButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox FieldsToIncludeListBox;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button UpdateCardsButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private TrelloExcelAddIn.ColorCheckedListBox LabelsBox;
        private System.Windows.Forms.Label label1;
    }
}
