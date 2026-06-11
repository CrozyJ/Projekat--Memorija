namespace WindowsFormsApplication13
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblHighScore = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.NChooser = new System.Windows.Forms.ComboBox();
            this.PictureChoose = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(22, 24);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(47, 13);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "Time: 0s";
            // 
            // lblHighScore
            // 
            this.lblHighScore.AutoSize = true;
            this.lblHighScore.Location = new System.Drawing.Point(89, 25);
            this.lblHighScore.Name = "lblHighScore";
            this.lblHighScore.Size = new System.Drawing.Size(72, 13);
            this.lblHighScore.TabIndex = 1;
            this.lblHighScore.Text = "High Score: --";
            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(179, 18);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 24);
            this.btnRestart.TabIndex = 2;
            this.btnRestart.Text = "Restart Game";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click_1);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 1000;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Pause";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NChooser
            // 
            this.NChooser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NChooser.FormattingEnabled = true;
            this.NChooser.Items.AddRange(new object[] {
            "8",
            "16",
            "32"});
            this.NChooser.Location = new System.Drawing.Point(363, 18);
            this.NChooser.Name = "NChooser";
            this.NChooser.Size = new System.Drawing.Size(97, 21);
            this.NChooser.TabIndex = 4;
            this.NChooser.SelectedIndexChanged += new System.EventHandler(this.NChooser_SelectedIndexChanged);
            // 
            // PictureChoose
            // 
            this.PictureChoose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PictureChoose.FormattingEnabled = true;
            this.PictureChoose.Items.AddRange(new object[] {
            "Regular",
            "3.SI2",
            "Fruit ",
            "Animals ",
            "Elites "});
            this.PictureChoose.Location = new System.Drawing.Point(478, 16);
            this.PictureChoose.Name = "PictureChoose";
            this.PictureChoose.Size = new System.Drawing.Size(103, 21);
            this.PictureChoose.TabIndex = 5;
            this.PictureChoose.SelectedIndexChanged += new System.EventHandler(this.PictureChoose_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 521);
            this.Controls.Add(this.PictureChoose);
            this.Controls.Add(this.NChooser);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.lblHighScore);
            this.Controls.Add(this.lblTime);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblHighScore;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox NChooser;
        private System.Windows.Forms.ComboBox PictureChoose;

    }
}

