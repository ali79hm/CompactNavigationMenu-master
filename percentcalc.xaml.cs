using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompactNavigationMenu
{
    /// <summary>
    /// Interaction logic for percentcalc.xaml
    /// </summary>
    public partial class percentcalc : Window
    {
        static double ans;
        public string answer { get { return ans.ToString(); } } 
        public percentcalc()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                float number = float.Parse(numbersubject.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                float max = float.Parse(maxsubject.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                if (number >= max)
                {
                    ans = 100;
                }
                else
                {
                    ans = (number * 1.0 / max) * 100;
                }
                outputText.Text = String.Format("شما {0} درصد پروژه را انجام دادید", ans);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "خطای ناشناخته", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void maxsubject_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
