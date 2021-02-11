
namespace Commodore_64_Emulator
{
    partial class DebugWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugWindow));
            this.frameRateLabel = new System.Windows.Forms.Label();
            this.frameTimeLabel = new System.Windows.Forms.Label();
            this.frameRateValue = new System.Windows.Forms.Label();
            this.frameTimeValue = new System.Windows.Forms.Label();
            this.cpuUsageLabel = new System.Windows.Forms.Label();
            this.cpuUsageValue = new System.Windows.Forms.Label();
            this.ramUsageLabel = new System.Windows.Forms.Label();
            this.ramUsageValue = new System.Windows.Forms.Label();
            this.programRAMTotalLabel = new System.Windows.Forms.Label();
            this.programRAMTotalValue = new System.Windows.Forms.Label();
            this.programRAMFreeLabel = new System.Windows.Forms.Label();
            this.programRAMFreeValue = new System.Windows.Forms.Label();
            this.programRAMUsedLabel = new System.Windows.Forms.Label();
            this.programRAMUsedValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // frameRateLabel
            // 
            this.frameRateLabel.AutoSize = true;
            this.frameRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameRateLabel.Location = new System.Drawing.Point(12, 10);
            this.frameRateLabel.Name = "frameRateLabel";
            this.frameRateLabel.Size = new System.Drawing.Size(144, 20);
            this.frameRateLabel.TabIndex = 0;
            this.frameRateLabel.Text = "Current Framerate:";
            // 
            // frameTimeLabel
            // 
            this.frameTimeLabel.AutoSize = true;
            this.frameTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameTimeLabel.Location = new System.Drawing.Point(24, 40);
            this.frameTimeLabel.Name = "frameTimeLabel";
            this.frameTimeLabel.Size = new System.Drawing.Size(132, 20);
            this.frameTimeLabel.TabIndex = 1;
            this.frameTimeLabel.Text = "Last Frame Time:";
            // 
            // frameRateValue
            // 
            this.frameRateValue.AutoSize = true;
            this.frameRateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameRateValue.Location = new System.Drawing.Point(162, 10);
            this.frameRateValue.Name = "frameRateValue";
            this.frameRateValue.Size = new System.Drawing.Size(53, 20);
            this.frameRateValue.TabIndex = 2;
            this.frameRateValue.Text = "0 FPS";
            // 
            // frameTimeValue
            // 
            this.frameTimeValue.AutoSize = true;
            this.frameTimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameTimeValue.Location = new System.Drawing.Point(162, 40);
            this.frameTimeValue.Name = "frameTimeValue";
            this.frameTimeValue.Size = new System.Drawing.Size(43, 20);
            this.frameTimeValue.TabIndex = 3;
            this.frameTimeValue.Text = "0 ms";
            // 
            // cpuUsageLabel
            // 
            this.cpuUsageLabel.AutoSize = true;
            this.cpuUsageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuUsageLabel.Location = new System.Drawing.Point(59, 70);
            this.cpuUsageLabel.Name = "cpuUsageLabel";
            this.cpuUsageLabel.Size = new System.Drawing.Size(97, 20);
            this.cpuUsageLabel.TabIndex = 4;
            this.cpuUsageLabel.Text = "CPU Usage:";
            // 
            // cpuUsageValue
            // 
            this.cpuUsageValue.AutoSize = true;
            this.cpuUsageValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuUsageValue.Location = new System.Drawing.Point(162, 70);
            this.cpuUsageValue.Name = "cpuUsageValue";
            this.cpuUsageValue.Size = new System.Drawing.Size(36, 20);
            this.cpuUsageValue.TabIndex = 5;
            this.cpuUsageValue.Text = "0 %";
            // 
            // ramUsageLabel
            // 
            this.ramUsageLabel.AutoSize = true;
            this.ramUsageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramUsageLabel.Location = new System.Drawing.Point(56, 100);
            this.ramUsageLabel.Name = "ramUsageLabel";
            this.ramUsageLabel.Size = new System.Drawing.Size(100, 20);
            this.ramUsageLabel.TabIndex = 6;
            this.ramUsageLabel.Text = "RAM Usage:";
            // 
            // ramUsageValue
            // 
            this.ramUsageValue.AutoSize = true;
            this.ramUsageValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ramUsageValue.Location = new System.Drawing.Point(162, 100);
            this.ramUsageValue.Name = "ramUsageValue";
            this.ramUsageValue.Size = new System.Drawing.Size(46, 20);
            this.ramUsageValue.TabIndex = 7;
            this.ramUsageValue.Text = "0 MB";
            // 
            // programRAMTotalLabel
            // 
            this.programRAMTotalLabel.AutoSize = true;
            this.programRAMTotalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programRAMTotalLabel.Location = new System.Drawing.Point(4, 150);
            this.programRAMTotalLabel.Name = "programRAMTotalLabel";
            this.programRAMTotalLabel.Size = new System.Drawing.Size(152, 20);
            this.programRAMTotalLabel.TabIndex = 8;
            this.programRAMTotalLabel.Text = "Program RAM Total:";
            // 
            // programRAMTotalValue
            // 
            this.programRAMTotalValue.AutoSize = true;
            this.programRAMTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programRAMTotalValue.Location = new System.Drawing.Point(162, 150);
            this.programRAMTotalValue.Name = "programRAMTotalValue";
            this.programRAMTotalValue.Size = new System.Drawing.Size(62, 20);
            this.programRAMTotalValue.TabIndex = 9;
            this.programRAMTotalValue.Text = "0 Bytes";
            // 
            // programRAMFreeLabel
            // 
            this.programRAMFreeLabel.AutoSize = true;
            this.programRAMFreeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programRAMFreeLabel.Location = new System.Drawing.Point(6, 180);
            this.programRAMFreeLabel.Name = "programRAMFreeLabel";
            this.programRAMFreeLabel.Size = new System.Drawing.Size(150, 20);
            this.programRAMFreeLabel.TabIndex = 10;
            this.programRAMFreeLabel.Text = "Program RAM Free:";
            // 
            // programRAMFreeValue
            // 
            this.programRAMFreeValue.AutoSize = true;
            this.programRAMFreeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programRAMFreeValue.Location = new System.Drawing.Point(162, 180);
            this.programRAMFreeValue.Name = "programRAMFreeValue";
            this.programRAMFreeValue.Size = new System.Drawing.Size(62, 20);
            this.programRAMFreeValue.TabIndex = 11;
            this.programRAMFreeValue.Text = "0 Bytes";
            // 
            // programRAMUsedLabel
            // 
            this.programRAMUsedLabel.AutoSize = true;
            this.programRAMUsedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programRAMUsedLabel.Location = new System.Drawing.Point(1, 210);
            this.programRAMUsedLabel.Name = "programRAMUsedLabel";
            this.programRAMUsedLabel.Size = new System.Drawing.Size(155, 20);
            this.programRAMUsedLabel.TabIndex = 12;
            this.programRAMUsedLabel.Text = "Program RAM Used:";
            // 
            // programRAMUsedValue
            // 
            this.programRAMUsedValue.AutoSize = true;
            this.programRAMUsedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.programRAMUsedValue.Location = new System.Drawing.Point(162, 210);
            this.programRAMUsedValue.Name = "programRAMUsedValue";
            this.programRAMUsedValue.Size = new System.Drawing.Size(62, 20);
            this.programRAMUsedValue.TabIndex = 13;
            this.programRAMUsedValue.Text = "0 Bytes";
            // 
            // DebugWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 252);
            this.Controls.Add(this.programRAMUsedValue);
            this.Controls.Add(this.programRAMUsedLabel);
            this.Controls.Add(this.programRAMFreeValue);
            this.Controls.Add(this.programRAMFreeLabel);
            this.Controls.Add(this.programRAMTotalValue);
            this.Controls.Add(this.programRAMTotalLabel);
            this.Controls.Add(this.ramUsageValue);
            this.Controls.Add(this.ramUsageLabel);
            this.Controls.Add(this.cpuUsageValue);
            this.Controls.Add(this.cpuUsageLabel);
            this.Controls.Add(this.frameTimeValue);
            this.Controls.Add(this.frameRateValue);
            this.Controls.Add(this.frameTimeLabel);
            this.Controls.Add(this.frameRateLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DebugWindow";
            this.Text = "Commodore 64 Emulator Debugging | Will Burland";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugWindow_FormClosing);
            this.Load += new System.EventHandler(this.DebugWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label frameRateLabel;
        private System.Windows.Forms.Label frameTimeLabel;
        private System.Windows.Forms.Label frameRateValue;
        private System.Windows.Forms.Label frameTimeValue;
        private System.Windows.Forms.Label cpuUsageLabel;
        private System.Windows.Forms.Label cpuUsageValue;
        private System.Windows.Forms.Label ramUsageLabel;
        private System.Windows.Forms.Label ramUsageValue;
        private System.Windows.Forms.Label programRAMTotalLabel;
        private System.Windows.Forms.Label programRAMTotalValue;
        private System.Windows.Forms.Label programRAMFreeLabel;
        private System.Windows.Forms.Label programRAMFreeValue;
        private System.Windows.Forms.Label programRAMUsedLabel;
        private System.Windows.Forms.Label programRAMUsedValue;
    }
}