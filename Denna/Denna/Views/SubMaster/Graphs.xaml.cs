using Denna.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Graphs : Page
    {
        public GraphViewModel VM { get; set; }

        public Graphs()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                VM = DataContext as GraphViewModel;
            };
        }
    }
}