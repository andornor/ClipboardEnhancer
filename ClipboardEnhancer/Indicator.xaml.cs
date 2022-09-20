using System.Windows;
using System.Windows.Controls;


namespace ClipboardEnhancer.User_Controls
{
    /// <summary>
    /// Interaction logic for Indicator.xaml
    /// </summary>
    public partial class Indicator : UserControl
    {
        public Indicator()
        {
            InitializeComponent();
            this.DataContext = this;
            //this.Type = "ahha";
            
        }

        public string Type { get; set; }
    }


}
