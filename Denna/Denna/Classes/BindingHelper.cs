using Denna.Views.SubMaster;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Core.Utils;

namespace Denna.Classes
{
    public class BindingHelper : DependencyObject
    {
        public static string GetText(DependencyObject obj) => (string)obj.GetValue(TextProperty);

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(BindingHelper), new PropertyMetadata(string.Empty, OnTextChanged));

        static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as RichTextBlock;
            if (control != null)
            {
                control.Blocks.Clear();
                var value = e.NewValue.ToString();
                using (var pg = new PassageHelper())
                {
                    var passages = pg.GetParagraph(value, CaptionHyperLinkClick);
                    control.Blocks.Add(passages);
                }
            }
        }

        static async void CaptionHyperLinkClick(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            if (sender == null)
                return;
            try
            {
                if (sender.Inlines.Count > 0)
                {
                    if (sender.Inlines[0] is Run run && run != null)
                    {
                        var text = run.Text;
                        text = text.ToLower();
                        run.Text.ShowInOutput();
                        if (text.StartsWith("http://") || text.StartsWith("https://") || text.StartsWith("www."))
                            run.Text.OpenUrl();
                        else
                        {
                            TimeLine.current.DoOutsiderSearch(run.Text);
                        }
                    }
                }
            }
            catch (Exception ex) { ex.ExceptionMessage("CaptionHyperLinkClick"); }
        }
    }
}