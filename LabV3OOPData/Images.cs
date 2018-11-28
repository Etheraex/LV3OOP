using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabV3OOPData
{
    class Images
    {
        private List<string> _imageNames = new List<string>();

        public List<string> ImageNames
        {
            get
            {
                return _imageNames;
            }
        }

        private Images()
        {
            LoadImages();
        }

        public void LoadImages()
        {
            _imageNames.Clear();
            foreach (string f in Directory.GetFiles("../../../data/", "*.png"))
            {
                _imageNames.Add(f);
            }
        }

        private static Images _loadedImages;
        public static Images LoadedImages
        {
            get
            {
                if (_loadedImages == null)
                    _loadedImages = new Images();
                return _loadedImages;
            }
        }
    }
}
