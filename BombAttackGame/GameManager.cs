using BombAttackGame.Enums;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame
{
    internal class GameManager
    {
        private readonly List<IGameObject> _gameObjects;
        public int MaxRounds { get; private set; }
        public int CTWinRounds { get; private set; }
        public int TTWinRounds { get; private set; }
        public Enums.Events Event { get; private set; }
        public int TeamMatesCount { get; private set; }
        public int EnemyCount { get; private set; }
        public static int PlayerSpeed = 2;
        public static int SheriffLatency = 200;
        public static int BulletSpeed = 5;
        public static int BotMovingTime = 1000;
        public static int BotNothingChange = 90;
        public static int BotGunChance = 5;
        public static int BotGrenadeChance = 1;
        
        public GameManager(List<IGameObject> gameObjects)
        {
            this.MaxRounds = 30;
            this._gameObjects = gameObjects;
            this.TeamMatesCount = 4;
            this.EnemyCount = 5;
        }
        public void Process()
        {
            var playersCT = new List<IGameObject>();
            var playersTT = new List<IGameObject>();
            playersCT.AddRange(this._gameObjects.OfType<Player>().Where(x => x.Team == Team.TeamMate));
            playersTT.AddRange(this._gameObjects.OfType<Player>().Where(x => x.Team == Team.Enemy));
            if (playersCT.Count == 0)
            {
                TTWin();
                return;
            }
            if (playersCT.Where(x => x.IsHuman).FirstOrDefault() == null)
            {
                TimeLapse();
            }
            if (playersTT.Count == 0) {
                CTWin();
                return;
            }
            CheckRounds();
        }
        private void CTWin()
        {
            this.CTWinRounds += 1;
            this.Event = Enums.Events.EndRound;
        }
        private void TTWin()
        {
            this.TTWinRounds += 1;
            this.Event = Enums.Events.EndRound;
        }
        private void CheckRounds()
        {
            if(this.CTWinRounds + this.TTWinRounds == this.MaxRounds)
            {
                this.Event = Enums.Events.EndGame;
            }
        }
        private void TimeLapse()
        {
            this.Event = Enums.Events.TimeLapse;
        }

        public void ResetEvent()
        {
            this.Event = Enums.Events.None;
        }
    }
}
