using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator_Kalorii.MVVM.Model
{
    public class User
    {
        public int UserID { get; set; }
        public string NazwaUzytkownika { get; set; }

        public int Wzrost { get; set; }
        public string Plec { get; set; }
        public decimal ObecnaWaga { get; set; }
        public decimal DocelowaWaga { get; set; }
        public int Wiek { get; set; }
        public decimal KalorieNaDzien { get; set; }
        public decimal WodaNaDzien { get; set; }
    }
}
