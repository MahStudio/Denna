using CrossPieCharts.UWP.PieCharts;
using Planel.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;

namespace Planel.Classes
{
    class worker
    {
        public static List<PieChartArgs> Piechartcol { get; set; }
        public static List<PieChartArgs> Piechartcol2 { get; set; }
        public static async void refresher()
        {
            MainPage.current.counterr(Models.Localdb.counter());
            await fmonth.current.filllist();
            await ftoday.current.filllist();
            await percentful();
            await fhome.current.refe();
            tiler();


        }
        public static async void tiler()
        {
            await Classes.LiveTile.livetile();
            await Classes.LiveTile.updatebadge();
        }
        public static async Task percentful()
        {
            
            Color yelow = new Color();
            yelow.A = 255;
            yelow.R = 255;
            yelow.G = 186;
            yelow.B = 0;

            Color green = new Color();
            green.A = 255;
            green.R = 32;
            green.G = 200;
            green.B = 165;
            Classes.mpercent percent = new Classes.mpercent();
            percent = Models.Localdb.percentage();
            Piechartcol = new List<PieChartArgs>
            {
                new PieChartArgs
                {
                    Percentage = percent.firstpercentage,
                    ColorBrush=new Windows.UI.Xaml.Media.SolidColorBrush(green)

                },
                new PieChartArgs
                {
                    Percentage = percent.firstsuspend,
                    ColorBrush=new Windows.UI.Xaml.Media.SolidColorBrush(yelow)

                }
            };
            Piechartcol2 = new List<PieChartArgs>
            {
                new PieChartArgs
                {
                    Percentage = percent.secondpercentage,
                    ColorBrush=new Windows.UI.Xaml.Media.SolidColorBrush(green)

                },
                new PieChartArgs
                {
                    Percentage = percent.secondsuspend,
                    ColorBrush=new Windows.UI.Xaml.Media.SolidColorBrush(yelow)

                }
            };


            
        }
    }
}
