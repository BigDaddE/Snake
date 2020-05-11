using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SnakeGame
{
    public class Food
    {
        Random r = new Random();
        int x;
        int y;

        public int XPosition
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int YPosition
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public Rectangle GenerateFood(List<Body> BodyParts)
        {
            Boolean FreeSquare = false;
            while (!FreeSquare)
            {
                x = r.Next(1, 18) * 40; // 40 för att varje block är 40px hög och bred
                y = r.Next(1, 13) * 40;

                if (BodyParts.Exists(part => (part.XPosition == x && part.YPosition == y)))
                {
                }
                else
                {
                    FreeSquare = true;
                    break;
                }
            }
            return new Rectangle(x, y, 40, 40);
        }

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
    }
}