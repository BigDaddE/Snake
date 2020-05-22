using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGame
{
    public class Food
    {
        Random rand = new Random();
        int x;
        int y;
        public int XPosition;
        public int YPosition;

        public Boolean EatFood(Rectangle Snake, Rectangle Food)
        {
            if (Snake.Contains(Food))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Rectangle GenerateFood(List<Body> BodyParts)
        {
            Boolean OccupiedSquare = true;
            while (OccupiedSquare)
            {
                x = rand.Next(1, 18) * 40; // 40 för att varje block är 40px hög och bred
                y = rand.Next(1, 13) * 40;

                if (!BodyParts.Exists(part => (part.XPosition == x && part.YPosition == y)))
                {
                    OccupiedSquare = false;
                    break;
                }
               
            }
            return new Rectangle(x, y, 40, 40);
        }

    }
}