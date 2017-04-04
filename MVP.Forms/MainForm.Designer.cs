namespace WFViewListBooksJournals.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxMainForm = new System.Windows.Forms.ListBox();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.buttonSaveNewspapers = new System.Windows.Forms.Button();
            this.buttonShowAllArticles = new System.Windows.Forms.Button();
            this.comboBoxAuthors = new System.Windows.Forms.ComboBox();
            this.labelPublication = new System.Windows.Forms.Label();
            this.comboBoxPublications = new System.Windows.Forms.ComboBox();
            this.buttonSaveJournals = new System.Windows.Forms.Button();
            this.buttonSaveBooks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxMainForm
            // 
            this.listBoxMainForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMainForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxMainForm.FormattingEnabled = true;
            this.listBoxMainForm.HorizontalScrollbar = true;
            this.listBoxMainForm.ItemHeight = 15;
            this.listBoxMainForm.Location = new System.Drawing.Point(13, 58);
            this.listBoxMainForm.Name = "listBoxMainForm";
            this.listBoxMainForm.Size = new System.Drawing.Size(1043, 199);
            this.listBoxMainForm.TabIndex = 0;
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Location = new System.Drawing.Point(13, 13);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(75, 23);
            this.buttonShowAll.TabIndex = 1;
            this.buttonShowAll.Text = "Show All";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            // 
            // buttonSaveNewspapers
            // 
            this.buttonSaveNewspapers.Location = new System.Drawing.Point(353, 13);
            this.buttonSaveNewspapers.Name = "buttonSaveNewspapers";
            this.buttonSaveNewspapers.Size = new System.Drawing.Size(107, 23);
            this.buttonSaveNewspapers.TabIndex = 2;
            this.buttonSaveNewspapers.Text = "Save Newspapers";
            this.buttonSaveNewspapers.UseVisualStyleBackColor = true;
            this.buttonSaveNewspapers.Click += new System.EventHandler(this.buttonSaveNewspapers_Click);
            // 
            // buttonShowAllArticles
            // 
            this.buttonShowAllArticles.Location = new System.Drawing.Point(94, 13);
            this.buttonShowAllArticles.Name = "buttonShowAllArticles";
            this.buttonShowAllArticles.Size = new System.Drawing.Size(75, 23);
            this.buttonShowAllArticles.TabIndex = 3;
            this.buttonShowAllArticles.Text = "All articles";
            this.buttonShowAllArticles.UseVisualStyleBackColor = true;
            this.buttonShowAllArticles.Click += new System.EventHandler(this.buttonShowAllArticles_Click);
            // 
            // comboBoxAuthors
            // 
            this.comboBoxAuthors.FormattingEnabled = true;
            this.comboBoxAuthors.Location = new System.Drawing.Point(466, 15);
            this.comboBoxAuthors.MaxDropDownItems = 10;
            this.comboBoxAuthors.Name = "comboBoxAuthors";
            this.comboBoxAuthors.Size = new System.Drawing.Size(127, 21);
            this.comboBoxAuthors.TabIndex = 4;
            this.comboBoxAuthors.SelectedIndexChanged += new System.EventHandler(this.comboBoxAuthors_SelectedIndexChanged);
            // 
            // labelPublication
            // 
            this.labelPublication.AutoSize = true;
            this.labelPublication.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPublication.Location = new System.Drawing.Point(599, 9);
            this.labelPublication.Name = "labelPublication";
            this.labelPublication.Size = new System.Drawing.Size(143, 17);
            this.labelPublication.TabIndex = 5;
            this.labelPublication.Text = "Changes publications";
            // 
            // comboBoxPublications
            // 
            this.comboBoxPublications.FormattingEnabled = true;
            this.comboBoxPublications.Location = new System.Drawing.Point(602, 29);
            this.comboBoxPublications.Name = "comboBoxPublications";
            this.comboBoxPublications.Size = new System.Drawing.Size(136, 21);
            this.comboBoxPublications.TabIndex = 6;
            this.comboBoxPublications.SelectedIndexChanged += new System.EventHandler(this.comboBoxPublications_SelectedIndexChanged);
            // 
            // buttonSaveJournals
            // 
            this.buttonSaveJournals.Location = new System.Drawing.Point(257, 13);
            this.buttonSaveJournals.Name = "buttonSaveJournals";
            this.buttonSaveJournals.Size = new System.Drawing.Size(90, 23);
            this.buttonSaveJournals.TabIndex = 7;
            this.buttonSaveJournals.Text = "Save Journals";
            this.buttonSaveJournals.UseVisualStyleBackColor = true;
            this.buttonSaveJournals.Click += new System.EventHandler(this.buttonSaveJournals_Click);
            // 
            // buttonSaveBooks
            // 
            this.buttonSaveBooks.Location = new System.Drawing.Point(176, 13);
            this.buttonSaveBooks.Name = "buttonSaveBooks";
            this.buttonSaveBooks.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveBooks.TabIndex = 8;
            this.buttonSaveBooks.Text = "Save Books";
            this.buttonSaveBooks.UseVisualStyleBackColor = true;
            this.buttonSaveBooks.Click += new System.EventHandler(this.buttonSaveBooks_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 288);
            this.Controls.Add(this.buttonSaveBooks);
            this.Controls.Add(this.buttonSaveJournals);
            this.Controls.Add(this.comboBoxPublications);
            this.Controls.Add(this.labelPublication);
            this.Controls.Add(this.comboBoxAuthors);
            this.Controls.Add(this.buttonShowAllArticles);
            this.Controls.Add(this.buttonSaveNewspapers);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.listBoxMainForm);
            this.MinimumSize = new System.Drawing.Size(589, 324);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Literutures";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMainForm;
        private System.Windows.Forms.Button buttonShowAll;
        private System.Windows.Forms.Button buttonSaveNewspapers;
        private System.Windows.Forms.Button buttonShowAllArticles;
        private System.Windows.Forms.ComboBox comboBoxAuthors;
        private System.Windows.Forms.Label labelPublication;
        private System.Windows.Forms.ComboBox comboBoxPublications;
        private System.Windows.Forms.Button buttonSaveJournals;
        private System.Windows.Forms.Button buttonSaveBooks;
    }
}

