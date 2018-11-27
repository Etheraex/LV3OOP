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
        private EventHandler _e;
        private string _backgroundImage = @"C:\Users\Mladen\source\repos\LabV3OOP\LabV3OOP\bin\Debug\data\background.jpg";
        private string _image;
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

        public Tile(string x, EventHandler e)
        {
            if (x == "empty")
            {
                _matched = true;
                _image = null;
            }
            else
                _image = x;

            _e = e;
            SetAction();
            base.ImageLocation = _backgroundImage;
            base.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void RemoveAction()
        {
            base.Click -= _e;
        }

        public void SetAction()
        {
            base.Click += _e;
        }

        public void swap()
        {
            base.ImageLocation = _image;
            if (_image == null)
                RemoveAction();
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
    }
}
