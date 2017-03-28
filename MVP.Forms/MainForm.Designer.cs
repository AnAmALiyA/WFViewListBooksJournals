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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.showAll = new System.Windows.Forms.Button();
            this.saveNewspapers = new System.Windows.Forms.Button();
            this.showAllArticles = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.buttonSaveJournals = new System.Windows.Forms.Button();
            this.buttonSaveBooks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(13, 58);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1043, 199);
            this.listBox1.TabIndex = 0;
            // 
            // showAll
            // 
            this.showAll.Location = new System.Drawing.Point(13, 13);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(75, 23);
            this.showAll.TabIndex = 1;
            this.showAll.Text = "Show All";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.Click += new System.EventHandler(this.ShowAll_Click);
            // 
            // saveNewspapers
            // 
            this.saveNewspapers.Location = new System.Drawing.Point(353, 13);
            this.saveNewspapers.Name = "saveNewspapers";
            this.saveNewspapers.Size = new System.Drawing.Size(107, 23);
            this.saveNewspapers.TabIndex = 2;
            this.saveNewspapers.Text = "Save Newspapers";
            this.saveNewspapers.UseVisualStyleBackColor = true;
            this.saveNewspapers.Click += new System.EventHandler(this.SaveNewspapers_Click);
            // 
            // showAllArticles
            // 
            this.showAllArticles.Location = new System.Drawing.Point(94, 13);
            this.showAllArticles.Name = "showAllArticles";
            this.showAllArticles.Size = new System.Drawing.Size(75, 23);
            this.showAllArticles.TabIndex = 3;
            this.showAllArticles.Text = "All articles";
            this.showAllArticles.UseVisualStyleBackColor = true;
            this.showAllArticles.Click += new System.EventHandler(this.ShowAllArticles_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(466, 15);
            this.comboBox1.MaxDropDownItems = 10;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(127, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(599, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Changes publications";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(602, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(136, 21);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 288);
            this.Controls.Add(this.buttonSaveBooks);
            this.Controls.Add(this.buttonSaveJournals);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.showAllArticles);
            this.Controls.Add(this.saveNewspapers);
            this.Controls.Add(this.showAll);
            this.Controls.Add(this.listBox1);
            this.MinimumSize = new System.Drawing.Size(589, 324);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Literutures";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button showAll;
        private System.Windows.Forms.Button saveNewspapers;
        private System.Windows.Forms.Button showAllArticles;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button buttonSaveJournals;
        private System.Windows.Forms.Button buttonSaveBooks;
    }
}

