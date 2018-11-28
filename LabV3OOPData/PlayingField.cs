using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabV3OOPData
{
    public class PlayingField
    {
        List<Tile> lista = new List<Tile>();
        int _pairs;
        int _rows;
        int _columns;

        private PlayingField() { }

        public bool AllMatched
        {
            get
            {
                bool tmp = true;
                foreach (Tile t in lista)
                    if (!t.Matched)
                        tmp = false;
                return tmp;
            }
        }

        public void RevealAll()
        {
            foreach (Tile t in lista)
                t.swap();
        }

        public TableLayoutPanel CreateTable(int pairs, int rows, int columns, EventHandler e)
        {
            _pairs = pairs;
            _rows = rows;
            _columns = columns;
            populateTileList(e);
            return createTable();
        }

        public List<Tile> returnList()
        {
            return lista;
        }

        private void populateTileList(EventHandler e)
        {
            List<string> asd = new RandomArrayGenerator().generateArray(_pairs, _rows * _columns);
            for(int i = 0; i < asd.Count; i++)
            {
                lista.Add(new Tile(asd[i], e));
            }
        }

        private TableLayoutPanel createTable()
        {
            TableLayoutPanel table = new TableLayoutPanel();
            table.RowCount = _rows;
            table.ColumnCount = _columns;
            for (int i = 0; i < _columns; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / _columns));
            for (int i = 0; i < _rows; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / _rows));

            for (int i = 0; i < lista.Count; i++)
            {
                table.Controls.Add(lista[i]);
            }

            table.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            table.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
            table.Location = new Point(0, 36);
            table.AutoSize = true;
            table.Dock = DockStyle.Fill;
            table.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right |  AnchorStyles.Left | AnchorStyles.Top);
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;

            return table;
        }

        private static PlayingField _playingFieldInstance;
        public static PlayingField PlayingFieldInstance
        {
            get
            {
                if (_playingFieldInstance == null)
                    _playingFieldInstance = new PlayingField();
                return _playingFieldInstance;
            }
        }
    }
}
