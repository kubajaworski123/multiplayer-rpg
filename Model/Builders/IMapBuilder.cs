using MazeGame.Model.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Builders;

public interface IMapBuilder
{
    void BuildEmptyDungeon(int width, int height);
    void BuildFilledDungeon(int width, int height);
    void BuildCorridors(int turns);
    void BuildChambers(int N);
    void BuildCentralHall(int width, int height);
    void AddItems(int count);
    void AddWeapons(int count);
    void AddMobs(int count);
    void AddArtifact();
    void AddMobGroup(Func<Mob> mobCreationDelegate, int count);
}