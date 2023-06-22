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
    public partial class MainWindow : Window
    {
        private string titleTextBoxPlaceholder = "Enter title";
        private string descriptionTextBoxPlaceholder = "Enter description";
        private int uniqueId = 0;

        public MainWindow()
        {
            InitializeComponent();

            titleTextBox.Text = titleTextBoxPlaceholder;
            descriptionTextBox.Text = descriptionTextBoxPlaceholder;
        }

        private bool isTextBoxEmpty(TextBox textBox) => textBox.Text.Trim() == "";
        private bool isPlaceholder(TextBox textBox, string placeholder) => textBox.Text == placeholder;

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (currentTextBox == titleTextBox && isPlaceholder(currentTextBox, titleTextBoxPlaceholder))
            {
                currentTextBox.Text = "";
            }
            else if (currentTextBox == descriptionTextBox && isPlaceholder(currentTextBox, descriptionTextBoxPlaceholder))
            {
                currentTextBox.Text = "";
            }

        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (currentTextBox == titleTextBox && isTextBoxEmpty(currentTextBox))
            {
                currentTextBox.Text = titleTextBoxPlaceholder;
            }
            else if (currentTextBox == descriptionTextBox && isTextBoxEmpty(currentTextBox))
            {
                currentTextBox.Text = descriptionTextBoxPlaceholder;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            appGrid.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool canBeAdded = true;

            if (isTextBoxEmpty(titleTextBox) || isPlaceholder(titleTextBox, titleTextBoxPlaceholder))
            {
                titleTextBox.BorderBrush = Brushes.Red;
                canBeAdded = false;
            }

            if (isTextBoxEmpty(descriptionTextBox) || isPlaceholder(descriptionTextBox, descriptionTextBoxPlaceholder))
            {
                descriptionTextBox.BorderBrush = Brushes.Red;
                canBeAdded = false;
            }

            if (canBeAdded)
            {
                ToDo toDo = new ToDo(titleTextBox.Text, descriptionTextBox.Text);

                AddToDoToStackPanel(toDo);
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs s)
        {
            // Unique id is set to toDoStackPanel (parent of the button parent), so firstly we must get
            Button removeButton = sender as Button;
            StackPanel titleStackPanel = removeButton.Parent as StackPanel;
            StackPanel toDoStackPanel = titleStackPanel.Parent as StackPanel;

            int toDoUniqueId = (int)toDoStackPanel.Tag;
            int elemIdToRemove = 0;

            if (toDoListStackPanel.Children.Count > 1)
            {          
                // We go through all the elements in the ToDo list stack panel and find appropriate id to remove
                foreach (StackPanel elemStackPanel in toDoListStackPanel.Children)
                {
                    int currentId = toDoListStackPanel.Children.IndexOf(elemStackPanel);

                    if (currentId == toDoUniqueId) elemIdToRemove = currentId;
                }
            }

            toDoListStackPanel.Children.RemoveAt(elemIdToRemove);
        }

        private void AddToDoToStackPanel(ToDo toDo)
        {
            StackPanel toDoStackPanel = new StackPanel 
            { 
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10, 0, 0, 10)
                
            };
            toDoStackPanel.Tag = uniqueId++;

            StackPanel titleStackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            TextBlock title = new TextBlock 
            { 
                Text = toDo.Title,
                FontSize = 18,
                Foreground = Brushes.Black
            };

            Button baseButton = new Button
            {
                Margin = new Thickness(10, 0, 0, 0),
                Padding = new Thickness(5, 0, 5, 0),
                BorderThickness = new Thickness(0),
                Width = 100,
                Height = 25,
            };
            Button removeButton = new Button
            {
                Content = "Remove",
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED5E68")),
                Foreground = Brushes.White,
                Margin = baseButton.Margin,
                Padding = baseButton.Padding,
                BorderThickness = baseButton.BorderThickness,
                Width = baseButton.Width,
                Height = baseButton.Height,
            };
            Button editButton = new Button 
            {
                Content = "Edit",
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3F8EBD")),
                Foreground = Brushes.White,
                Margin = baseButton.Margin,
                Padding = baseButton.Padding,
                BorderThickness = baseButton.BorderThickness,
                Width = baseButton.Width,
                Height = baseButton.Height
            };
            titleStackPanel.Children.Add(title);
            titleStackPanel.Children.Add(removeButton);
            titleStackPanel.Children.Add(editButton);

            TextBlock description = new TextBlock 
            {
                Text = toDo.Description,
                FontSize = 14,
                Foreground = Brushes.Black,
                Opacity = 0.7,
                TextWrapping = TextWrapping.Wrap,
            };

            toDoStackPanel.Children.Add(titleStackPanel);
            toDoStackPanel.Children.Add(description);

            toDoListStackPanel.Children.Add(toDoStackPanel);

            removeButton.Click += RemoveButton_Click;
        }
    }
}
