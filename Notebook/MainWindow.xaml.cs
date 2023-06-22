using System;
using System.Collections.Generic;
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

namespace Notebook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string titleInputPlaceholder = "Enter title";
        private string descriptionInputPlaceholder = "Enter description";

        public MainWindow()
        {
            InitializeComponent();

            titleInput.Text = titleInputPlaceholder;
            descriptionInput.Text = descriptionInputPlaceholder;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (currentTextBox == titleInput && currentTextBox.Text == titleInputPlaceholder)
            {
                currentTextBox.Text = "";
            }
            else if (currentTextBox == descriptionInput && currentTextBox.Text == descriptionInputPlaceholder)
            {
                currentTextBox.Text = "";
            }

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (currentTextBox.Text.Trim() == "")
            {
                if (currentTextBox == titleInput)
                {
                    currentTextBox.Text = titleInputPlaceholder;
                }
                else if (currentTextBox == descriptionInput)
                {
                    currentTextBox.Text = descriptionInputPlaceholder;
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            appGrid.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
