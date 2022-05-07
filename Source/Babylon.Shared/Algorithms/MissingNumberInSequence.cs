using System.Collections.Generic;
using System.Linq;

namespace Babylon.Shared.Algorithms
{
    public class MissingNumberInSequence
    {
        public static int GetMissingElements(List<int> numbers)
        {
            numbers.Sort((item1, item2) => item1.CompareTo(item2));

            var result = new List<int>();
            var cnt = 0;

            if (!numbers.Any())
            {
                return default;
            }

            for (var i = numbers[0]; i <= numbers[numbers.Count - 1]; i++)
            {
                if (numbers[cnt] == i)
                {
                    cnt++;
                }
                else
                {
                    result.Add(i);
                }
            }

            return result.FirstOrDefault()==0 ? numbers.Max()+1 : result.FirstOrDefault();
        }
    }
}