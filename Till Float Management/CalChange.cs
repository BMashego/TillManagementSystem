using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Till_Float_Management
{
    public class CalChange
    {
        public int FiftyRands { get; }
        public int TwentyRands { get; }
        public int TenRands { get; }
        public int FiveRands { get; }
        public int TwoRands { get; }
        public int OneRands { get; }

        public CalChange(int change)
        {
            FiftyRands = (int)(change / 50);
            change %= 50;

            TwentyRands = (int)(change / 20);
            change %= 20;

            TenRands = (int)(change / 10);
            change %= 10;

            FiveRands = (int)(change / 5);
            change %= 5;

            TwoRands = (int)(change / 2);
            change %= 2;

            OneRands = (int)(change / 1);
            change %= 1;
        }
    }
}
