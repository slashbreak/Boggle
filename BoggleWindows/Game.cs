using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleWindows
{
    public class Game
    {
        static List<string> _wordList = new List<string>(File.ReadAllLines("enable1.txt"));
        public Board _board;
        public List<string> _validWords = new List<string>();
        public int Score { get; set; }
        public List<string> _longestWords = new List<string>();
        public Game()
        {  
            _board = new Board(_wordList);
            _validWords = _board.FindAllWords();
            Score = 0;
            _longestWords = FindLongestWords();
        }

        public void NewGame()
        {
            _board.ResetBoard();
            _validWords = _board.FindAllWords();
            Score = 0;
            _longestWords = FindLongestWords();
        }

        private List<string> FindLongestWords()
        {
            var words = _validWords.OrderBy(s => s.Length );
            int length = words.Last().Length;
            List<string> temp = new List<string>();
            foreach (string str in _validWords)
            {
                if (str.Length == length)
                {
                    temp.Add(str);
                }
            }
            return temp;
        }
        
        public int CalculateWordScore(int len)
        {
            int score = 0;
            if(len >= 8)
            {
                score = 11;
            }
            else if (len == 7)
            {
                score = 5;
            }
            else if (len == 6)
            {
                score = 3;
            }
            else if (score == 5)
            {
                score = 2;
            }
            else
            {
                score = 1;
            }
            return score;
        }
    }
}
