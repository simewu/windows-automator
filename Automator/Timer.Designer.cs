namespace Automator
{
    partial class Timer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Timer));
            this.clickBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clickBtn
            // 
            this.clickBtn.BackColor = System.Drawing.Color.Teal;
            this.clickBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clickBtn.FlatAppearance.BorderSize = 0;
            this.clickBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.clickBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.clickBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clickBtn.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clickBtn.ForeColor = System.Drawing.Color.White;
            this.clickBtn.Location = new System.Drawing.Point(0, 0);
            this.clickBtn.Name = "clickBtn";
            this.clickBtn.Size = new System.Drawing.Size(234, 91);
            this.clickBtn.TabIndex = 0;
            this.clickBtn.Text = "Timer";
            this.clickBtn.UseVisualStyleBackColor = false;
            this.clickBtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clickBtn_KeyDown);
            this.clickBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.clickBtn_MouseDown);
            // 
            // Timer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(234, 91);
            this.Controls.Add(this.clickBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Timer";
            this.Text = "Time Sampler";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button clickBtn;
    }
}