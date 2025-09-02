﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumNyilvantarto.Utils.UndoAbleCommand
{
    public interface IUndoableCommand
    {
        void Execute();
        void Undo();
    }
}
