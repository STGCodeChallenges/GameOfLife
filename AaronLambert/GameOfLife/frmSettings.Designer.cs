namespace GameOfLife
{
    partial class frmSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnBegin = new System.Windows.Forms.Button();
            this.txtChanceOfLife = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtChanceOfLife)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Initial chance of life:";
            // 
            // btnBegin
            // 
            this.btnBegin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBegin.Location = new System.Drawing.Point(68, 119);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(75, 25);
            this.btnBegin.TabIndex = 2;
            this.btnBegin.Text = "Begin";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // txtChanceOfLife
            // 
            this.txtChanceOfLife.Location = new System.Drawing.Point(12, 29);
            this.txtChanceOfLife.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtChanceOfLife.Name = "txtChanceOfLife";
            this.txtChanceOfLife.Size = new System.Drawing.Size(120, 22);
            this.txtChanceOfLife.TabIndex = 0;
            this.txtChanceOfLife.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "100 ms between updates:";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(12, 91);
            this.txtInterval.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(120, 22);
            this.txtInterval.TabIndex = 1;
            this.txtInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 154);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtChanceOfLife);
            this.Controls.Add(this.btnBegin);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Shown += new System.EventHandler(this.frmSettings_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.txtChanceOfLife)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.NumericUpDown txtChanceOfLife;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtInterval;
    }
}