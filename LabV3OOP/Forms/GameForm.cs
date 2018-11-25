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
        private int _rows;
        private int _columns;
        List<Label> _selected = new List<Label>(2);
        List<Label> _matched = new List<Label>();
        private int _arraySize;

        public GameForm(int r, int c)
        {
            InitializeComponent();

            _rows = r;
            _columns = c;

            tableLayoutPanel1.RowCount = _rows;
            tableLayoutPanel1.ColumnCount = _columns;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < _columns; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _columns));
            for (int i = 0; i < _rows; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _rows));

            int arraySize = _columns * _rows;
            if ((_columns * _rows) % 2 != 0)
                arraySize--;
            _arraySize = arraySize;
            List<string> test = RandomArrayGenerator.generateArray(arraySize);

            for(int i = 0; i < _rows * _columns; i++)
            {
                var b = new Label();
                b.TextAlign = ContentAlignment.MiddleCenter;
                b.Font = new Font("Webdings", 40);
                if (i < test.Count)
                    b.Text = test[i].ToString();
                else
                    b.Text = "";
                b.Name = string.Format("b_{0}", i + 1);
                b.Click += b_Click;
                b.Dock = DockStyle.Fill;
                b.ForeColor = Color.White;
                this.tableLayoutPanel1.Controls.Add(b);
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            var b = sender as Label;
            if (b != null)
            {
                if (_selected.Count == 0)
                {
                    b.ForeColor = Color.Black;
                    _selected.Add(b);
                    DoIt();
                }
                else if (_selected.Count == 1)
                {
                    b.ForeColor = Color.Black;
                    _selected.Add(b);
                }
            }
        }

        private async Task DoIt()
        {
            await Task.Delay(1500);
            if (CheckList())
                SaveItems();
            RemoveItems();
        }

        private void SaveItems()
        {
            for (int i = _selected.Count - 1; i >= 0; i--)
            {
                _selected[i].Click -= b_Click;
                _matched.Add(_selected[i]);
                _selected.RemoveAt(i);
            }
            if (_matched.Count == _arraySize)
            {
                GameWonForm gwf = new GameWonForm();
                gwf.ShowDialog();
            }
        }

        private void RemoveItems()
        {
            for(int i = _selected.Count - 1; i>=0; i--)
            {
                _selected[i].ForeColor = Color.White;
                _selected.RemoveAt(i);
            }
        }

        private bool CheckList()
        {
            if (_selected.Count == 2)
                if (_selected[0].Text == _selected[1].Text)
                    return true;
            return false;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
