using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Globalization;

namespace CompactNavigationMenu.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        double fullPenalty = 30000;
        public HomeView()
        {
            InitializeComponent();
        }
        public void showerr(int errcode)
        {
            if (errcode == 1)
            {
                MessageBox.Show("لطفا هیچ یک از کادر ها را خالی نگذارید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (errcode == 2)
            {
                MessageBox.Show("لطفا در کادر ''میزان ویدئو دیده شده'' درصد را به طور درست وارد کنید ", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (errcode == 3)
            {
                MessageBox.Show("لطفا در کادر ''درصد پروژه انجام شده'' درصد را به طور درست وارد کنید ", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (errcode == 4)
            {
                MessageBox.Show("خطایی ناشناخته رخ داده ", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void calcBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            outputText.Text = "";
            //check
            int err = checkBoxes();
            if (err > 0)
            {
                showerr(err);
            }
            else
            {
                //darsad if more than 85 noting
                try
                {
                    float fullX = float.Parse(videoNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat)
                    + (float.Parse(projectNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat) * 5);
                    float compX = 0;
                    if (RdNumber.IsChecked == true)
                    {
                        compX = float.Parse(compVideoNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat) + (float.Parse(projectNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat)
                            * float.Parse(compProjectNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat) * 5 / 100);
                    }
                    else
                    {
                        compX = (float.Parse(compVideoNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat)
                        * float.Parse(videoNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat) / 100)
                            + (float.Parse(projectNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat)
                            * float.Parse(compProjectNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat) * 5 / 100);
                    }
                    //mohasebe x 
                    double X = (10 / (fullX * 0.1)) / 100;
                    //hal nahsode be kol darsadi
                    double compPercent = X * compX;
                    
                    if (compPercent < 0.85)
                    {
                        compPercent = Math.Round(compPercent*100, 4);
                        outputText.Text += String.Format("متاسفانه شما فقط {0} درصد از ددلاین خود را انجام داده اید و مشمول جریمه می شوید", compPercent);
                        double notcomppercent = (fullX - compX) / fullX;
                        double penalty = Math.Round(fullPenalty * notcomppercent, 2);
                        int roundpenalty = (int)Math.Round(penalty / 100, 0)*100;
                        outputText.Text += String.Format("\n جریمه شما {0} تومان میشود (قابل پرداخت : {1} تومان) ", penalty, roundpenalty);
                    }
                    else
                    {
                        compPercent = Math.Round(compPercent * 100, 4);
                        outputText.Text += String.Format("تبریک شما {0} درصد از ددلاین خود را انجام داده اید و مشمول جریمه نمی شوید", compPercent);
                    }
                }
                catch (Exception)
                {
                    showerr(4);
                }
            }
        }
        private int checkBoxes()  //return 1 => boxes are empty  | retun 2 => compVideoNumber in percent mode must be between 0-100 | retun 3 => compProjectNumber must be between 0-100 | return 0 => no err
        {
            try
            {
                if (videoNumber.Text.Length < 1 || projectNumber.Text.Length < 1 || compProjectNumber.Text.Length < 1 || compVideoNumber.Text.Length < 1)
                {
                    return 1;
                }
                else if (RDPersent.IsChecked == true)
                {
                    float temp = float.Parse(compVideoNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    if (temp < 0 || temp > 100)
                    {
                        return 2;
                    }
                }
                    float temp2 = float.Parse(compProjectNumber.Text.ToString(), CultureInfo.InvariantCulture.NumberFormat);
                    if (temp2 < 0 || temp2 > 100)
                    {
                        return 3;
                    }
            }
            catch (Exception errroe)
            {
                MessageBox.Show(errroe.ToString(), "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                return 4;
            }
            return 0;
        }

        private void RdNumber_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (percentLable != null)
            {
                percentLable.Visibility = System.Windows.Visibility.Hidden;
                compVideoNumber.Padding = new Thickness(1, 1.5, 0, 1);
            }
        }

        private void RDPersent_Checked(object sender, RoutedEventArgs e)
        {
            if (percentLable !=null)
            {
                percentLable.Visibility = System.Windows.Visibility.Visible;
                compVideoNumber.Padding = new Thickness(12, 1.5, 0, 1);
            }
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string temp="";
            var window = new percentcalc();
            window.ShowDialog();
            temp = window.answer;
            compProjectNumber.Text = temp;
            
        }


        private void videoNumber_GotFocus_1(object sender, RoutedEventArgs e)
        {
            videoNumber.Text = "";
        }

        private void projectNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            projectNumber.Text = "";
        }

        private void compVideoNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            compVideoNumber.Text = "";
        }

        private void compProjectNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            compProjectNumber.Text = "";
        }
    }
}
