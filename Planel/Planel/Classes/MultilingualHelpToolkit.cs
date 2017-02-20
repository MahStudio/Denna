using Windows.UI.Xaml;

namespace Planel.Classes
{
    class MultilingualHelpToolkit
    {
        //GetString("LanguageOptionsSubTitle","Text")l
        public static string GetString(string Title, string Property)
        {
            Windows.ApplicationModel.Resources.ResourceLoader loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var expected = loader.GetString(Title + "/" + Property);
            return expected;
        }

        public static FlowDirection GetObjectFlowDirection(string Title)
        {
            Windows.ApplicationModel.Resources.ResourceLoader loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var expected = loader.GetString(Title + "/FlowDirection");
            if (expected.StartsWith("R") || expected.StartsWith("r"))
                return FlowDirection.RightToLeft;
            else return FlowDirection.LeftToRight;
        }

        public static bool IsApplicationLanguage(string Language)
        {
            if (GetApplicationLanguage() == Language)
                return true;
            else return false;
        }

        public static string GetApplicationLanguage()
        {
            return GetString("SelectedLanguage", "Text");
        }
    }
}
