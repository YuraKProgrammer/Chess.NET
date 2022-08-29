﻿using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public interface IMatDetector
    {
        public bool Detect(List<IFigure> figures, King king);
    }
}