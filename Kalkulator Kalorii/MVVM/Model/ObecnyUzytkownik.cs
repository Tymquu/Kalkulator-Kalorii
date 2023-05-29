using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator_Kalorii.MVVM.Model
{
    public static class ObecnyUzytkownik
    {
        public static int wybrany_uzytkownik_id { get; set; }
        public static string wybrany_uzytkownik_nazwa { get; set; }

        public static int Wzrost { get; set; }
        public static string Plec { get; set; }
        public static decimal ObecnaWaga { get; set; }
        public static decimal DocelowaWaga { get; set; }
        public static int Wiek { get; set; }
        public static decimal KalorieNaDzien { get; set; }
        public static decimal WodaNaDzien { get; set; }
    }

}
