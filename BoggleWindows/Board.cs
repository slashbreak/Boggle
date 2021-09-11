using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoggleWindows
{

    public class Board
    {
        int j = 0;
        List<string> _wordlist = new List<string>();
        public Random random = new Random();
        static List<string> dice = new List<string>(){
            "AAEEGN", "ELRTTY", "AOOTTW", "ABBJOO",
            "EHRTVW", "CIMOTU", "DISTTY", "EIOSST",
            "DELRVY", "ACHOPS", "HIMNQU", "EEINSU",
            "EEGHNW", "AFFKPS", "HLNNRZ", "DEILRX" };
        //static List<string> dice = new List<string>(){
        //    "AAAAAA", "BBBBBB", "CCCCCC", "DDDDDD",
        //    "EEEEEE", "FFFFFF", "GGGGGG", "HHHHHH",
        //    "IIIIII", "JJJJJJ", "KKKKKK", "LLLLLL",
        //    "MMMMMM", "NNNNNN", "OOOOOO", "PPPPPP" };

        static int _size = 4;
        public char[,] _board;
        public bool[,] _checked;
        public Board(List<string> wordlist)
        {
            _wordlist = wordlist;
            _board = new char[4, 4];
            _checked = new bool[4, 4];
            ResetBoard();
        }
        public void PrintBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    
                    Console.Write(_board[i, j]);
                }
                Console.Write('\n');
            }
        }
        public void ResetVisitedBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _checked[i, j] = false;
                }

            }
        }
        public void ResetBoard()
        {
            int count = 0;
            List<string> diceCopy = new List<string>(dice);
            diceCopy.Shuffle();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    string tempDice = diceCopy[0];
                    diceCopy.RemoveAt(0);
                    _board[i, j] = tempDice[random.Next(0, 6)];
                    count++;
                }
            }
        }

        //populate current board's validword list
         public List<string> FindAllWords()
        {
            List<string> foundWords = new List<string>();
            foreach (string s in _wordlist)
            {
                if (FindWord(s, false))
                {
                    foundWords.Add(s);
                }
            }
            return foundWords;
        }
        // get word ready for search. find inital starting points on the board.
        public bool FindWord(string initialWord, bool showNegativeResults)
        {

            //boggle rules that words must be at least 3 letters long
            if(initialWord.Length <= 2)
            {
                return false;
            }
            // trim 'QU' into 'Q', for boggle rules
            string word;
            if (initialWord.Contains("qu"))
            {
                StringBuilder split = new StringBuilder(initialWord);
                split.Replace("qu", "q");
                word = split.ToString();

            }
            else
            {
                word = initialWord;
            }
            word = word.ToUpper();
            char first = word[0];

            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    if (_board[i, j] == first)
                    {
                        positions.Add(Tuple.Create(i, j));
                    }
                }
            }

            // doesn't work if there are multiple starting points for words
            bool foundWord = false;
            for (int i = 0; i < positions.Count; i++)
            {
                foundWord = Solve(word, positions[i].Item1, positions[i].Item2, 0);
                if (foundWord)
                {
                    return true;
                }
            }
            return false;
        }


        // assumes the word to find is a valid dictionary word
        private bool Solve(string word, int offsetX, int offsetY, int index)
        {

            if (word.Length <= 1) return true;
            if (index >= word.Length) return true; // we've already found the whole word. no need to go deeper
            if (offsetX < 0 || offsetX >= _size || offsetY < 0 || offsetY >= _size) return false;
            if (_checked[offsetX, offsetY] == true) return false;
            if (_board[offsetX, offsetY] != word[index]) return false;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        _checked[offsetX, offsetY] = true;
                        bool b = Solve(word, offsetX + i, offsetY + j, index + 1);
                        _checked[offsetX, offsetY] = false;
                        if (b) return true;
                    }
                }
            }

            return false;
        }
    }
    public static class Extensions
    {
        static Random random = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
