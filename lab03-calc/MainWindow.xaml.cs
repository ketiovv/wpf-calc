using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace lab03_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonDigit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (Component.Text != "0")
            {
                Component.Text += button.Content.ToString();
                
            }
            else
            {
                Component.Text = button.Content.ToString();
            }
        }
        private void ButtonSign_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (Component.Text != "0" && Operation.Text == "0")
            {
                Operation.Text = Component.Text + " " + button.Content.ToString();
                Component.Text = 0.ToString();
            }
            else if (Component.Text != "0" && Operation.Text != "0")
            {
                Operation.Text += " " + Component.Text + " " + button.Content.ToString();
                Component.Text = 0.ToString();
            }
        }
        private void ButtonDot_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Component.Text += button.Content.ToString();
        }
        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            string math = (Operation.Text += Component.Text);
            string value = new DataTable().Compute(math, null).ToString();
            Operation.Text = "0";
            Component.Text = value;
        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button.Name == "ButtonC")
            {
                Operation.Text = "0";
                Component.Text = "0";
            }
            else if(button.Name == "ButtonCE")
            {
                Component.Text = "0";
            }
            else if (button.Name == "ButtonBackspace")
            {
                Component.Text = Component.Text.Remove(Component.Text.Length-1, 1);
            }
        }
    }
}
