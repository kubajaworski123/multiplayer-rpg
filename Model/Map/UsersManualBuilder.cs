using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MazeGame.Model.Map
{
    internal class UsersManualBuilder : IMapBuilder
    {
        public List<string> _help { get; private set; } = new List<string>()
        { 
            "██    ██ ███████ ███████ ██████  ███████     ███    ███  █████  ███    ██ ██    ██  █████  ██     ",  
            "██    ██ ██      ██      ██   ██ ██          ████  ████ ██   ██ ████   ██ ██    ██ ██   ██ ██     ", 
            "██    ██ ███████ █████   ██████  ███████     ██ ████ ██ ███████ ██ ██  ██ ██    ██ ███████ ██     ", 
            "██    ██      ██ ██      ██   ██      ██     ██  ██  ██ ██   ██ ██  ██ ██ ██    ██ ██   ██ ██     ", 
            " ██████  ███████ ███████ ██   ██ ███████     ██      ██ ██   ██ ██   ████  ██████  ██   ██ ███████",
            "PRESS H TO EXIT THIS PANEL",
            " "
        };

        private bool _general = false;
        private List<string> _generalStrings = new List<string>()
        {
            "OVERVIEW",
            "Your goal is to traverse a labirynth. Depending on settings, the layout of the maze will change.",
            "Everyting is played in console. All actions are accesible through keybord binds."
        };

        private bool _weapon = false;
        private List<string> _weaponStrings = new List<string>()
        {
            "WEAPONS",
            "In the dungeon you will find weapons. There are one and two handed weapons available.",
            "Every weapon has a damage stat. It will display next to its name on the right hand side of the map.",
            "Pick them up and drop like regular items. Use 0-9 to hold a weapon in hands. "
        };
        private bool _move = false;
        private List<string> _moveStrings = new List<string>()
        {
            "EXPLORING",
            "Move around the labirynth using WSAD or arrows. Depending on the map, you may encounter",
            "long corridors, large chambers or open areas."
        };
        private bool _pickupAndDrop = false;
        private List<string> _pickupAndDropStrings = new List<string>()
        {
            "ITEMS",
            "In the dungeon you will find various items. They will be marked on a tile on with they are accesible.",
            "To pick up an item move to its tile and press E. If there was enough space in your inventory, it will appear there.",
            "You can drop an item from inventory using alt+0-9 bind. It will then appear on players tile.",
            "If there are multiple items on a tile, a number will appear as a tile symbol"
        };
        private bool _exit = false;
        private List<string> _exitStrings = new List<string>()
        {
            "EXITING",
            "To exit the game, pres ESC key. The console will shut down instantly."
        };
        public void BuildCentralHall(int width, int height)
        {
            if (!_move)
            {
                _move = true;
                _help.AddRange(_moveStrings);
            }
        }

        public void BuildChambers(int N)
        {
            if (!_move)
            {
                _move = true;
                _help.AddRange(_moveStrings);
            }
        }

        public void BuildCorridors(int turns)
        {
            if (!_move)
            {
                _move = true;
                _help.AddRange(_moveStrings);
            }
        }

        public void BuildEmptyDungeon(int width, int height)
        {
            if (!_move)
            {
                _move = true;
                _help.AddRange(_moveStrings);
            }
            if (!_exit)
            {
                _exit = true;
                _help.AddRange(_exitStrings);
            }
            if (!_general)
            {
                _general = true;
                _help.AddRange(_generalStrings);
            }
        }

        public void BuildFilledDungeon(int width, int height)
        {
            if (!_exit)
            {
                _exit = true;
                _help.AddRange(_exitStrings);
            }
            if (!_general)
            {
                _general = true;
                _help.AddRange(_generalStrings);
            }

        }

        public void BuildItems(int count)
        {
            if (!_move)
            {
                _move = true;
                _help.AddRange(_moveStrings);
            }
            if (!_pickupAndDrop)
            {
                _pickupAndDrop = true;
                _help.AddRange(_pickupAndDropStrings);
            }
        }
        public void BuildWeapons(int count)
        {
            if (!_move)
            {
                _move = true;
                _help.AddRange(_moveStrings);
            }
            if (!_pickupAndDrop)
            {
                _pickupAndDrop = true;
                _help.AddRange(_pickupAndDropStrings);
            }
            if (!_weapon)
            {
                _weapon = true;
                _help.AddRange(_weaponStrings);
            }
        }
        public List<string> getUsersManual()
        {
            return _help;
        }
    }
}
