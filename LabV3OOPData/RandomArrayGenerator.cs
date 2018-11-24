using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabV3OOPData
{
    public class RandomArrayGenerator
    {
        

        public static List<string> generateArray(int count)
        {
            Random random = new Random();

            List<string> icons = new List<string>
            {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                "1","2","3","4","5","6","7","8","9","0"
            };

            List<string> randomString = new List<string>();
            int rand;
            for (int i = 0; i < count / 2; i++)
            {
                rand = random.Next(0, icons.Count);
                randomString.Add(icons[rand]);
            }
            List<string> tmp = randomString;

            randomString.AddRange(tmp);

            return randomString;
        }
    }
}
