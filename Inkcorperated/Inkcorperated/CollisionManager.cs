﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkcorperated
{
	class CollisionManager
	{
		// Fields
		private MapController controller;
		private List<Block> allBlocks;

		// Properties
		
		// Constructor

		public CollisionManager(MapController controller)
		{
			this.controller = controller;
			allBlocks = new List<Block>();
		}

		// Methods

		/// <summary>
		/// Checks if the two Drawable objects are colliding
		/// </summary>
		/// <param name="objOne">The first object</param>
		/// <param name="objTwo">The second object</param>
		/// <returns>Returns if they are colliding</returns>
		public bool isColliding(Drawable objOne, Drawable objTwo)
		{
			if (objOne.Bounds.Intersects(objTwo.Bounds))
				return true;
			else
				return false;
		}

		public void Colliding()
		{
            allBlocks = new List<Block>();

			// Player is always falling, unless otherwise stated
			controller.LevelPlayer.Falling = true; 

			// 1 pixel "line" below the player
			// If THIS is colliding, but the player is not, the player does not get moved
			// (removes the "bouncing" of the player being moved back up off the floor every frame)
			Rectangle bounceCheck = new Rectangle(controller.LevelPlayer.X, controller.LevelPlayer.Y + controller.LevelPlayer.Height, controller.LevelPlayer.Width, 1);

			// Adds all drawn blocks and preset blocks to a 
			// third list of all blocks on the screen
			allBlocks.AddRange(controller.CustomBlocks);
			allBlocks.AddRange(controller.GetCurrentMap().MapBlocks);

			// Checks collisions between the player and each block on the screen
			foreach (Block block in allBlocks)
			{
				if (controller.LevelPlayer.Bounds.Intersects(block.Bounds))
				{
					Rectangle intersection = Rectangle.Intersect(controller.LevelPlayer.Bounds, block.Bounds);

					// Collision is occuring on top or bottom
					if (intersection.Width > intersection.Height)
					{
						// Moves the player up or down away from the block
						controller.LevelPlayer.Y -= intersection.Height * Math.Sign(block.Bounds.Y - controller.LevelPlayer.Y);
						controller.LevelPlayer.YVelocity = 0;
					}
					// Collision is occuring on either the right or left
					else
					{
						// Moves the player left or right away from the block
						controller.LevelPlayer.X -= intersection.Width * Math.Sign(block.Bounds.X - controller.LevelPlayer.X);
					}
				}
				else if(block.Bounds.Intersects(bounceCheck) && controller.LevelPlayer.YVelocity >= 0)
				{
					controller.LevelPlayer.Falling = false;
					controller.LevelPlayer.YVelocity = 0;
				}
			}

			// Checks for win condition (if player collides with the goal flag)
			if(isColliding(controller.LevelPlayer, controller.Goal))
			{
				// moves to next level
			}
		}
	}
}
