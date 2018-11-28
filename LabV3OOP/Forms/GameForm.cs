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
using LabV3OOPData;

namespace LabV3OOP
{
    public partial class GameForm : Form
    {
        Tile selected1 = null;
        Tile selected2 = null;

        public GameForm()
        {
            InitializeComponent();
            List<int> values = readFile();
            Table.Controls.Clear();
            Table.Controls.Add(PlayingField.PlayingFieldInstance.CreateTable(values[2], values[0], values[1], reaction));
        }

        private void reaction(object sender, EventArgs e)
        {
            var b = sender as Tile;
            if (b != null)
            {
                if ( selected2 != null )
                    return;
                b.swap();
                if (b.Empty) { }
                else if (selected1 == null)
                {
                    selected1 = b;
                    selected1.RemoveAction();
                }
                else if (selected1 != null)
                {
                    selected2 = b;
                    CheckCards();
                }
            }
            GameWonCheck();
        }

        private void CheckCards()
        {
            if (!selected2.IsEqual(selected1))
            {
                timer1 = new Timer();
                timer1.Tick += Tick;
                timer1.Interval = 700;
                timer1.Start();
            }
            else
            {
                selected1.Matched = true;
                selected2.Matched = true;
                selected2 = null;
                selected1 = null;
            }
        }

        private void GameWonCheck()
        {
            if (PlayingField.PlayingFieldInstance.AllMatched)
            {
                GameWonForm gwf = new GameWonForm();
                gwf.ShowDialog();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Tick(object sender, EventArgs e)
        {
            selected1.SetAction();
            selected1.swapBack();
            selected2.swapBack();
            selected1 = null;
            selected2 = null;
            timer1.Stop();
        }

        private List<int> readFile()
        {
            List<int> setValues = new List<int>(4);
            using(StreamReader file = new StreamReader("../../../data/info.txt"))
            {
                setValues.Add(int.Parse(file.ReadLine()));
                setValues.Add(int.Parse(file.ReadLine()));
                setValues.Add(int.Parse(file.ReadLine()));
                setValues.Add(int.Parse(file.ReadLine()));
            }
            return setValues;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            DialogResult dr = sf.ShowDialog();
            if (dr == DialogResult.OK)
                Application.Restart();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            PlayingField.PlayingFieldInstance.RevealAll();
            DialogResult dr = MessageBox.Show("You lost","Game over",MessageBoxButtons.RetryCancel);
            if (dr == DialogResult.Retry)
                Application.Restart();
            else if (dr == DialogResult.Cancel)
                Application.Exit();
        }
    }
}
