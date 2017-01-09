
using Planel.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;

namespace Planel.Classes
{
    class worker
    {
        
        public static async void refresher()
        {
            MainPage.current.counterr(Models.Localdb.counter());
            await fmonth.current.filllist();
            await ftoday.current.filllist();
            await fhome.current.percentful();
            tiler();


        }
        public static async void tiler()
        {
           await Classes.LiveTile.livetile();
            await Classes.LiveTile.updatebadge();
        }
        
    }
}
