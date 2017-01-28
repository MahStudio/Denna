using Core;
using Planel.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;

namespace Planel.Classes
{
    class worker
    {
        
        public static async void refresher()
        {
            MainPage.current.counterr(Core.Models.Localdb.counter());
            await fmonth.current.filllist();
            await ftoday.current.filllist();
            await fhome.current.percentful();
            tiler();


        }
        public static async void tiler()
        {
           await Core.Classes.LiveTile.livetile();
           await Core.Classes.LiveTile.updatebadge();
        }
        public static smartyieval smartie ()
        {
            int[] smartiearray = new int [4];
            smartiearray[0] = (int)ApplicationData.Current.LocalSettings.Values["SmartieHome"]  ;
            smartiearray[1] = (int)ApplicationData.Current.LocalSettings.Values["SmartieToday"];
            smartiearray[2] = (int)ApplicationData.Current.LocalSettings.Values["SmartieMonth"];
            smartiearray[3] = (int)ApplicationData.Current.LocalSettings.Values["SmartiePref"];
            int maxValue = smartiearray.Max();
            int maxIndex = smartiearray.ToList().IndexOf(maxValue);

            smartyieval returner = smartyieval.Home;
            if (maxIndex == 0)
            {
                returner = smartyieval.Home;
            }
            else if(maxIndex == 1)
            {
                returner = smartyieval.Today;
            }
            else if (maxIndex == 2)
            {
                returner = smartyieval.Month;
            }
            else if (maxIndex == 3)
            {
                returner = smartyieval.Pref;
            }


            return returner;
        }
        public enum smartyieval
        {
            Home , Today , Month , Pref
        }
        
    }
}
