using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public GameForm(int rows,int columns, int pairs)
        {
            InitializeComponent();
            
            Table.Controls.Clear();
            Table.Controls.Add(PlayingField.PlayingFieldInstance.CreateTable(pairs, rows, columns, reaction));
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

    }
}
