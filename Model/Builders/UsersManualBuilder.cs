using MazeGame.Controller;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MazeGame.Model.Builders
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
            $"PRESS {KeyBinds.HelpKey} TO EXIT THIS PANEL",
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
            "Pick them up and drop like regular items. Use 0-9 to hold a weapon in hands. ",
            "Weapons have diffferent classes - Light, Heavy, Magical. They also come with modifiers altering their stats. ",
            "Modifiers will be displayed next to weapons name in your inventory"
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
            $"To pick up an item move to its tile and press {KeyBinds.PickupKey}. If there was enough space in your inventory, it will appear there.",
            "You can drop an item from inventory using alt+0-9 bind. It will then appear on players tile.",
            "If there are multiple items on a tile, a number will appear as a tile symbol"
        };
        private bool _exit = false;
        private List<string> _exitStrings = new List<string>()
        {
            "EXITING",
            $"To exit the game, pres {KeyBinds.ExitKey} key. The console will shut down instantly."
        };
        private bool _mobs = false;
        private List<string> _mobsStrings = new List<string>()
        {
            "MOBS",
            "In the dungeon you will encounter various mobs. They will be marked on a tile where they are accesible.",
            "Mobs can be hostile. Hostile mobs will attack you when you step on their tile and combat cant be avoided.",
            "Combat is a turn game. Every turn you choose one attack. Attacks deal different damage depending on weapon class.",
            "Chosen attack determines your defence for the opponents turn. Your performance depends on players abilities as well.",
            "If your health drops to 0, you die and the game is over. If you kill a mob, it is removed from the game."
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

        public void AddItems(int count)
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
        public void AddWeapons(int count)
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
        public void AddMobs(int count)
        {
            if (!_mobs)
            {
                _mobs = true;
                _help.AddRange(_mobsStrings);
            }
        }
        public List<string> getUsersManual()
        {
            return _help;
        }
    }
}
