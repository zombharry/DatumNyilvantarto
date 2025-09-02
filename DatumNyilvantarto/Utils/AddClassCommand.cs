using DatumNyilvantarto.Utils.UndoAbleCommand;
using DatumNyilvantarto.viewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumNyilvantarto.Utils
{
    class AddOsztalyCommand : IUndoableCommand
    {
        private readonly ObservableCollection<SchoolClassViewModel> _collection;
        private readonly SchoolClassViewModel _classToAdd;

        public AddOsztalyCommand(ObservableCollection<SchoolClassViewModel> collection, SchoolClassViewModel @class)
        {
            _collection = collection;
            _classToAdd = @class;
        }

        public void Execute()
        {
            _collection.Add(_classToAdd);
        }

        public void Undo()
        {
            _collection.Remove(_classToAdd);
        }
    }
}
