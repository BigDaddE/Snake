using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Body
    {

        Boolean collide = false;

       
        public int XPosition
        {
            get;
            set;
        }
        public int LastXPosition
        {
            get;
            set;
        }
        public int YPosition
        {
            get;
            set;
        }
        public int LastYPosition
        {
            get;
            set;
        }
        public int BodyLength
        {
            get;
            set;
        }

      
        public Body(int x, int y)
        {
            this.XPosition = x;
            this.YPosition = y;
        }

        public void MakeSnake(List<Body> BodyParts)
        {
            int x = BodyParts[0].XPosition;
            int y = BodyParts[0].YPosition;
            for (int i = 0; i <= 3; i++)
            {
                Body TempPart = new Body(x - (40 * i), y - (40 * 1));
                BodyParts.Add(TempPart);
            }
        }

        public int[] MoveHead(int direction) 
        {
            int[] movement = new int[2];
            if (direction == 1)
            { 
                movement[0] = this.XPosition -= 0;
                movement[1] = this.YPosition -= 40;
                return movement;
            }
            else if (direction == 2)
            {
                movement[0] = this.XPosition += 40;
                movement[1] = this.YPosition -= 0;
                return movement;
            }
            else if (direction == 3)
            {
                movement[0] = this.XPosition -= 0;
                movement[1] = this.YPosition += 40;
                return movement;
            }
            else if (direction == 4)
            { 
                movement[0] = this.XPosition -= 40;
                movement[1] = this.YPosition -= 0;
                return movement;
            }
            return movement;

        }

        public void MoveBody(List<Body> BodyParts) 
        {                                              
            for (int i = 1; i < BodyParts.Count; i++)
            {
                BodyParts[i].LastXPosition = BodyParts[i].XPosition;
                BodyParts[i].LastYPosition = BodyParts[i].YPosition;
                BodyParts[i].XPosition = BodyParts[i - 1].LastXPosition;
                BodyParts[i].YPosition = BodyParts[i - 1].LastYPosition;
            }
        }

        public Boolean SnakeCollision(List<Body> BodyParts)
        { 
            for (int i = 1; i < BodyParts.Count; i++)
            { 
                Boolean SnakeCollide = (BodyParts[0].XPosition == BodyParts[i].XPosition) && (BodyParts[0].YPosition == BodyParts[i].YPosition);
                Boolean WallCollide = (BodyParts[0].XPosition < 0 || BodyParts[0].XPosition > 760 || BodyParts[0].YPosition < 40 || BodyParts[0].YPosition > 560);
                if (SnakeCollide || WallCollide)
                {
                    collide = true;
                    break;
                }
                else
                {
                    collide = false;
                }
            }
            return collide;
        }

        public void GrowSnake(List<Body> BodyParts) 
        {                                                
            int length = BodyParts.Count - 1;
            int x = BodyParts[length].XPosition;
            int y = BodyParts[length].YPosition;

            BodyParts.Add(new Body(x, y));
        }
        
    }
}
