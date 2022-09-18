using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Figures
{
    /// <summary>
    /// Описание фигуры
    /// </summary>
    public class FigureAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }
        public FigureAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
