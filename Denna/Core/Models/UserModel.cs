using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase.Lite;
using Windows.UI.Xaml.Media.Imaging;

namespace Core.Models
{
    public class UserModel
    {
       public static async void CreateUser(string ID, string email, string Name, string Family, string pass)
        {

            Windows.Storage.StorageFolder storageFolder =
Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
                await storageFolder.GetFileAsync("avatar.jpg");
            Windows.Storage.Streams.IRandomAccessStream random = await Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(sampleFile).OpenReadAsync();
            Windows.Graphics.Imaging.BitmapDecoder decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(random);
            Windows.Graphics.Imaging.PixelDataProvider pixelData = await decoder.GetPixelDataAsync();
            byte[] bytes = pixelData.DetachPixelData();
            var blob = BlobFactory.Create("image/jpg", bytes);
            Dictionary<string, object> mydic = new Dictionary<string, object>
            {
                ["Type"] = "user",
                ["ID"] = ID,
                ["Email"] = email,
                ["FirstName"] =Name ,
                ["LastName"] = Family,
                ["PassHash"] = pass,
                ["Avatar"] = blob,
                ["Created"] = DateTimeOffset.UtcNow,
                ["SchemaVers"] = 1
            }
            ;
            DBH.MakeDoc(mydic);

        }


        public static string GetName()
        {
           
            return "c";
        }
    }
}
