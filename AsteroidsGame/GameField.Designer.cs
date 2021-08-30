namespace AsteroidsGame
{
    partial class GameField
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
            this.components = new System.ComponentModel.Container();
            this._refreshTimer = new System.Windows.Forms.Timer(this.components);
            this._score = new System.Windows.Forms.Label();
            this._addEnemyTimer = new System.Windows.Forms.Timer(this.components);
            this._laserEnergyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _refreshTimer
            // 
            this._refreshTimer.Enabled = true;
            this._refreshTimer.Interval = 25;
            this._refreshTimer.Tick += new System.EventHandler(this._refreshTimer_Tick);
            // 
            // _score
            // 
            this._score.AutoSize = true;
            this._score.ForeColor = System.Drawing.SystemColors.ControlLight;
            this._score.Location = new System.Drawing.Point(13, 13);
            this._score.Name = "_score";
            this._score.Size = new System.Drawing.Size(47, 13);
            this._score.TabIndex = 0;
            this._score.Text = "Score: 0";
            // 
            // _addEnemyTimer
            // 
            this._addEnemyTimer.Enabled = true;
            this._addEnemyTimer.Interval = 1000;
            this._addEnemyTimer.Tick += new System.EventHandler(this._addEnemyTimer_Tick);
            // 
            // _laserEnergyLabel
            // 
            this._laserEnergyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._laserEnergyLabel.AutoSize = true;
            this._laserEnergyLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this._laserEnergyLabel.Location = new System.Drawing.Point(12, 428);
            this._laserEnergyLabel.Name = "_laserEnergyLabel";
            this._laserEnergyLabel.Size = new System.Drawing.Size(57, 13);
            this._laserEnergyLabel.TabIndex = 1;
            this._laserEnergyLabel.Text = "Laser: 100";
            // 
            // GameField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._laserEnergyLabel);
            this.Controls.Add(this._score);
            this.Name = "GameField";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GameField_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameField_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameField_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameField_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer _refreshTimer;
        private System.Windows.Forms.Label _score;
        private System.Windows.Forms.Timer _addEnemyTimer;
        public System.Windows.Forms.Label _laserEnergyLabel;
    }
}

