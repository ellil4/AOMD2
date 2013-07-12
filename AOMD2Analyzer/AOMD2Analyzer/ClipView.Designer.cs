namespace AOMD2Analyzer
{
    partial class ClipView
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
            this.amBtnApply = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.amTBFrom = new System.Windows.Forms.TextBox();
            this.amTBTo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // amBtnApply
            // 
            this.amBtnApply.Location = new System.Drawing.Point(212, 42);
            this.amBtnApply.Name = "amBtnApply";
            this.amBtnApply.Size = new System.Drawing.Size(75, 23);
            this.amBtnApply.TabIndex = 0;
            this.amBtnApply.Text = "Apply";
            this.amBtnApply.UseVisualStyleBackColor = true;
            this.amBtnApply.Click += new System.EventHandler(this.amBtnApply_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "To";
            // 
            // amTBFrom
            // 
            this.amTBFrom.Location = new System.Drawing.Point(65, 15);
            this.amTBFrom.Name = "amTBFrom";
            this.amTBFrom.Size = new System.Drawing.Size(100, 21);
            this.amTBFrom.TabIndex = 3;
            // 
            // amTBTo
            // 
            this.amTBTo.Location = new System.Drawing.Point(65, 44);
            this.amTBTo.Name = "amTBTo";
            this.amTBTo.Size = new System.Drawing.Size(100, 21);
            this.amTBTo.TabIndex = 4;
            // 
            // ClipView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 75);
            this.Controls.Add(this.amTBTo);
            this.Controls.Add(this.amTBFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.amBtnApply);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClipView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clip Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button amBtnApply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox amTBFrom;
        private System.Windows.Forms.TextBox amTBTo;
    }
}