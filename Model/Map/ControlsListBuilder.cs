using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.Model.Map;

public class ControlsListBuilder: IMapBuilder
{
    const string moveControl = "\x1b[33mWSAD: MOVE\x1b[0m";
    const string pickupControl = "\x1b[33mE: PICK UP\x1b[0m";
    const string dropControl = "\x1b[33mALT+0-9: DROP\x1b[0m";
    const string equipControl = "\x1b[33m0-9: EQUIP\x1b[0m";
    const string exitControl = "\x1b[33mESC: EXIT\x1b[0m";
    public List<string> Controls {get; private set;} = new List<string>();

    public void BuildFilledDungeon(int width, int height) 
    {
        if (!Controls.Contains(exitControl))
        {
            Controls.Add(exitControl);
        }
    }
    public  void BuildEmptyDungeon(int width, int height)
    {
        if (!Controls.Contains(moveControl))
        {
            Controls.Add(moveControl);
        }
        if (!Controls.Contains(exitControl))
        {
            Controls.Add(exitControl);
        }
    }
    public void BuildCorridors(int turns) { 
        if(!Controls.Contains(moveControl))
        {
            Controls.Add(moveControl);
        }
    }
    public void BuildChambers(int N)
    {
            if (!Controls.Contains(moveControl))
            {
                Controls.Add(moveControl);
        }
    }
    public void BuildCentralHall(int width, int height)
    {
        if (!Controls.Contains(moveControl))
        {
            Controls.Add(moveControl);
        }
    }
    public void BuildItems(int count)
    {
        if (!Controls.Contains(pickupControl))
        {
            Controls.Add(pickupControl);
        }
        if (!Controls.Contains(dropControl))
        {
            Controls.Add(dropControl);
        }
    }
    public void BuildWeapons(int count)
    {
        if (!Controls.Contains(pickupControl))
        {
            Controls.Add(pickupControl);
        }
        if (!Controls.Contains(dropControl))
        {
            Controls.Add(dropControl);
        }
        if (!Controls.Contains(equipControl))
        {
            Controls.Add(equipControl);
        }
    }


}
