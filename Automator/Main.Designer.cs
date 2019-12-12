namespace Automator
{
    partial class AutomatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutomatorForm));
            this.controlsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.loadBtn = new System.Windows.Forms.Button();
            this.menuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FunctionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presetsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFineLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLastSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDefaultProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.samplerFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseLoggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeoutLbl = new System.Windows.Forms.Label();
            this.toggleBtn = new System.Windows.Forms.Button();
            this.intervalSeconds = new System.Windows.Forms.NumericUpDown();
            this.instructions = new System.Windows.Forms.RichTextBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.mouseLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.controlsLayoutPanel.SuspendLayout();
            this.menuContext.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalSeconds)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlsLayoutPanel
            // 
            this.controlsLayoutPanel.BackColor = System.Drawing.Color.Teal;
            this.controlsLayoutPanel.ColumnCount = 4;
            this.controlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.controlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.99921F));
            this.controlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0008F));
            this.controlsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 197F));
            this.controlsLayoutPanel.Controls.Add(this.loadBtn, 0, 0);
            this.controlsLayoutPanel.Controls.Add(this.timeoutLbl, 1, 0);
            this.controlsLayoutPanel.Controls.Add(this.toggleBtn, 3, 0);
            this.controlsLayoutPanel.Controls.Add(this.intervalSeconds, 2, 0);
            this.controlsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlsLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.controlsLayoutPanel.Name = "controlsLayoutPanel";
            this.controlsLayoutPanel.RowCount = 1;
            this.controlsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.controlsLayoutPanel.Size = new System.Drawing.Size(681, 42);
            this.controlsLayoutPanel.TabIndex = 0;
            // 
            // loadBtn
            // 
            this.loadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadBtn.ContextMenuStrip = this.menuContext;
            this.loadBtn.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadBtn.Location = new System.Drawing.Point(3, 4);
            this.loadBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(74, 34);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "Menu";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // menuContext
            // 
            this.menuContext.BackColor = System.Drawing.Color.Teal;
            this.menuContext.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FunctionsMenuItem,
            this.presetsMenuItem,
            this.saveFileToolStripMenuItem,
            this.loadFileToolStripMenuItem,
            this.openFineLocation,
            this.loadLastSaveToolStripMenuItem,
            this.loadDefaultProgramToolStripMenuItem,
            this.samplerFormToolStripMenuItem,
            this.mouseLoggerToolStripMenuItem});
            this.menuContext.Name = "menuContext";
            this.menuContext.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuContext.ShowImageMargin = false;
            this.menuContext.Size = new System.Drawing.Size(184, 202);
            this.menuContext.Opening += new System.ComponentModel.CancelEventHandler(this.menuContext_Opening);
            // 
            // FunctionsMenuItem
            // 
            this.FunctionsMenuItem.ForeColor = System.Drawing.Color.White;
            this.FunctionsMenuItem.Name = "FunctionsMenuItem";
            this.FunctionsMenuItem.Size = new System.Drawing.Size(183, 22);
            this.FunctionsMenuItem.Text = "Added Functions";
            this.FunctionsMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FunctionsMenuItem_DropDownItemClicked);
            // 
            // presetsMenuItem
            // 
            this.presetsMenuItem.ForeColor = System.Drawing.Color.White;
            this.presetsMenuItem.Name = "presetsMenuItem";
            this.presetsMenuItem.Size = new System.Drawing.Size(183, 22);
            this.presetsMenuItem.Text = "Preset Syntaxes";
            this.presetsMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.presetsMenuItem_DropDownItemClicked);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.BackColor = System.Drawing.Color.MediumTurquoise;
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.BackColor = System.Drawing.Color.MediumTurquoise;
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.loadFileToolStripMenuItem_DropDownItemClicked);
            // 
            // openFineLocation
            // 
            this.openFineLocation.BackColor = System.Drawing.Color.MediumTurquoise;
            this.openFineLocation.Name = "openFineLocation";
            this.openFineLocation.Size = new System.Drawing.Size(183, 22);
            this.openFineLocation.Text = "Open file location";
            this.openFineLocation.Click += new System.EventHandler(this.openFineLocation_Click);
            // 
            // loadLastSaveToolStripMenuItem
            // 
            this.loadLastSaveToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.loadLastSaveToolStripMenuItem.Name = "loadLastSaveToolStripMenuItem";
            this.loadLastSaveToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadLastSaveToolStripMenuItem.Text = "Load Last Saved Run";
            this.loadLastSaveToolStripMenuItem.Click += new System.EventHandler(this.loadLastSaveToolStripMenuItem_Click);
            // 
            // loadDefaultProgramToolStripMenuItem
            // 
            this.loadDefaultProgramToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.loadDefaultProgramToolStripMenuItem.Name = "loadDefaultProgramToolStripMenuItem";
            this.loadDefaultProgramToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadDefaultProgramToolStripMenuItem.Text = "Reset to Default Code";
            this.loadDefaultProgramToolStripMenuItem.Click += new System.EventHandler(this.loadDefaultProgramToolStripMenuItem_Click);
            // 
            // samplerFormToolStripMenuItem
            // 
            this.samplerFormToolStripMenuItem.BackColor = System.Drawing.Color.Teal;
            this.samplerFormToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.samplerFormToolStripMenuItem.Name = "samplerFormToolStripMenuItem";
            this.samplerFormToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.samplerFormToolStripMenuItem.Text = "Timing Sampler";
            this.samplerFormToolStripMenuItem.Click += new System.EventHandler(this.samplerFormToolStripMenuItem_Click);
            // 
            // mouseLoggerToolStripMenuItem
            // 
            this.mouseLoggerToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.mouseLoggerToolStripMenuItem.Name = "mouseLoggerToolStripMenuItem";
            this.mouseLoggerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.mouseLoggerToolStripMenuItem.Text = "Mouse Logger";
            this.mouseLoggerToolStripMenuItem.Click += new System.EventHandler(this.mouseLoggerToolStripMenuItem_Click);
            // 
            // timeoutLbl
            // 
            this.timeoutLbl.AutoSize = true;
            this.timeoutLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeoutLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeoutLbl.ForeColor = System.Drawing.Color.White;
            this.timeoutLbl.Location = new System.Drawing.Point(83, 0);
            this.timeoutLbl.Name = "timeoutLbl";
            this.timeoutLbl.Size = new System.Drawing.Size(236, 42);
            this.timeoutLbl.TabIndex = 2;
            this.timeoutLbl.Text = "Number of seconds per refresh\r\n(0 = Only run once)";
            this.timeoutLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toggleBtn
            // 
            this.toggleBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleBtn.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleBtn.Location = new System.Drawing.Point(486, 4);
            this.toggleBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toggleBtn.Name = "toggleBtn";
            this.toggleBtn.Size = new System.Drawing.Size(192, 34);
            this.toggleBtn.TabIndex = 0;
            this.toggleBtn.Text = "Start Simulation";
            this.toggleBtn.UseVisualStyleBackColor = true;
            this.toggleBtn.Click += new System.EventHandler(this.toggleBtn_Click);
            // 
            // intervalSeconds
            // 
            this.intervalSeconds.BackColor = System.Drawing.Color.Teal;
            this.intervalSeconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.intervalSeconds.DecimalPlaces = 3;
            this.intervalSeconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.intervalSeconds.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intervalSeconds.ForeColor = System.Drawing.Color.White;
            this.intervalSeconds.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.intervalSeconds.Location = new System.Drawing.Point(325, 4);
            this.intervalSeconds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.intervalSeconds.Name = "intervalSeconds";
            this.intervalSeconds.Size = new System.Drawing.Size(155, 36);
            this.intervalSeconds.TabIndex = 3;
            this.intervalSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.intervalSeconds.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.intervalSeconds.ValueChanged += new System.EventHandler(this.intervalSeconds_ValueChanged);
            // 
            // instructions
            // 
            this.instructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instructions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.instructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.instructions.ForeColor = System.Drawing.Color.White;
            this.instructions.Location = new System.Drawing.Point(0, 42);
            this.instructions.Margin = new System.Windows.Forms.Padding(0);
            this.instructions.Name = "instructions";
            this.instructions.ShowSelectionMargin = true;
            this.instructions.Size = new System.Drawing.Size(681, 380);
            this.instructions.TabIndex = 1;
            this.instructions.Text = resources.GetString("instructions.Text");
            this.instructions.ZoomFactor = 1.5F;
            this.instructions.SelectionChanged += new System.EventHandler(this.instructions_SelectionChanged);
            this.instructions.TextChanged += new System.EventHandler(this.instructions_TextChanged);
            this.instructions.Enter += new System.EventHandler(this.instructions_Enter);
            this.instructions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.instructions_KeyPress);
            this.instructions.Leave += new System.EventHandler(this.instructions_Leave);
            // 
            // statusBar
            // 
            this.statusBar.BackColor = System.Drawing.Color.DarkCyan;
            this.statusBar.Font = new System.Drawing.Font("Century Gothic", 7F);
            this.statusBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseLbl,
            this.colorLbl,
            this.status});
            this.statusBar.Location = new System.Drawing.Point(0, 422);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(681, 22);
            this.statusBar.TabIndex = 2;
            // 
            // mouseLbl
            // 
            this.mouseLbl.Font = new System.Drawing.Font("Century Gothic", 7F, System.Drawing.FontStyle.Italic);
            this.mouseLbl.ForeColor = System.Drawing.Color.White;
            this.mouseLbl.Name = "mouseLbl";
            this.mouseLbl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.mouseLbl.Size = new System.Drawing.Size(70, 17);
            this.mouseLbl.Text = "Mouse: (0,0)";
            // 
            // colorLbl
            // 
            this.colorLbl.Font = new System.Drawing.Font("Century Gothic", 7F, System.Drawing.FontStyle.Italic);
            this.colorLbl.ForeColor = System.Drawing.Color.White;
            this.colorLbl.Name = "colorLbl";
            this.colorLbl.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.colorLbl.Size = new System.Drawing.Size(73, 17);
            this.colorLbl.Text = "Color: (0,0,0)";
            // 
            // status
            // 
            this.status.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Bold);
            this.status.ForeColor = System.Drawing.Color.White;
            this.status.Name = "status";
            this.status.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // AutomatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(681, 444);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.instructions);
            this.Controls.Add(this.controlsLayoutPanel);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AutomatorForm";
            this.Text = "Automator";
            this.Activated += new System.EventHandler(this.AutomatorForm_Activated);
            this.Deactivate += new System.EventHandler(this.AutomatorForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutomatorForm_FormClosing);
            this.Load += new System.EventHandler(this.AutomatorForm_Load);
            this.controlsLayoutPanel.ResumeLayout(false);
            this.controlsLayoutPanel.PerformLayout();
            this.menuContext.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intervalSeconds)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel controlsLayoutPanel;
        private System.Windows.Forms.Button toggleBtn;
        private System.Windows.Forms.RichTextBox instructions;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Label timeoutLbl;
        private System.Windows.Forms.NumericUpDown intervalSeconds;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ContextMenuStrip menuContext;
        private System.Windows.Forms.ToolStripMenuItem loadDefaultProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FunctionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLastSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem samplerFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel mouseLbl;
        private System.Windows.Forms.ToolStripStatusLabel colorLbl;
        private System.Windows.Forms.ToolStripMenuItem openFineLocation;
        private System.Windows.Forms.ToolStripMenuItem presetsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseLoggerToolStripMenuItem;
    }
}

