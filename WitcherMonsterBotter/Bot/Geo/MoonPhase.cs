using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WitcherMonsterBotter.Bot.Geo
{
    public static class MoonPhase
    {
        private static double rang(double x)
        {
            double b = x / 360;
            double A = 360 * (b - (int)(b));
            if (A < 0) A = A + 360;
            return A;
        }

        public static double GetMoonPhase(DateTime dateTime)
        {
            double year = dateTime.Year;
            double month = dateTime.Month;
            double day = dateTime.Day;
            double hour = dateTime.Hour;
            double min = dateTime.Minute;
            double sec = dateTime.Second;

            if (month <= 2.0)
            {
                month = month + 12.0;
                year = year - 1.0;
            }

            double A = (int)(year / 100);
            double b = 2 - A + (int)(A / 4);
            double jdp = (int)(365.25 * (year + 4716)) + (int)(30.6001 * (month + 1)) + day + b +
                  ((hour + min / 60 + sec / 3600) / 24) - 1524.5;

            double tzd = (jdp - 2451545) / 36525;
            double elm = rang(297.8502042 + 445267.1115168 * tzd - (0.00163 * tzd * tzd) + tzd * tzd * tzd / 545868 - tzd * tzd * tzd * tzd / 113065000);
            double ams = rang(357.5291092 + 35999.0502909 * tzd - 0.0001536 * tzd * tzd + tzd * tzd * tzd / 24490000);
            double aml = rang(134.9634114 + 477198.8676313 * tzd - 0.008997 * tzd * tzd + tzd * tzd * tzd / 69699 - tzd * tzd * tzd * tzd / 14712000);
            double asd = 180 - elm - (6.289 * Math.Sin((3.1415926535 / 180) * ((aml)))) +
                                (2.1 * Math.Sin((3.1415926535 / 180) * ((ams)))) -
                                (1.274 * Math.Sin((3.1415926535 / 180) * (((2 * elm) - aml)))) -
                                (0.658 * Math.Sin((3.1415926535 / 180) * ((2 * elm)))) -
                                (0.214 * Math.Sin((3.1415926535 / 180) * ((2 * aml)))) -
                                (0.11 * Math.Sin((3.1415926535 / 180) * ((elm))));
            double phi1 = (1 + Math.Cos((3.1415926535 / 180) * (asd))) / 2;


            tzd = (jdp + (0.5 / 24) - 2451545) / 36525;
            elm = rang(297.8502042 + 445267.1115168 * tzd - (0.00163 * tzd * tzd) + tzd * tzd * tzd / 545868 - tzd * tzd * tzd * tzd / 113065000);
            ams = rang(357.5291092 + 35999.0502909 * tzd - 0.0001536 * tzd * tzd + tzd * tzd * tzd / 24490000);
            aml = rang(134.9634114 + 477198.8676313 * tzd - 0.008997 * tzd * tzd + tzd * tzd * tzd / 69699 - tzd * tzd * tzd * tzd / 14712000);
            asd = 180 - elm - (6.289 * Math.Sin((3.1415926535 / 180) * ((aml)))) +
                                (2.1 * Math.Sin((3.1415926535 / 180) * ((ams)))) -
                                (1.274 * Math.Sin((3.1415926535 / 180) * (((2 * elm) - aml)))) -
                                (0.658 * Math.Sin((3.1415926535 / 180) * ((2 * elm)))) -
                                (0.214 * Math.Sin((3.1415926535 / 180) * ((2 * aml)))) -
                                (0.11 * Math.Sin((3.1415926535 / 180) * ((elm))));
            double phi2 = (1 + Math.Cos((3.1415926535 / 180) * (asd))) / 2;


            if ((phi2 - phi1) < 0) phi1 = -1 * phi1;

            return 100 * phi1;
        }
    }
}
