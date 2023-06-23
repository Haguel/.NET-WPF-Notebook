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
        CustomElements customElements = new CustomElements();

        private string titleState;
        private string descriptionState;

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

            titleTextBox.BorderBrush = colorsConfig.textBoxBorderDefaultBackground;
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
                titleTextBox.BorderBrush = colorsConfig.textBoxBorderErrorBackground;
                canBeAdded = false;
            }

            if (isTextBoxEmpty(descriptionTextBox) || isPlaceholder(descriptionTextBox, descriptionTextBoxPlaceholder))
            {
                descriptionTextBox.BorderBrush = colorsConfig.textBoxBorderErrorBackground;
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
            Button removeButton = sender as Button;
            int toDoUniqueId = (int)removeButton.Tag;
            int elemIdToRemove = 0;

            if (toDoListStackPanel.Children.Count > 1)
            {          
                foreach (StackPanel elemStackPanel in toDoListStackPanel.Children)
                {
                    int elemStackPanelUniqueId = (int)elemStackPanel.Tag;

                    if (elemStackPanelUniqueId == toDoUniqueId) elemIdToRemove = toDoListStackPanel.Children.IndexOf(elemStackPanel);
                }
            }

            toDoListStackPanel.Children.RemoveAt(elemIdToRemove);
        }

        // button have to be a button that is buttonStackPanel's child
        private void HandleToDoTools(Button button)
        {        
            StackPanel buttonsStackPanel = button.Parent as StackPanel;
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
            title.BorderBrush = (title.BorderBrush == Brushes.Transparent) ? colorsConfig.textBoxBorderDefaultBackground : Brushes.Transparent;
            description.BorderBrush = (description.BorderBrush == Brushes.Transparent) ? colorsConfig.textBoxBorderDefaultBackground : Brushes.Transparent;
        }

        private void EditButton_Click(object sender, RoutedEventArgs s)
        {
            Button editButton = sender as Button;
            StackPanel buttonsStackPanel = editButton.Parent as StackPanel;
            StackPanel toDoStackPanel = buttonsStackPanel.Parent as StackPanel;

            titleState = (toDoStackPanel.Children[(int)ToDoStackPanelOrder.Title] as TextBox).Text;
            descriptionState = (toDoStackPanel.Children[(int)ToDoStackPanelOrder.Description] as TextBox).Text;

            HandleToDoTools(editButton);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs s)
        {
            Button cancelButton = sender as Button;
            StackPanel buttonsStackPanel = cancelButton.Parent as StackPanel;
            StackPanel toDoStackPanel = buttonsStackPanel.Parent as StackPanel;

            (toDoStackPanel.Children[(int)ToDoStackPanelOrder.Title] as TextBox).Text = titleState;
            (toDoStackPanel.Children[(int)ToDoStackPanelOrder.Description] as TextBox).Text = descriptionState;

            HandleToDoTools(cancelButton);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs s)
        {
            Button cancelButton = sender as Button;

            HandleToDoTools(cancelButton);
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

            StackPanel toDoStackPanel = customElements.getToDoStackPanel();
            toDoStackPanel.Tag = toDoUniqueId;

            TextBox title = customElements.getTitleTextBox(toDo.Title);
            TextBox description = customElements.getTitleTextBox(toDo.Description);
            description.Opacity = 0.7;
            description.FontSize = 14;

            StackPanel buttonsStackPanel = new StackPanel { Orientation = Orientation.Horizontal };

            Button removeButton = customElements.getRemoveButton();
            Button editButton = customElements.getEditButton();
            Button saveButton = customElements.getSaveButton();
            Button cancelButton = customElements.getCancelButton();
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

            removeButton.Tag = toDoUniqueId;
            removeButton.Click += RemoveButton_Click;
            editButton.Click += EditButton_Click;
            saveButton.Click += SaveButton_Click;
            cancelButton.Click += CancelButton_Click;

            toDoUniqueId++;
        }
    }
}
