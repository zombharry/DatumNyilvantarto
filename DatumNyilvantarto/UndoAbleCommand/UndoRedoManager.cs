using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.UndoAbleCommand
{
    public class UndoRedoManager
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly Stack<IUndoableCommand> _undoStack = new();
        private readonly Stack<IUndoableCommand> _redoStack = new();

        private int _version = 0;
        private int _savePointVersion = 0;

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;
        public bool IsModified => _version != _savePointVersion;

        public void ExecuteCommand(IUndoableCommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();
            _version++;
            OnStateChanged();
        }
        public void Undo()
        {
            if (_undoStack.Any())
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                _version--;
                OnStateChanged();
            }
        }
        public void Redo()
        {
            if (_redoStack.Any())
            {
                var command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                _version++;
                OnStateChanged();
            }
        }
        public void SetSavePoint()
        {
            _savePointVersion = _version;
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanUndo)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanRedo)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsModified)));

            CommandManager.InvalidateRequerySuggested();
        }
    }
}
