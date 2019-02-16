﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Inkcorperated
{
    class Map
    {
        private Rectangle playerStart;
        private List<Block> map;
        private List<Enemy> enemies;
        private int inkLimit;
        private Rectangle goal;

        public Rectangle PlayerStart
        {
            get
            {
                return playerStart;
            }
        }

        public Rectangle Goal
        {
            get
            {
                return goal;
            }
        }

        public int InkLimit
        {
            get
            {
                return inkLimit;
            }
        }

        public Map(Rectangle playerStart, int inkLimit, Rectangle goal){
            this.playerStart = playerStart;
            this.inkLimit = inkLimit;
            this.goal = goal;
            map = new List<Block>();
            enemies = new List<Enemy>();
        }

        public void AddBlock(Block block){
            map.Add(block);
        }

        public void AddEnemy(Enemy enemy){
            enemies.Add(enemy);
        }

        /// <summary>
        /// Checks if an existing object intersects with anything in the map
        /// </summary>
        public bool IntersectsWithExisting(Drawable other){
            for(int i = 0; i < map.Count; i++)
            {
                if (other.Bounds.Intersects(map[i].Bounds))
                {
                    return true;
                }
            }

            for(int i = 0; i < enemies.Count; i++)
            {
                if (other.Bounds.Intersects(enemies[i].Bounds))
                {
                    return true;
                }
            }

            if (other.Bounds.Intersects(goal))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Draws all of the default blocks and enemies to the specified SpriteBatch
        /// </summary>
        public void Draw(SpriteBatch batch){
            foreach(Block b in map){
                b.Draw(batch, Color.White);
            }
            foreach(Enemy e in enemies){
                e.Draw(batch, Color.White);
            }
        }
    }
}
