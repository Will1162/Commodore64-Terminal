
namespace Commodore_64_Emulator
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.displayPictureBox = new System.Windows.Forms.PictureBox();
            this.toggleKeyboardButton = new System.Windows.Forms.Button();
            this.toggleDebugButton = new System.Windows.Forms.Button();
            this.LoadProgramButton = new System.Windows.Forms.Button();
            this.SaveProgramButton = new System.Windows.Forms.Button();
            this.FocusDummy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPictureBox
            // 
            this.displayPictureBox.Location = new System.Drawing.Point(0, 0);
            this.displayPictureBox.Name = "displayPictureBox";
            this.displayPictureBox.Size = new System.Drawing.Size(320, 200);
            this.displayPictureBox.TabIndex = 0;
            this.displayPictureBox.TabStop = false;
            // 
            // toggleKeyboardButton
            // 
            this.toggleKeyboardButton.Location = new System.Drawing.Point(0, 200);
            this.toggleKeyboardButton.Name = "toggleKeyboardButton";
            this.toggleKeyboardButton.Size = new System.Drawing.Size(80, 50);
            this.toggleKeyboardButton.TabIndex = 1;
            this.toggleKeyboardButton.TabStop = false;
            this.toggleKeyboardButton.Text = "Toggle Extended Keyboard";
            this.toggleKeyboardButton.UseVisualStyleBackColor = true;
            this.toggleKeyboardButton.Click += new System.EventHandler(this.ToggleKeyboardButton_Click);
            this.toggleKeyboardButton.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            // 
            // toggleDebugButton
            // 
            this.toggleDebugButton.Location = new System.Drawing.Point(80, 200);
            this.toggleDebugButton.Name = "toggleDebugButton";
            this.toggleDebugButton.Size = new System.Drawing.Size(80, 50);
            this.toggleDebugButton.TabIndex = 2;
            this.toggleDebugButton.TabStop = false;
            this.toggleDebugButton.Text = "Toggle Debug Info";
            this.toggleDebugButton.UseVisualStyleBackColor = true;
            this.toggleDebugButton.Click += new System.EventHandler(this.ToggleDebugButton_Click);
            this.toggleDebugButton.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            // 
            // LoadProgramButton
            // 
            this.LoadProgramButton.Location = new System.Drawing.Point(160, 200);
            this.LoadProgramButton.Name = "LoadProgramButton";
            this.LoadProgramButton.Size = new System.Drawing.Size(80, 50);
            this.LoadProgramButton.TabIndex = 3;
            this.LoadProgramButton.TabStop = false;
            this.LoadProgramButton.Text = "Load Program";
            this.LoadProgramButton.UseVisualStyleBackColor = true;
            this.LoadProgramButton.Click += new System.EventHandler(this.LoadProgramButton_Click);
            this.LoadProgramButton.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            // 
            // SaveProgramButton
            // 
            this.SaveProgramButton.Location = new System.Drawing.Point(240, 200);
            this.SaveProgramButton.Name = "SaveProgramButton";
            this.SaveProgramButton.Size = new System.Drawing.Size(80, 50);
            this.SaveProgramButton.TabIndex = 4;
            this.SaveProgramButton.TabStop = false;
            this.SaveProgramButton.Text = "Save Program";
            this.SaveProgramButton.UseVisualStyleBackColor = true;
            this.SaveProgramButton.Click += new System.EventHandler(this.SaveProgramButton_Click);
            this.SaveProgramButton.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            // 
            // FocusDummy
            // 
            this.FocusDummy.AutoSize = true;
            this.FocusDummy.Location = new System.Drawing.Point(0, 0);
            this.FocusDummy.Name = "FocusDummy";
            this.FocusDummy.Size = new System.Drawing.Size(58, 13);
            this.FocusDummy.TabIndex = 5;
            this.FocusDummy.Text = "Hello there";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 250);
            this.Controls.Add(this.SaveProgramButton);
            this.Controls.Add(this.LoadProgramButton);
            this.Controls.Add(this.toggleDebugButton);
            this.Controls.Add(this.toggleKeyboardButton);
            this.Controls.Add(this.displayPictureBox);
            this.Controls.Add(this.FocusDummy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Commodore 64 Emulator | Will Burland";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.displayPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox displayPictureBox;
        private System.Windows.Forms.Button toggleKeyboardButton;
        private System.Windows.Forms.Button toggleDebugButton;
        private System.Windows.Forms.Button LoadProgramButton;
        private System.Windows.Forms.Button SaveProgramButton;
        private System.Windows.Forms.Label FocusDummy;
    }
}

