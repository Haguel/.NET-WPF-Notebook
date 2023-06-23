using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Notebook
{
    internal class CustomElements
    {
        ColorsConfig colorsConfig = new ColorsConfig();

        private Button baseButton = new Button
        {
            Margin = new Thickness(0, 10, 10, 0),
            Padding = new Thickness(5, 0, 5, 0),
            BorderThickness = new Thickness(0),
            Width = 100,
            Height = 25,
        };

        private Button baseToolsButton = new Button
        {
            Margin = new Thickness(0, 10, 10, 0),
            Padding = new Thickness(5, 0, 5, 0),
            BorderThickness = new Thickness(0),
            Width = 75,
            Height = 25,
        };

        private TextBox baseTextBox = new TextBox
        {
            BorderThickness = new Thickness(1),
            MaxWidth = 500,
            Padding = new Thickness(5, 0, 5, 0),
        };

        public Button getSaveButton() 
        {
            return new Button
            {
                Content = "Save",
                Background = colorsConfig.saveButtonBackground,
                Foreground = Brushes.White,
                Margin = baseToolsButton.Margin,
                Padding = baseToolsButton.Padding,
                BorderThickness = baseToolsButton.BorderThickness,
                Width = baseToolsButton.Width,
                Height = baseToolsButton.Height,
            };
        }

        public Button getCancelButton()
        {
            return new Button
            {
                Content = "Cancel",
                Background = colorsConfig.cancelButtonBackground,
                Foreground = colorsConfig.cancelButtonForeground,
                Margin = baseToolsButton.Margin,
                Padding = baseToolsButton.Padding,
                BorderThickness = baseToolsButton.BorderThickness,
                Width = baseToolsButton.Width,
                Height = baseToolsButton.Height,
            };
        }

        public Button getRemoveButton()
        {
            return new Button
            {
                Content = "Remove",
                Background = colorsConfig.removeButtonBackground,
                Foreground = Brushes.White,
                Margin = baseButton.Margin,
                Padding = baseButton.Padding,
                BorderThickness = baseButton.BorderThickness,
                Width = baseButton.Width,
                Height = baseButton.Height,
            };
        }

        public Button getEditButton()
        {
            return new Button
            {
                Content = "Edit",
                Background = colorsConfig.editButtonBackground,
                Foreground = Brushes.White,
                Margin = baseButton.Margin,
                Padding = baseButton.Padding,
                BorderThickness = baseButton.BorderThickness,
                Width = baseButton.Width,
                Height = baseButton.Height
            };
        }

        public StackPanel getToDoStackPanel()
        {
            return new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 0, 0, 10)
            };
        }

        public TextBox getTitleTextBox(string text)
        {
            TextBox test = new TextBox
            {
                Text = text,
                FontSize = 18,
                Foreground = Brushes.Black,
                Focusable = false,
                BorderBrush = Brushes.Transparent,
                BorderThickness = baseTextBox.BorderThickness,
                Padding = baseTextBox.Padding,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = baseTextBox.MaxWidth,
                Margin = new Thickness(-10, 0, 0, 5)
            };

            return test;
        }

        public TextBox getDescriptionTextBox(string text) 
        {
            return new TextBox
            {
                Text = text,
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap,
                Focusable = false,
                BorderBrush = Brushes.Transparent,
                BorderThickness = baseTextBox.BorderThickness,
                MaxWidth = baseTextBox.MaxWidth,
                Padding = baseTextBox.Padding,
                Margin = new Thickness(-10, 0, 0, 0)
            };
        }
    }
}
