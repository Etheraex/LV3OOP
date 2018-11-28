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
        #region Atributes
        Tile selected1 = null;
        Tile selected2 = null;
        private int _cellHeight;
        private int _cellWidth;
        private int seconds = 0;
        private int minutes = 0;
        #endregion

        #region Init
        public GameForm()
        {
            InitializeComponent();
            timer2 = new Timer();
            timer2.Interval = 1000;
            timer2.Tick += Tick2;
            timer2.Start();
            Table.Controls.Clear();
            Dimensions();
            int rows;
            int columns;
            int pairs;
            int imageCount;
            readFile(out rows, out columns, out pairs, out imageCount);
            Table.Controls.Add(PlayingField.PlayingFieldInstance.CreateTable(rows, columns, pairs * 2, imageCount, tile_reaction, _cellHeight, _cellWidth));
        }
        #endregion

        #region Additional Methods

        private void Dimensions()
        {
            int rows;
            int columns;
            int pairs;
            int images;
            readFile(out rows, out columns, out pairs, out images);
            for (int i = 0; i < columns; i++)
                Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columns));
            for (int i = 0; i < rows; i++)
                Table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / rows));
            _cellWidth = Table.GetColumnWidths()[0];
            _cellHeight = Table.GetRowHeights()[0];
        }

        private void CheckCards()
        {
            if (!selected2.IsEqual(selected1))
            {
                timer1 = new Timer();
                timer1.Tick += Tick1;
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
                PlayingField.PlayingFieldInstance.FlipEmpty();
                GameWonForm gwf = new GameWonForm();
                gwf.ShowDialog();
            }
        }

        private void readFile(out int rows,out int columns,out int pairs,out int images)
        {
            using(StreamReader file = new StreamReader("../../../data/info.txt"))
            {
                rows = int.Parse(file.ReadLine());
                columns = int.Parse(file.ReadLine());
                pairs = int.Parse(file.ReadLine());
                images = int.Parse(file.ReadLine());
            }
        }

        #endregion

        #region Events

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Tick1(object sender, EventArgs e)
        {
            selected1.SetAction();
            selected1.swapBack();
            selected2.swapBack();
            selected1 = null;
            selected2 = null;
            timer1.Stop();
        }

        private void Tick2(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            lblTimer.Text = String.Format("{0:D2}:{1:D2}", minutes, seconds);
        }

        private void tile_reaction(object sender, EventArgs e)
        {
            var b = sender as Tile;
            if (b != null)
            {
                if (selected2 != null)
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

        #endregion
    }
}
