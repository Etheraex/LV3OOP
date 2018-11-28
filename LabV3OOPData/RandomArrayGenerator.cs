using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabV3OOPData
{
    public class RandomArrayGenerator
    {
        public List<string> generateArray(int count,int size, int imageCount)
        {
            Random random = new Random();
            List<string> icons = Images.LoadedImages.ImageNames;
            List<string> randomString = new List<string>();

            int rand;
            int tmp = count;
            if (count > size)
                tmp = size;
            for (int i = 0; i < tmp / 2; i++)
            {
                rand = random.Next(0, icons.Count);
                randomString.Add(icons[rand]);
                if(imageCount > 0)
                {
                    imageCount--;
                    icons.RemoveAt(rand);
                }
            }
            // List doubles itself 
            // to provide a pair to every image
            randomString.AddRange(randomString);

            // Filling the rest of the list with "empty" tiles
            for (int i = count; i < size; i++)
                randomString.Add("empty");

            // Aditional shuffling to break up the empty elements
            randomString = ShuffleList(randomString);
            return randomString;
        }

        private List<string> ShuffleList(List<string> inputList)
        {
            List<string> randomList = new List<string>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); 
                randomList.Add(inputList[randomIndex]); 
                inputList.RemoveAt(randomIndex); 
            }

            return randomList; 
        }
    }
}
