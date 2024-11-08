using System.Windows;
using System.Windows.Controls;

namespace Remembvoc.AdditionalUI.DialogHosts
{
    /// <summary>
    /// Interakční logika pro ErrorDialogUserControl.xaml
    /// </summary>
    public partial class ErrorDialogUserControl : UserControl
    {
        public string ErrorText { get; set; }
        public string? DialogHostIdentifier { get; set; }

        public ErrorDialogUserControl()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DialogHostIdentifier))
                MaterialDesignThemes.Wpf.DialogHost.Close(DialogHostIdentifier);
        }
    }
}
