using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabV3OOPData
{
    public class Tile : PictureBox
    {
        private EventHandler _event;
        private string _backgroundImage = "../../../data/background.jpg";
        public string _image;
        private bool _matched;

        public bool Empty
        {
            get
            {
                return (_image == null) ? true : false;
            }
        }

        public bool Matched
        {
            get { return _matched; }
            set
            {
                _matched = value;
                RemoveAction();
            }
        }

        public Tile(string x, EventHandler e, int cellHeight, int cellWidth)
        {
            if (x == "empty")
            {
                _matched = true;
                _image = null;
            }
            else
                _image = x;

            _event = e;
            SetAction();
            base.ImageLocation = _backgroundImage;
            base.Width = cellWidth;
            base.Height = cellHeight;
            base.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void RemoveAction()
        {
            base.Click -= _event;
        }

        public void SetAction()
        {
            base.Click += _event;
        }

        public void swap()
        {
            base.ImageLocation = _image;
            if (_image == null)
            {
                _matched = true;
                RemoveAction();
            }
        }

        public void swapBack()
        {
            base.ImageLocation = _backgroundImage;
        }

        public bool IsEqual(Tile a)
        {
            if (a._image == this._image)
                return true;
            return false;
        }

        public void revealHint()
        {
            if (base.ImageLocation != _backgroundImage)
                return;
            base.ImageLocation = _image;
            this.RemoveAction();
        }

        public void hideHint()
        {
            base.ImageLocation = _backgroundImage;
            this.SetAction();
        }

        public bool toHint()
        {
            if (base.ImageLocation == _backgroundImage)
                return true;
            return false;
        }

    }
}
