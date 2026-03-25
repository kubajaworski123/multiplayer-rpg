using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.Model.Map;

public class ControlsListBuilder: IMapBuilder
{
    public const string moveControl = "WSAD: MOVE";
    public const string pickupControl = "E: PICK UP";
    public const string dropControl = "ALT+0-9: DROP";
    public const string equipControl = "0-9: EQUIP";
    public const string exitControl = "ESC: EXIT";
    public const string helpControl = "H: HELP";
    public List<string> Controls {get; private set;} = new List<string>();

    public void BuildFilledDungeon(int width, int height) 
    {
        if (!Controls.Contains(exitControl))
        {
            Controls.Add(exitControl);
        }
        if (!Controls.Contains(helpControl))
        {
            Controls.Add(helpControl);
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
        if (!Controls.Contains(helpControl))
        {
            Controls.Add(helpControl);
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
    public List<string> GetControls()
    {
        return Controls;
    }
}
