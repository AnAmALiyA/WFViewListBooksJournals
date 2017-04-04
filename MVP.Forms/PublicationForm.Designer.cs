namespace WFViewListBooksJournals.Forms
{
    partial class PublicationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxPublicationForm = new System.Windows.Forms.ListBox();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelNamePublications = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.textBoxPages = new System.Windows.Forms.TextBox();
            this.labelPages = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelLocation = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.comboBoxAuthor = new System.Windows.Forms.ComboBox();
            this.buttonNewAuthor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxPublicationForm
            // 
            this.listBoxPublicationForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPublicationForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxPublicationForm.FormattingEnabled = true;
            this.listBoxPublicationForm.ItemHeight = 15;
            this.listBoxPublicationForm.Location = new System.Drawing.Point(12, 71);
            this.listBoxPublicationForm.Name = "listBoxPublicationForm";
            this.listBoxPublicationForm.Size = new System.Drawing.Size(1069, 244);
            this.listBoxPublicationForm.TabIndex = 0;
            this.listBoxPublicationForm.SelectedIndexChanged += new System.EventHandler(this.listBoxPublicationForm_SelectedIndexChanged);
            this.listBoxPublicationForm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxPublicationForm_MouseDoubleClick);
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(12, 11);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(38, 13);
            this.labelAuthor.TabIndex = 2;
            this.labelAuthor.Text = "Author";
            // 
            // labelNamePublications
            // 
            this.labelNamePublications.AutoSize = true;
            this.labelNamePublications.Location = new System.Drawing.Point(171, 13);
            this.labelNamePublications.Name = "labelNamePublications";
            this.labelNamePublications.Size = new System.Drawing.Size(60, 13);
            this.labelNamePublications.TabIndex = 3;
            this.labelNamePublications.Text = "NameBook";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(174, 31);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(177, 20);
            this.textBoxName.TabIndex = 4;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(358, 13);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(30, 13);
            this.labelDate.TabIndex = 5;
            this.labelDate.Text = "Date";
            // 
            // textBoxPages
            // 
            this.textBoxPages.Location = new System.Drawing.Point(560, 30);
            this.textBoxPages.Name = "textBoxPages";
            this.textBoxPages.Size = new System.Drawing.Size(91, 20);
            this.textBoxPages.TabIndex = 7;
            // 
            // labelPages
            // 
            this.labelPages.AutoSize = true;
            this.labelPages.Location = new System.Drawing.Point(560, 11);
            this.labelPages.Name = "labelPages";
            this.labelPages.Size = new System.Drawing.Size(37, 13);
            this.labelPages.TabIndex = 8;
            this.labelPages.Text = "Pages";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(658, 13);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 9;
            this.labelTitle.Text = "Title";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(661, 30);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(138, 20);
            this.textBoxTitle.TabIndex = 10;
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(806, 30);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.Size = new System.Drawing.Size(100, 20);
            this.textBoxLocation.TabIndex = 11;
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(806, 12);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(48, 13);
            this.labelLocation.TabIndex = 12;
            this.labelLocation.Text = "Location";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(923, 7);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 13;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(1006, 7);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 14;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(361, 31);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(183, 20);
            this.dateTimePickerDate.TabIndex = 15;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(923, 36);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 16;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // comboBoxAuthor
            // 
            this.comboBoxAuthor.FormattingEnabled = true;
            this.comboBoxAuthor.Location = new System.Drawing.Point(12, 29);
            this.comboBoxAuthor.Name = "comboBoxAuthor";
            this.comboBoxAuthor.Size = new System.Drawing.Size(143, 21);
            this.comboBoxAuthor.TabIndex = 17;
            // 
            // buttonNewAuthor
            // 
            this.buttonNewAuthor.Location = new System.Drawing.Point(1006, 36);
            this.buttonNewAuthor.Name = "buttonNewAuthor";
            this.buttonNewAuthor.Size = new System.Drawing.Size(75, 23);
            this.buttonNewAuthor.TabIndex = 18;
            this.buttonNewAuthor.Text = "New Author";
            this.buttonNewAuthor.UseVisualStyleBackColor = true;
            this.buttonNewAuthor.Click += new System.EventHandler(this.buttonNewAuthor_Click);
            // 
            // PublicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 331);
            this.Controls.Add(this.buttonNewAuthor);
            this.Controls.Add(this.comboBoxAuthor);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelLocation);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelPages);
            this.Controls.Add(this.textBoxPages);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelNamePublications);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.listBoxPublicationForm);
            this.MinimumSize = new System.Drawing.Size(1109, 367);
            this.Name = "PublicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BeforeClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox listBoxPublicationForm;
        public System.Windows.Forms.Label labelAuthor;
        public System.Windows.Forms.Label labelNamePublications;
        public System.Windows.Forms.TextBox textBoxName;
        public System.Windows.Forms.Label labelDate;
        public System.Windows.Forms.DateTimePicker dateTimePickerDate;
        public System.Windows.Forms.TextBox textBoxPages;
        public System.Windows.Forms.Label labelPages;
        public System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.TextBox textBoxTitle;
        public System.Windows.Forms.TextBox textBoxLocation;
        public System.Windows.Forms.Label labelLocation;
        public System.Windows.Forms.ComboBox comboBoxAuthor;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonNewAuthor;
    }
}