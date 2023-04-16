using BombAttackGame.Collisions;
using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BombAttackGame.Bonuses
{
    internal class MainSpeed : IGameObject
    {
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public double WalkSpeed { get; set; }
        public double ShootSpeed { get; set; }
        public double ShootingSpeed { get; set; }
        public double BonusTime { get; set; }
        public double SpawnStartTime { get; set; }
        public double SpawnEndTime { get; set; }
        public double NextSpawnTime { get; set; }
        public double FinallySpawnTime { get; set; }
        public bool IsDead { get; set; }
        public Rectangle Rectangle { get; set; }

        public MainSpeed()
        {
            this.WalkSpeed = 2;
            this.Color = Color.Tomato;
            this.ShootSpeed = 2;
            this.ShootingSpeed = 2;
            this.IsDead = true;
            this.BonusTime = 10000;
            this.SpawnStartTime = 8000;
            this.SpawnEndTime = 16000;
            this.FinallySpawnTime = 0;
        }
        public void Tick(GameTime GameTime, MainSpeed MainSpeed, int[] MapSize)
        {
            UpdateCollision(MainSpeed);
        }
        private static void UpdateCollision(MainSpeed MainSpeed)
        {
            //MainSpeed.Collision = VectorTool.Collision(MainSpeed.Location, MainSpeed.Texture);
        }
        public static void Kill(MainSpeed MainSpeed)
        {
            MainSpeed.IsDead = true;
            MainSpeed.Location = new Vector2(-100000, -100000);
        }
        public static void PickedBonus(Player Player, MainSpeed MainSpeed, GameTime GameTime)
        {
            //if (Player.OnMainSpeed) return;
            //Player.Speed *= MainSpeed.WalkSpeed;
            //Player.ShotLatency /= MainSpeed.ShootingSpeed;
            //Player.MainSpeedEndTime = GameTime.TotalGameTime.TotalMilliseconds + MainSpeed.BonusTime;
            //Player.OnMainSpeed = true;
            MainSpeed.Kill(MainSpeed);
        }
        public static void SpawnRandomly(GameTime GameTime, MainSpeed MainSpeed, int[] MapSize, List<Rectangle> Collision)
        {
            if(GameTime.TotalGameTime.TotalMilliseconds >= MainSpeed.FinallySpawnTime && MainSpeed.FinallySpawnTime != 0)
            {
                MainSpeed.IsDead = false;
                MainSpeed.Location = Spawn.GenerateRandomSpawnPoint(MapSize, MainSpeed.Texture, Collision);
                MainSpeed.FinallySpawnTime = 0;
            }
            if (MainSpeed.FinallySpawnTime != 0) return;
            Random random = new Random();
            int StartSec = Convert.ToInt32(MainSpeed.SpawnStartTime / 1000);
            int EndSec = Convert.ToInt32(MainSpeed.SpawnEndTime / 1000);
            int FinallySec = random.Next(StartSec,EndSec);
            double FinnalyMil = random.NextDouble();
            MainSpeed.FinallySpawnTime = GameTime.TotalGameTime.TotalMilliseconds + FinallySec*1000 + FinnalyMil;
        }
    }
}
