using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabV3OOPData
{
    public class RandomArrayGenerator
    {
        public List<string> generateArray(int count,int size)
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
            }
            List<string> toDouble = randomString;
            randomString.AddRange(toDouble);
            for (int i = count; i < size; i++)
                randomString.Add("empty");

            List<string> asd = ShuffleList(randomString);
            return asd;
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
