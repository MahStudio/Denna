using CrossPieCharts.UWP.PieCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Planel.Classes
{
    
    class ViewModel
    {
    public static List<PieChartArgs> Piechartcol { get; set; }
    public static List<PieChartArgs> Piechartcol2 { get; set; }
        public ViewModel()
        {

            piecha();


        }
        public static async Task piecha()
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

                },new PieChartArgs
                {
                    Percentage = 100-percent.firstsuspend-percent.firstpercentage,
                    ColorBrush=new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Gray)

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

                },new PieChartArgs
                {
                    Percentage = 100-percent.secondsuspend-percent.secondpercentage,
                    ColorBrush=new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Gray)

                }
            };
        }
    }
    }

