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

            int asd = _columns * _rows;
            if ((_columns * _rows) % 2 != 0)
                asd--;
            List<string> test = RandomArrayGenerator.generateArray(asd);

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
                DoIt(b);
            }
        }

        private async Task DoIt(Label b)
        {
            b.ForeColor = Color.Black;
            await Task.Delay(1500);
            b.ForeColor = Color.White;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
