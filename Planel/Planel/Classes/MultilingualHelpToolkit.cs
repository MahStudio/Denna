using Windows.UI.Xaml;

namespace Planel.Classes
{
    class MultilingualHelpToolkit
    {
        // GetString("LanguageOptionsSubTitle","Text")l
        public static string GetString(string title, string Property)
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var expected = loader.GetString(title + "/" + Property);
            return expected;
        }

        public static FlowDirection GetObjectFlowDirection(string Title)
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
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

        public static string GetApplicationLanguage() => GetString("SelectedLanguage", "Text");
    }
}