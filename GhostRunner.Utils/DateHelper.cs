using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Utils
{
    public class DateHelper
    {
        public static String ToDateOrdinal(int date)
        {
            string suffix = "th";

            if (((date % 100) / 10) != 1)
            {
                switch (date % 10)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    case 31:
                        suffix = "st";
                        break;
                }
            }

            return date.ToString() + suffix;
        }
    }
}
