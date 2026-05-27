using MazeGame.Model.Characters;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MazeGame.Model.Builders;

public class ControlsListBuilder: IMapBuilder
{
    public static readonly string moveControl = "WSAD: MOVE";
    public static readonly string pickupControl = "E: PICK UP";
    public static readonly string dropControl = "ALT+0-9: DROP";
    public static readonly string equipControl = "0-9: EQUIP";
    public static readonly string exitControl = "Escape: EXIT";
    public static readonly string helpControl = "H: HELP";
    public static readonly string logsControl = "L: LOGS";
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
        if (!Controls.Contains(logsControl))
        {
            Controls.Add(logsControl);
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
        if (!Controls.Contains(logsControl))
        {
            Controls.Add(logsControl);
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
    public void AddItems(int count)
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
    public void AddWeapons(int count)
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

    public void AddMobs(int count) { }
    public void AddArtifact() { }
    public void AddMobGroup(Func<Mob> mobFactory, int count) { }
    public List<string> GetControls()
    {
        return Controls;
    }
}
