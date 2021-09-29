using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mg1
{
    class Player
    {
        private string nama;
        private string level;
        private int score;

        public Player(string nama, string level, int score)
        {
            this.Nama = nama;
            this.Level = level;
            this.Score = score;
        }

        public string Nama { get => nama; set => nama = value; }
        public string Level { get => level; set => level = value; }
        public int Score { get => score; set => score = value; }

        public override string ToString()
        {
            return $"{nama} - {level} - {score}";
        }
    }
}
