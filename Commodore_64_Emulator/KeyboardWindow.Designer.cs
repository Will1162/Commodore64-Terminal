
namespace Commodore_64_Emulator
{
    partial class KeyboardWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardWindow));
            this.textCharactersImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.enterButton = new System.Windows.Forms.Button();
            this.backspaceButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.spaceButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textCharactersImage)).BeginInit();
            this.SuspendLayout();
            // 
            // textCharactersImage
            // 
            this.textCharactersImage.Location = new System.Drawing.Point(10, 40);
            this.textCharactersImage.Name = "textCharactersImage";
            this.textCharactersImage.Size = new System.Drawing.Size(512, 128);
            this.textCharactersImage.TabIndex = 1;
            this.textCharactersImage.TabStop = false;
            this.textCharactersImage.Click += new System.EventHandler(this.textCharactersImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text Characters";
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(445, 187);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(75, 23);
            this.enterButton.TabIndex = 3;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // backspaceButton
            // 
            this.backspaceButton.Location = new System.Drawing.Point(445, 219);
            this.backspaceButton.Name = "backspaceButton";
            this.backspaceButton.Size = new System.Drawing.Size(75, 23);
            this.backspaceButton.TabIndex = 4;
            this.backspaceButton.Text = "Backspace";
            this.backspaceButton.UseVisualStyleBackColor = true;
            this.backspaceButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(42, 184);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(26, 26);
            this.upButton.TabIndex = 5;
            this.upButton.Text = "🡅";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(42, 216);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(26, 26);
            this.downButton.TabIndex = 6;
            this.downButton.Text = "🡇";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(10, 216);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(26, 26);
            this.leftButton.TabIndex = 7;
            this.leftButton.Text = "🡄";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(74, 216);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(26, 26);
            this.rightButton.TabIndex = 8;
            this.rightButton.Text = "🡆";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // spaceButton
            // 
            this.spaceButton.Location = new System.Drawing.Point(364, 219);
            this.spaceButton.Name = "spaceButton";
            this.spaceButton.Size = new System.Drawing.Size(75, 23);
            this.spaceButton.TabIndex = 9;
            this.spaceButton.Text = "Space";
            this.spaceButton.UseVisualStyleBackColor = true;
            this.spaceButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // KeyboardWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 254);
            this.Controls.Add(this.spaceButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.backspaceButton);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCharactersImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "KeyboardWindow";
            this.Text = "Commodore 64 Emulator Extended Keyboard | Will Burland";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardWindow_FormClosing);
            this.Load += new System.EventHandler(this.KeyboardWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textCharactersImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox textCharactersImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.Button backspaceButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button spaceButton;
    }
}