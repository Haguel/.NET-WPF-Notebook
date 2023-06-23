using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Notebook
{
    internal class ColorsConfig
    {
        public readonly SolidColorBrush textBoxBorderDefaultBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ABADB3"));
        public readonly SolidColorBrush textBoxBorderErrorBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EE4238"));
        public readonly SolidColorBrush removeButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ED5E68"));
        public readonly SolidColorBrush editButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3F8EBD"));
        public readonly SolidColorBrush saveButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#25C077"));
        public readonly SolidColorBrush cancelButtonBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8388A4"));
        public readonly SolidColorBrush cancelButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5FDFF"));
    }
}
