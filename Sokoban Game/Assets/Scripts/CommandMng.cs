using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMng{
    List<Command> CommandList = new List<Command>();
    int idx=-1;
    public void Execute(Vector2 after, Vector2 before) {
        Command command = new Command(after, before);
        CommandList.Add(command);
        idx++;
    }
    public int GetCount() {
        return CommandList.Count;
    }
    public Vector2 Undo() {
        if (CommandList.Count > 0) {
            Vector2 result = CommandList[idx].before_;
            CommandList.RemoveAt(idx);
            idx--;
            return result;
        }
        return new Vector2(0, 0);
    }
    public Vector2 Reset() {
        if (CommandList.Count > 0) {
            Vector2 result = CommandList[0].before_;
            CommandList.Clear();
            idx = -1;
            return result;
        }
        return new Vector2(0, 0);

    }
}
public class Command {
    public Vector2 after_;
    public Vector2 before_;
    public Command(Vector2 after, Vector2 before) {
        after_ = after;
        before_ = before;

    }
}
