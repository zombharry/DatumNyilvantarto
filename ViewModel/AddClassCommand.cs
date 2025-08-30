using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.UndoAbleCommand;

namespace ViewModel
{
    class AddOsztalyCommand : IUndoableCommand
    {
        private readonly ObservableCollection<OsztalyViewModel> _collection;
        private readonly OsztalyViewModel _osztalyToAdd;

        public AddOsztalyCommand(ObservableCollection<OsztalyViewModel> collection, OsztalyViewModel @osztaly)
        {
            _collection = collection;
            _osztalyToAdd = @osztaly;
        }

        public void Execute()
        {
            _collection.Add(_osztalyToAdd);
        }

        public void Undo()
        {
            _collection.Remove(_osztalyToAdd);
        }
    }
}
