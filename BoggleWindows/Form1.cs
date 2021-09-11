using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleWindows
{
    public partial class Form1 : Form
    {
        Game game;
        public List<string> _userWords;
        public Form1()
        {
            InitializeComponent();
            //this.Show();
            //this.Load += new EventHandler(this.Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            _userWords = new List<string>();
            game = new Game();
            #region set board labels
            label1.Text = game._board._board[0, 0].ToString();
            label2.Text = game._board._board[0, 1].ToString();
            label3.Text = game._board._board[0, 2].ToString();
            label4.Text = game._board._board[0, 3].ToString();
            label5.Text = game._board._board[1, 0].ToString();
            label6.Text = game._board._board[1, 1].ToString();
            label7.Text = game._board._board[1, 2].ToString();
            label8.Text = game._board._board[1, 3].ToString();
            label9.Text = game._board._board[2, 0].ToString();
            label10.Text = game._board._board[2, 1].ToString();
            label11.Text = game._board._board[2, 2].ToString();
            label12.Text = game._board._board[2, 3].ToString();
            label13.Text = game._board._board[3, 0].ToString();
            label14.Text = game._board._board[3, 1].ToString();
            label15.Text = game._board._board[3, 2].ToString();
            label16.Text = game._board._board[3, 3].ToString();
            #endregion
            foreach (string s in game._validWords)
            {
                textBoxCheat.AppendText(s);
                textBoxCheat.AppendText(Environment.NewLine);
            }
            foreach (string s in game._longestWords)
            {
                textBoxLongestWords.AppendText(s);
                textBoxLongestWords.AppendText(Environment.NewLine);
            }
            textBoxLongestWords.Hide();
            textBoxCheat.Hide();
            progressBar1.Maximum = game._validWords.Count;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            game.NewGame();
            _userWords.Clear();
            #region set board labels
            label1.Text = game._board._board[0, 0].ToString();
            label2.Text = game._board._board[0, 1].ToString();
            label3.Text = game._board._board[0, 2].ToString();
            label4.Text = game._board._board[0, 3].ToString();
            label5.Text = game._board._board[1, 0].ToString();
            label6.Text = game._board._board[1, 1].ToString();
            label7.Text = game._board._board[1, 2].ToString();
            label8.Text = game._board._board[1, 3].ToString();
            label9.Text = game._board._board[2, 0].ToString();
            label10.Text = game._board._board[2, 1].ToString();
            label11.Text = game._board._board[2, 2].ToString();
            label12.Text = game._board._board[2, 3].ToString();
            label13.Text = game._board._board[3, 0].ToString();
            label14.Text = game._board._board[3, 1].ToString();
            label15.Text = game._board._board[3, 2].ToString();
            label16.Text = game._board._board[3, 3].ToString();
            #endregion
            textBoxCheat.Text = "";
            richTextBox2.Text = "";
            labelScore.Text = $"Score: {game.Score}";
            textBoxLongestWords.Text = "";
            foreach (string s in game._validWords)
            {
                textBoxCheat.AppendText(s);
                textBoxCheat.AppendText(Environment.NewLine);

            }
            foreach (string s in game._longestWords)
            {
                textBoxLongestWords.AppendText(s);
                textBoxLongestWords.AppendText(Environment.NewLine);
            }
            textBoxLongestWords.Hide();
            textBoxCheat.Hide();
            progressBar1.Value = 0;
            progressBar1.Maximum = game._validWords.Count;
        }
        

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //MessageBox.Show("ok");// buttonTest_Click(this, new EventArgs());
                bool correct = false;
                string userWord = textBox1.Text;
                foreach (string s in game._validWords)
                {
                    if (userWord.ToLower() == s && !(_userWords.Contains(userWord)))
                    {
                        correct = true;
                        _userWords.Add(userWord);
                        richTextBox2.AppendText(userWord);
                        richTextBox2.AppendText(Environment.NewLine);
                    }
                }
                if (correct)
                {
                    textBox1.Text = "";
                    game.Score += game.CalculateWordScore(userWord.Length);
                    labelScore.Text = $"Score: {game.Score}";
                    textBox1.BackColor = Color.Green;
                    progressBar1.Value += 1;
                }
                else
                {
                    textBox1.BackColor = Color.Red;
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            // 
        }

        private void buttonReveal_Click(object sender, EventArgs e)
        {
            textBoxCheat.Show();
            textBoxLongestWords.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }
    }
}

