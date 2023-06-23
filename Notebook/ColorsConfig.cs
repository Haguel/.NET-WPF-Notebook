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
        public readonly SolidColorBrush test = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ABADB3"));
        public readonly string textBoxBorderDefaultBackground = "#ABADB3";
        public readonly string textBoxBorderErrorBackground = "#EE4238";
        public readonly string removeButtonBackground = "#ED5E68";
        public readonly string editButtonBackground = "#3F8EBD";
        public readonly string saveButtonBackground = "#25C077";
        public readonly string cancelButtonBackground = "#8388A4";
        public readonly string cancelButtonForeground = "#F5FDFF";
    }
}
