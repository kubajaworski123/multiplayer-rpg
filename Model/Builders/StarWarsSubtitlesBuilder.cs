using MazeGame.Model.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame.Model.Builders
{
    internal class StarWarsSubtitlesBuilder : IMapBuilder
    {
        private List<string> _manual = new List<string>();
        public void BuildCentralHall(int width, int height)
        {
            _manual.Add("GEOLOGICAL ANALISIS INDICATES THE EXISTENS OF A BIG OPEN CHAMBER IN ITS CORE"); //76
            _manual.Add("ITS BELIEVED BY WISE SCHOLARS TO BE LOCATED AROUND THE VERY CENTER OF THE MOON"); //78
        }

        public void BuildChambers(int N)
        {
            _manual.Add("IN THE MAZE OF MINI YOU SHALL FIND OPEN CHABERS FULL OF PRECIOUS TREASURES"); //74
        }

        public void BuildCorridors(int turns)
        {
            _manual.Add("ONLY THE BOLD MAY TRAVERSE THE ENDLESS LABYRINTHS OF THIS ALIEN PLANET"); //70
            _manual.Add("YOU WERE TAUGHT THE ARTS OF NAVIGATION LIKE DFS/BFS BY GRAND MASTER YODA"); //72
        }

        public void BuildEmptyDungeon(int width, int height)
        {
            _manual.Add("IT IS A TIME OF CHAOS IN THE GALAXY FAR FAR AWAY"); //48
            _manual.Add("THE EMPIRE HOLDS IRON GRIP ON THE CIVILIZED WORLDS"); //50
            _manual.Add("DRIVING DESPERATE REFUGEES DEEP INTO UNKNOWN REGIONS"); //52
            _manual.Add("YOU'RE CHOSEN TO EXPLORE A PLACE UNKNOWN IN THE GALAXY"); //54
            _manual.Add("ITS CALLED MINI AND IS BELEVED TO BE AN UNDERGROUND MOON"); //56
        }
        public void BuildFilledDungeon(int width, int height)
        {
            _manual.Add("IT IS A TIME OF CHAOS IN THE GALAXY FAR FAR AWAY"); //48
            _manual.Add("THE EMPIRE HOLDS IRON GRIP ON THE CIVILIZED WORLDS"); //50
            _manual.Add("DRIVING DESPERATE REFUGEES DEEP INTO UNKNOWN REGIONS"); //52
            _manual.Add("YOU'RE CHOSEN TO EXPLORE A PLACE UNKNOWN IN THE GALAXY"); //54
            _manual.Add("ITS CALLED MINI AND IS BELEVED TO BE AN UNDERGROUND MOON"); //56
        }

        public void AddItems(int count)
        {                                                                      
            _manual.Add("MINI IS FULL OF ITEMS UNKNOWN IN OLD REGIONS OF THE GALAXY"); //58
            _manual.Add("YOU SHALL COLLECT THEM IN ORDER TO ADVANCE IN YOUR BOLD TASK"); //60
        }

        public void AddWeapons(int count)
        {
            _manual.Add("BE CAREFULL THOUGH! THIS WORLD MAY BE FULL OF MALICIOUS ALIENS"); //62
            _manual.Add("YOU WILL HAVE TO FIND WEAPONS TO PREPARE FOR ALL THREATS OF MINI"); //64
            _manual.Add("PICK THEM UP WHEN YOU FIND THEM WORTHY OF A MIGHTY LEGEND LIKE YOU"); //66
            _manual.Add("DO NOT WORRY ! YOU CAN ALWAYS LEAVE THEM SOMEWHERE ALONG YOUR JOUNEY"); //68
        }

        public void AddMobs(int count)
        {
        }
        public List<String> GetSubtitles()
        {
            _manual.Add("MAY THE FORCE BE WITH YOU BRAVE TRAVELER AS YOU DESCEND INTO THE PLANETS DEPTHS!"); //80
            _manual.Sort((x, y) => x.Length.CompareTo(y.Length));
            return _manual;
        }
        public void AddArtifact() { }
        public void AddMobGroup(Func<Mob> mobFactory, int count) { }
    }
}
