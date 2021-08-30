using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidsGame
{
    public partial class GameField : Form
    {
        private GameManager _gameManager;
        private TextBox _gameOver;
        public bool TexturesOn { set; get; }
        public GameField()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            _gameOver = null;
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            InitializeGameOverTextBox();
           TexturesOn = false;
            NewGame();
        }

        private void InitializeGameOverTextBox()
        {
            _gameOver = new TextBox();
            _gameOver.Size = this.ClientSize;
            _gameOver.TextAlign = HorizontalAlignment.Center;
            _gameOver.BackColor = Color.Empty;
            _gameOver.ForeColor = Color.White;
            _gameOver.Font = new Font("Times", 48);
            _gameOver.Multiline = true;
            _gameOver.Enabled = false;           
        }
        public void UpdateScore(int score)
        {
            _score.Text = "Score: " + score.ToString();
        }

        public void NewGame()
        {
            this.Controls.Remove(_gameOver);
            _score.Text = "Score: 0";
            _gameManager = new GameManager(this);
            _gameManager.CreateEnemy();
            _refreshTimer.Start();
            _addEnemyTimer.Start();
        }

        public void GameOver()
        {
            _refreshTimer.Stop();
            _addEnemyTimer.Stop();
            _gameOver.Text = "Game Over" + Environment.NewLine + _score.Text + Environment.NewLine + Environment.NewLine + "Press R to restart";
            this.Controls.Add(_gameOver);

        }

        private void _refreshTimer_Tick(object sender, EventArgs e)
        {           
            Refresh();
            _gameManager.UpdateField();
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            _gameManager.KeyDown(e);
        }

        private void GameField_KeyUp(object sender, KeyEventArgs e)
        {
            _gameManager.KeyUp(e);
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            _gameManager.DrawAll(e.Graphics);
        }

        private void _addEnemyTimer_Tick(object sender, EventArgs e)
        {
                _gameManager.CreateEnemy();
        }
    }
}
