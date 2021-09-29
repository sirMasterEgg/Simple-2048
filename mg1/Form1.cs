using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mg1
{
    public partial class Form1 : Form
    {
        List<List<Button>> myBtn = new List<List<Button>>();
        List<Player> p = new List<Player>();

        private int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox3.Enabled = false;
            comboBox1.Text = "Easy";
            List<Button> firstBtn = new List<Button>();
            List<Button> secBtn = new List<Button>();
            List<Button> thirdBtn = new List<Button>();
            List<Button> fourthBtn = new List<Button>();

            firstBtn.Add(button0);
            firstBtn.Add(button1);
            firstBtn.Add(button2);
            firstBtn.Add(button3);

            myBtn.Add(firstBtn);

            secBtn.Add(button4);
            secBtn.Add(button5);
            secBtn.Add(button6);
            secBtn.Add(button7);
            
            myBtn.Add(secBtn);
            
            thirdBtn.Add(button8);
            thirdBtn.Add(button9);
            thirdBtn.Add(button10);
            thirdBtn.Add(button11);
            
            myBtn.Add(thirdBtn);
            
            fourthBtn.Add(button12);
            fourthBtn.Add(button13);
            fourthBtn.Add(button14);
            fourthBtn.Add(button15);

            myBtn.Add(fourthBtn);

            foreach (var item in myBtn)
            {
                foreach (var i in item)
                {
                    i.Font = new Font("Arial", 15);
                }
            }

            clearTile();

        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "")
            {
                groupBox1.Enabled = false;
                groupBox3.Enabled = true;
                generateNumber();
            }
            else
            {
                MessageBox.Show("Isi nama dan level dulu!");
            }
        }

        private void updateLeaderboard()
        {
            for (int i = 0; i < p.Count-1; i++)
            {
                for (int j = i+1; j < p.Count; j++)
                {
                    if (p[i].Score < p[j].Score)
                    { 
                        Player temp = p[i];
                        p[i] = p[j];
                        p[j]= temp;
                    }
                }
            }
            listBox1.DataSource = null;
            listBox1.DataSource = p;
        }

        private void clearTile()
        {
            foreach (var item in myBtn)
            {
                foreach (var i in item)
                {
                    i.Text = "";
                    i.BackColor = SystemColors.Control;
                }
            }
        }

        private void reset()
        {
            clearTile();
            groupBox1.Enabled = true;
            groupBox3.Enabled = false;
            textBox1.Text = "";
            comboBox1.Text = "Easy";
            label4.Text = "Score : 0";
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            reset();   
        }

        private bool cekMenangEzMed()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (myBtn[i][j].Text == "2048")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool isPenuhMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (myBtn[i][j].Text == "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void generateNumber()
        {
            Random r = new Random();
            int nilai = r.Next() % 2 == 0 ? 2 : 4;

            while (true)
            {
                int x = r.Next(0, 4);
                int y = r.Next(0, 4);
                if (myBtn[y][x].Text == "")
                {
                    myBtn[y][x].Text = nilai.ToString();
                    warnaLevel(comboBox1.Text, myBtn[y][x]);
                    break;
                }
                if (isPenuhMap())
                {
                    break;
                }
            }
        }

        private void warnaLevel(String level, Button b)
        {
            if (level == "Easy")
            {
                bikinWarnaEasy(b);
            }
            else if (level == "Medium")
            {
                bikinWarnaMed(b);
            }
        }

        private void bikinWarnaEasy(Button b)
        {
                        //    1    2    3    4    5    6    7    8    9   10   11
            int[] red =   { 255, 255, 255, 255, 255, 255, 255, 255, 128, 255, 255 };
            int[] green = { 255, 255, 255, 255, 255, 255, 153, 153, 255, 159, 128 };
            int[] blue =  { 204, 179, 153, 128, 102,   0, 255,  51, 128, 128, 128 };

            int id = 0;
            for (int i = 2; i <= 2048; i*=2)
            {
                if (b.Text == i.ToString())
                {
                    b.BackColor = Color.FromArgb(red[id], green[id], blue[id]);
                }
                id++;
            }

        }

        private void bikinWarnaMed(Button b)
        {
            b.BackColor = Color.FromArgb(255, 255, 0);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (groupBox3.Enabled)
            {
                if (e.KeyCode == Keys.W)
                {
                    gerakW();
                }
                if (e.KeyCode == Keys.S)
                {
                    gerakS();
                }
                if (e.KeyCode == Keys.A)
                {
                    gerakA();
                }
                if (e.KeyCode == Keys.D)
                {
                    gerakD();
                }
            }
        }

        private void buttonW_Click(object sender, EventArgs e)
        {
            gerakW();
        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            gerakA();
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            gerakS();
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            gerakD();
        }

        private void clearOneTile(Button b)
        {
            b.Text = "";
            b.BackColor = SystemColors.Control;
        }

        private void gerakW()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (myBtn[i][j].Text != "")
                    {
                        int idx = i;
                        do
                        {
                            if (myBtn[idx - 1][j].Text == "")
                            {
                                myBtn[idx - 1][j].Text = myBtn[idx][j].Text;
                                myBtn[idx - 1][j].BackColor = myBtn[idx][j].BackColor;
                                clearOneTile(myBtn[idx][j]);
                                idx--;
                            }
                            else
                            {
                                if (myBtn[idx - 1][j].Text != myBtn[idx][j].Text)
                                {
                                    break;
                                }
                                else
                                {
                                    int temppp = Convert.ToInt32(myBtn[idx - 1][j].Text) * 2;
                                    myBtn[idx - 1][j].Text = temppp.ToString();
                                    warnaLevel(comboBox1.Text, myBtn[idx - 1][j]);
                                    clearOneTile(myBtn[idx][j]);
                                    idx--;
                                }
                            }
                        } while (idx != 0);
                    }
                }
            }
            generateNumber();
            hitungScore();

            if (comboBox1.Text != "Hard") 
            {
                if (cekMenangEzMed())
                {
                    MessageBox.Show("Menang! Score anda: "+score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
                else if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: "+score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
            else
            {
                if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: "+score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
        }

        private void gerakS()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (myBtn[i][j].Text != "")
                    {
                        int idx = i;
                        do
                        {
                            if (myBtn[idx + 1][j].Text == "")
                            {             
                                myBtn[idx + 1][j].Text = myBtn[idx][j].Text;
                                myBtn[idx + 1][j].BackColor = myBtn[idx][j].BackColor;
                                clearOneTile(myBtn[idx][j]);
                                idx++;
                            }
                            else
                            {
                                if (myBtn[idx + 1][j].Text != myBtn[idx][j].Text)
                                {
                                    break;
                                }
                                else
                                {
                                    int temppp = Convert.ToInt32(myBtn[idx + 1][j].Text) * 2;
                                    myBtn[idx + 1][j].Text = temppp.ToString();
                                    warnaLevel(comboBox1.Text, myBtn[idx + 1][j]);
                                    clearOneTile(myBtn[idx][j]);
                                    idx++;
                                }
                            }
                        } while (idx != 3);
                    }
                }
            }
            generateNumber();
            hitungScore();

            if (comboBox1.Text != "Hard")
            {
                if (cekMenangEzMed())
                {
                    MessageBox.Show("Menang! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
                else if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
            else
            {
                if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
        }

        private void gerakA()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if (myBtn[i][j].Text != "")
                    {
                        int idx = j;
                        do
                        {
                            if (myBtn[i][idx - 1].Text == "") 
                            {
                                myBtn[i][idx - 1].Text = myBtn[i][idx].Text;
                                myBtn[i][idx - 1].BackColor = myBtn[i][idx].BackColor;
                                clearOneTile(myBtn[i][idx]);
                                idx--;
                            }
                            else
                            {
                                if (myBtn[i][idx - 1].Text != myBtn[i][idx].Text) 
                                {
                                    break;
                                }
                                else
                                {
                                    int temppp = Convert.ToInt32(myBtn[i][idx - 1].Text) * 2;
                                    myBtn[i][idx - 1].Text = temppp.ToString();
                                    warnaLevel(comboBox1.Text, myBtn[i][idx - 1]);
                                    clearOneTile(myBtn[i][idx]);
                                    idx--;
                                }
                            }
                        } while (idx != 0);
                    }
                }
            }
            generateNumber();
            hitungScore();

            if (comboBox1.Text != "Hard")
            {
                if (cekMenangEzMed())
                {
                    MessageBox.Show("Menang! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
                else if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
            else
            {
                if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
        }

        private void gerakD()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (myBtn[i][j].Text != "")
                    {
                        int idx = j;
                        do
                        {
                            if (myBtn[i][idx + 1].Text == "")
                            {                
                                myBtn[i][idx + 1].Text = myBtn[i][idx].Text;
                                myBtn[i][idx + 1].BackColor = myBtn[i][idx].BackColor;
                                clearOneTile(myBtn[i][idx]);
                                idx++;
                            }
                            else
                            {
                                if (myBtn[i][idx + 1].Text != myBtn[i][idx].Text)
                                {
                                    break;
                                }
                                else
                                {
                                    int temppp = Convert.ToInt32(myBtn[i][idx + 1].Text) * 2;
                                    myBtn[i][idx + 1].Text = temppp.ToString();
                                    warnaLevel(comboBox1.Text, myBtn[i][idx + 1]);
                                    clearOneTile(myBtn[i][idx]);
                                    idx++;
                                }
                            }
                        } while (idx != 3);
                    }
                }
            }
            generateNumber();
            hitungScore();

            if (comboBox1.Text != "Hard")
            {
                if (cekMenangEzMed())
                {
                    MessageBox.Show("Menang! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
                else if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
            else
            {
                if (isPenuhMap())
                {
                    MessageBox.Show("Kalah! Score anda: " + score);
                    p.Add(new Player(textBox1.Text, comboBox1.Text, score));
                    updateLeaderboard();
                    reset();
                }
            }
        }

        private void hitungScore()
        {
            score = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (myBtn[i][j].Text != "") 
                    {
                        score += Convert.ToInt32(myBtn[i][j].Text);
                    }
                }
            }
            label4.Text = "Score : " + score;
        }

    }
}
