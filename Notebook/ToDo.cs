using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook
{
    class ToDo
    {
        private string title;
        private string description;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }

        public ToDo(string title, string description)
        {
            this.title = title;
            this.description = description;
        }
    }
}
