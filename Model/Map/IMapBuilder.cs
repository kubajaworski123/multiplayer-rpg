using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Map;

public interface IMapBuilder
{
    void BuildEmptyDungeon(int width, int height);
    void BuildFilledDungeon(int width, int height);
    void BuildCorridors(int turns);
    void BuildChambers(int N);
    void BuildCentralHall(int width, int height);
    void BuildItems(int count);
    void BuildWeapons(int count);
}
