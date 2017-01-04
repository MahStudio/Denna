using Planel.Views;


namespace Planel.Classes
{
    class worker
    {
        public static async void refresher()
        {
            MainPage.current.counterr(Models.Localdb.counter());
            fmonth.current.filllist();
            ftoday.current.filllist();
           // MainPage.current.fhome.Navigate(typeof(fhome));
           // MainPage.current.ftoday.Navigate(typeof(ftoday));
           // MainPage.current.fmonth.Navigate(typeof(fmonth));
           // MainPage.current.fpref.Navigate(typeof(fpref));


        }
    }
}
