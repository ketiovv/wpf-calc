using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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


namespace Calculator
{
    public partial class MainWindow: Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            InitializeComponent();
        }
        private void ButtonDigit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isNumber(Component.Text))
            {
                if (Component.Text == "0")
                {
                    Component.Text = button.Content.ToString();
                }
                else
                {
                    Component.Text += button.Content.ToString();
                }
            }
        }
        private void ButtonSign_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isNumber(Component.Text) && !Component.Text.EndsWith("."))
            {


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
        }
        private void ButtonDot_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (isNumber(Component.Text))
            {
                if (!Component.Text.Contains('.'))
                {
                    Component.Text += button.Content.ToString();
                }
            }
        }
        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            if (isNumber(Component.Text) && !Component.Text.EndsWith("."))
            {
                string math = (Operation.Text += Component.Text);
                Operation.Text = "0";
                Component.Text = calculate(math);
            }

        }
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Name == "ButtonC")
            {
                Operation.Text = "0";
                Component.Text = "0";
            }
            else if (button.Name == "ButtonCE")
            {
                Component.Text = "0";
            }
            else if (button.Name == "ButtonBackspace")
            {
                if (Component.Text != "0" && isNumber(Component.Text))
                {
                    if (Component.Text.Length == 1)
                    {
                        Component.Text = "0";
                    }
                    else
                    {
                        Component.Text = Component.Text.Remove(Component.Text.Length - 1, 1);
                    }

                }
            }
        }
        private string calculate(string math)
        {
            math = Regex.Replace
                (
                    math, @"\d+(\.\d+)?", m =>
                    {
                        var x = m.ToString();
                        return x.Contains(".") ? x : string.Format("{0}.0", x);
                    }
                );
            try
            {
                double value = Math.Round(Convert.ToDouble(new DataTable().Compute(math, string.Empty)), 8);
                return (value < -9999999999 || value > 9999999999) ? "out of range" : value.ToString();
            }
            catch (DivideByZeroException)
            {
                return "zero divide";
            }

        }
        private bool isNumber(string text)
        {
            double temp;
            if (double.TryParse(text, out temp))
            {
                return true;
            }
            return false;
        }
    }
}
