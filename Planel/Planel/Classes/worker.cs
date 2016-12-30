using Planel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planel.Classes
{
    class worker
    {
        public static void refresher()
        {
            MainPage.current.counterr(Models.Localdb.counter());
            MainPage.current.fhome.Navigate(typeof(fhome));
            MainPage.current.ftoday.Navigate(typeof(ftoday));
            MainPage.current.fmonth.Navigate(typeof(fmonth));
            MainPage.current.fpref.Navigate(typeof(fpref));


        }
    }
}
