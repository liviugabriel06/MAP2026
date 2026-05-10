using System.Collections.Generic;

namespace MusicPlayer.Commands;

public class CommandHistory
{
    private readonly Stack<IPlayerCommand> _undoStack = new();
    private readonly Stack<IPlayerCommand> _redoStack = new();
    private const int MaxHistorySize = 50;

    public bool CanUndo => _undoStack.Count > 0;
    public bool CanRedo => _redoStack.Count > 0;

    public void Execute(IPlayerCommand cmd)
    {
        cmd.Execute();
        if (cmd.CanUndo)
        {
            _undoStack.Push(cmd);
            _redoStack.Clear();

            if (_undoStack.Count > MaxHistorySize)
            {
                var temp = new Stack<IPlayerCommand>(_undoStack);
                temp.Pop();
                _undoStack.Clear();
                foreach (var item in temp) _undoStack.Push(item);
            }
        }
    }

    public void Undo()
    {
        if (!CanUndo) return;
        var cmd = _undoStack.Pop();
        cmd.Undo();
        _redoStack.Push(cmd);
    }

    public void Redo()
    {
        if (!CanRedo) return;
        var cmd = _redoStack.Pop();
        cmd.Execute();
        _undoStack.Push(cmd);
    }

    public IEnumerable<IPlayerCommand> GetHistory() => _undoStack;
}