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
        private int toDoUniqueId = 0;

        private string titleTextBoxPlaceholder = "Enter title";
        private string descriptionTextBoxPlaceholder = "Enter description";

        ColorsConfig colorsConfig = new ColorsConfig();
        CustomButtons customButtons = new CustomButtons();

        private enum ButtonsStackPanelOrder
        {
            RemoveButton = 0,
            EditButton,
            SaveButton,
            CancelButton
        }

        private enum ToDoStackPanelOrder
        {
            Title = 0,
            Description,
            ButtonsStackPanel,
        }

        public MainWindow()
        {
            InitializeComponent();

            titleTextBox.Text = titleTextBoxPlaceholder;
            titleTextBox.Tag = titleTextBoxPlaceholder;
            descriptionTextBox.Text = descriptionTextBoxPlaceholder;
            descriptionTextBox.Tag = descriptionTextBoxPlaceholder;

            titleTextBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.textBoxBorderDefaultBackground));
        }

        private bool isTextBoxEmpty(TextBox textBox) => textBox.Text.Trim() == "";
        private bool isPlaceholder(TextBox textBox, string placeholder) => textBox.Text == placeholder;

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (isPlaceholder(currentTextBox, (string)currentTextBox.Tag)) currentTextBox.Text = "";
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox currentTextBox = sender as TextBox;

            if (isTextBoxEmpty(currentTextBox)) currentTextBox.Text = (string)currentTextBox.Tag; 
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
                titleTextBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.textBoxBorderErrorBackground));
                canBeAdded = false;
            }

            if (isTextBoxEmpty(descriptionTextBox) || isPlaceholder(descriptionTextBox, descriptionTextBoxPlaceholder))
            {
                descriptionTextBox.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.textBoxBorderErrorBackground));
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
            // Unique id is set to toDoStackPanel (parent of the button parent), so firstly we must get it
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

        private void HandleToDoTools(StackPanel buttonsStackPanel)
        {
            StackPanel toDoStackPanel = buttonsStackPanel.Parent as StackPanel;

            Button editButton = buttonsStackPanel.Children[(int)ButtonsStackPanelOrder.EditButton] as Button;
            Button saveButton = buttonsStackPanel.Children[(int)ButtonsStackPanelOrder.SaveButton] as Button;
            Button cancelButton = buttonsStackPanel.Children[(int)ButtonsStackPanelOrder.CancelButton] as Button;

            editButton.Visibility = (editButton.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            saveButton.Visibility = (saveButton.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            cancelButton.Visibility = (cancelButton.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;

            TextBox title = toDoStackPanel.Children[(int)ToDoStackPanelOrder.Title] as TextBox;
            TextBox description = toDoStackPanel.Children[(int)ToDoStackPanelOrder.Description] as TextBox;

            title.Focusable = (title.Focusable) ? false : true;
            description.Focusable = (description.Focusable) ? false : true;
            title.BorderThickness = (title.BorderThickness.Top == 0) ? new Thickness(1) : new Thickness(0);
            description.BorderThickness = (title.BorderThickness.Top == 0) ? new Thickness(1) : new Thickness(0);
        }

        private void EditButton_Click(object sender, RoutedEventArgs s)
        {
            Button editButton = sender as Button;
            StackPanel buttonsStackPanel = editButton.Parent as StackPanel;

            HandleToDoTools(buttonsStackPanel);          
        }

        private void AddEditButtonTools(StackPanel buttonsStackPanel)
        {
            buttonsStackPanel.Children.Add(customButtons.getSaveButton());
            buttonsStackPanel.Children.Add(customButtons.getCancelButton());
        }

        private void AddToDoToStackPanel(ToDo toDo)
        {
            /*
                The structure of each toDo is that:
                ToDoStackPanel: (look ToDoStackPanelOrder enum)
                    - Title TextBox
                    - Description TextBox
                    - Buttons' StackPanel: (look ButtonsStackPanelOrder enum)
                        - Remove Button
                        - Edit Button
                        - Save Button (will appear after edit button click)
                        - Cancel Button (will appear after edit button click)
            */
            StackPanel toDoStackPanel = new StackPanel 
            { 
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10, 0, 0, 10)
                
            };
            toDoStackPanel.Tag = toDoUniqueId++;

            TextBox title = new TextBox
            { 
                Text = toDo.Title,
                FontSize = 18,
                Foreground = Brushes.Black,
                Focusable = false,
                BorderThickness = new Thickness(0)
            };
            TextBox description = new TextBox
            {
                Text = toDo.Description,
                FontSize = 14,
                Foreground = Brushes.Black,
                Opacity = 0.7,
                TextWrapping = TextWrapping.Wrap,
                Focusable = false,
                BorderThickness = new Thickness(0)
            };

            StackPanel buttonsStackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            Button removeButton = customButtons.getRemoveButton();
            Button editButton = customButtons.getEditButton();
            Button saveButton = customButtons.getSaveButton();
            Button cancelButton = customButtons.getCancelButton();
            saveButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Collapsed;

            buttonsStackPanel.Children.Add(removeButton);
            buttonsStackPanel.Children.Add(editButton);
            buttonsStackPanel.Children.Add(saveButton);
            buttonsStackPanel.Children.Add(cancelButton);

            toDoStackPanel.Children.Add(title);
            toDoStackPanel.Children.Add(description);
            toDoStackPanel.Children.Add(buttonsStackPanel);

            toDoListStackPanel.Children.Add(toDoStackPanel);

            removeButton.Click += RemoveButton_Click;
            editButton.Click += EditButton_Click;
        }
    }
}
