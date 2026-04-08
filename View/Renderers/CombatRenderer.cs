using MazeGame.Model.Characters;
using MazeGame.Model.Combat;
using MazeGame.Model.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace MazeGame.View.Renderers
{
    public class CombatRenderer(ICombat leftActor, ICombat rightActor, CombatSession session) : IRenderer
    {
        private ICombat _leftActor = leftActor;
        private ICombat _rightActor = rightActor;
        //private int HighlightedAttack = 0;
        StringBuilder sb = new StringBuilder();
        CombatSession _session = session;
        private List<string> VSbanner = new List<string>()
        {
            "██    ██ ███████", 
            "██    ██ ██     ", 
            "██    ██ ███████", 
            " ██  ██       ██", 
            "  ████   ███████"
        };
        private List<string> YOUbanner = new List<string>()
        {
            "██  ██ ▄████▄ ██  ██ ",
            " ▀██▀  ██  ██ ██  ██ ",
            "  ██   ▀████▀ ▀████▀ "   
        };
        private List<string> WINbanner = new List<string>()
        {   
            "██     ██ ██ ███  ██ ",
            "██ ▄█▄ ██ ██ ██ ▀▄██ ",
            " ▀██▀██▀  ██ ██   ██ "
        };
        private List<string> LOSEbanner = new List<string>()
        {
                                              
            "██     ▄████▄ ▄█████ ██████", 
            "██     ██  ██ ▀▀▀▄▄▄ ██▄▄  ", 
            "██████ ▀████▀ █████▀ ██▄▄▄▄"
                                           
        };
        private List<string> CalculateLeftPanel()
        {
            if(_leftActor.Banner == null)
                return new List<string>();
            List<string> leftPanel = new List<string>();
            int portraitWidth = _leftActor.Banner[0].Length;

            leftPanel.Add(string.Empty);
            leftPanel.Add(_leftActor.Name.ToUpper());
            leftPanel.AddRange(_leftActor.Banner);
            int hpFilledBoxes = _leftActor.Health * (portraitWidth - "HP:".Length) / 100;
            string hpBar = "HP:" + ViewUtils.RedCode + new string('█', hpFilledBoxes) +
                           new string(' ', portraitWidth - hpFilledBoxes - "HP:".Length) + ViewUtils.ResetCode;
            leftPanel.Add(hpBar);

            return leftPanel;
        }
        private List<string> CalculateRightPanel()
        {
            if (_rightActor.Banner == null)
                return new List<string>();
            List<string> rightPanel = new List<string>();
            int portraitWidth = _rightActor.Banner[0].Length;

            rightPanel.Add(string.Empty);
            rightPanel.Add(_rightActor.Name.ToUpper());
            rightPanel.AddRange(_rightActor.Banner);

            int hpFilledBoxes = _rightActor.Health * (portraitWidth - "HP:".Length) / 100;
            string hpBar = "HP:" + ViewUtils.RedCode + new string('█', hpFilledBoxes) +
                           new string(' ', portraitWidth - hpFilledBoxes - "HP:".Length) + ViewUtils.ResetCode;
            rightPanel.Add(hpBar);

            return rightPanel;
        }
        private List<string> CalculateMiddlePanel()
        {
            List<string> middlePanel = new List<string>();
            middlePanel.Add(string.Empty);

            if (_leftActor.Health <= 0)
            {
                middlePanel.AddRange(YOUbanner);
                middlePanel.Add(string.Empty);
                middlePanel.AddRange(LOSEbanner);
                middlePanel.Add(string.Empty);
                middlePanel.Add(string.Empty);
                middlePanel.Add("PRESS ANY KEY");
                middlePanel.Add("TO CONTINUE");

            }
            else if (_rightActor.Health <= 0)
            {
                middlePanel.AddRange(YOUbanner);
                middlePanel.Add(string.Empty);
                middlePanel.AddRange(WINbanner);
                middlePanel.Add(string.Empty);
                middlePanel.Add(string.Empty);
                middlePanel.Add("PRESS ANY KEY");
                middlePanel.Add("TO CONTINUE");
            }
            else
            {
                middlePanel.AddRange(VSbanner);
                middlePanel.Add(string.Empty);
                middlePanel.Add("CLASSIC ATTACK");
                middlePanel.Add("STEALTH ATTACK");
                middlePanel.Add("MAGICAL ATTACK");
            }
            return middlePanel;
        }
        public void Prepare()
        {
            int portraitPanelWidth = (int)(Console.WindowWidth * 0.35);
            int middlePanelWidth = Console.WindowWidth - 2*portraitPanelWidth;
            sb.Clear();
            sb.Append(ViewUtils.YellowCode);
            var leftPanel = CalculateLeftPanel();
            var rightPanel = CalculateRightPanel();
            var middlePanel = CalculateMiddlePanel();
            int longerPanel = Math.Max(leftPanel.Count, rightPanel.Count);

            for(int y = 0; y < longerPanel; y++)
            {
                string leftLine = y < leftPanel.Count ?
                    ViewUtils.GenerateStarLine(portraitPanelWidth, ViewUtils.ResetCode + leftPanel[y] + ViewUtils.YellowCode)  
                    : ViewUtils.GenerateStarLine(portraitPanelWidth,string.Empty,false,y);

                string rightLine = y < rightPanel.Count ?
                    ViewUtils.GenerateStarLine(portraitPanelWidth,ViewUtils.ResetCode + rightPanel[y] + ViewUtils.YellowCode) 
                    : ViewUtils.GenerateStarLine(portraitPanelWidth, string.Empty, false, y+200);

                string middleLine = y < middlePanel.Count ?
                    ViewUtils.GenerateStarLine(middlePanelWidth, middlePanel[y], (_session.ActiveCombat && y == 2+VSbanner.Count+_session.HighlightedAttackOtption) ? true:false) 
                    : ViewUtils.GenerateStarLine(middlePanelWidth, string.Empty, false, y+400);

                sb.Append(leftLine.VisiblePadRight(portraitPanelWidth));
                //sb.Append(new string(' ', middlePanelWidth));
                sb.Append(middleLine.VisiblePadRight(middlePanelWidth));
                sb.Append(rightLine.VisiblePadRight(portraitPanelWidth));
                sb.AppendLine();
            }
            for(int y = longerPanel; y < Console.WindowHeight-1; y++)
            {
                sb.Append(ViewUtils.GenerateStarLine(Console.WindowWidth - 1, string.Empty));
            }
        }

        public void Render()
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());
            Console.ResetColor();
        }
    }
}
