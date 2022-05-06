using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba_BD2.Data
{
    static internal class ParserOfAddedInfo
    {
        public static List<string> TrimValue(string value)
        {
            var words = value.Split(',').Select(c => c.Trim()).ToList();
            return words;
        }
        public static List<string> TrimValue(string value1, string value2)
        {
            var words = TrimValue(value1).Concat(TrimValue(value2)).ToList();
            return words;
        }
    }
}
