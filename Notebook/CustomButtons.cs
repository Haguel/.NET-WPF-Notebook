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
    internal class CustomButtons
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

        public Button getSaveButton() 
        {
            return new Button
            {
                Content = "Save",
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.saveButtonBackground)),
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
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.cancelButtonBackground)),
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.cancelButtonForeground)),
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
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.removeButtonBackground)),
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
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorsConfig.editButtonBackground)),
                Foreground = Brushes.White,
                Margin = baseButton.Margin,
                Padding = baseButton.Padding,
                BorderThickness = baseButton.BorderThickness,
                Width = baseButton.Width,
                Height = baseButton.Height
            };
        }
    }
}
