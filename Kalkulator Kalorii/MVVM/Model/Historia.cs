using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator_Kalorii.MVVM.Model
{
    public class Historia
    {
        public int HistoriaID { get; set; }
        public int UserID { get; set; }
        public string PosilekTyp { get; set; }
        public string DanyPosilek { get; set; }
        public decimal WagaPosilku { get; set; }
        public int KalorycznoscPosilku { get; set; }
        public decimal Woda { get; set; }
        public string Aktywnosc { get; set; }
        public decimal CzasAktywnosci { get; set; }
        public DateTime Data { get; set; }
    }
}
