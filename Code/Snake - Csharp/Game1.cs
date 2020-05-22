
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeGame
{

 
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D SnakeTop;
        Body Snake1;
        Food Food = new Food();

        Boolean GameOver;
        static public int gameScore;
  

        private int xPosition;
        private int yPosition;

        Rectangle SnakeHead;
        Rectangle FoodPosition;
        private SpriteFont font;

        Direction direction;
        Direction newDirection;

        int[] movement = new int[2];



        List<Body> BodyParts = new List<Body>();

        Vector2 screenSize = new Vector2(800, 600);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = (int)screenSize.Y,
                PreferredBackBufferWidth = (int)screenSize.X
            }
            ;

            Content.RootDirectory = "Assets";

            graphics.IsFullScreen = false;

            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(75);

        }

        protected override void Initialize()
        {
            BodyParts.Clear();
            Snake1 = new Body(120, 400);
            BodyParts.Add(Snake1);
            Snake1.SnakeCreate(BodyParts);

            FoodPosition = Food.GenerateFood(BodyParts);

            direction = Direction.RIGHT;
            newDirection = Direction.RIGHT;
            
            base.Initialize();

        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

            SnakeTop = Content.Load<Texture2D>("Snake");
            font = Content.Load<SpriteFont>("Arial");
        }

        void Reset()
        {
            direction = Direction.RIGHT;
            newDirection = Direction.RIGHT;
            GameOver = false;
            gameScore = 0;
            BodyParts.Clear();
            Snake1 = new Body(120, 400);
            BodyParts.Add(Snake1);
            Snake1.SnakeCreate(BodyParts);
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState Key = Keyboard.GetState();
            
            if (Key.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Key.IsKeyDown(Keys.Space))
            {
                Reset();
            }
     
            if (Food.EatFood(SnakeHead, FoodPosition) == true)
            { 
                FoodPosition = Food.GenerateFood(BodyParts); 
                Snake1.GrowSnake(BodyParts);
                
                gameScore++;
                
            }

            if ((Key.IsKeyDown(Keys.Up) && direction != Direction.DOWN))
            {
                newDirection = Direction.UP;
            }
            if ((Key.IsKeyDown(Keys.Down) && direction != Direction.UP))
            {
                newDirection = Direction.DOWN;
            }
            if ((Key.IsKeyDown(Keys.Left) && direction != Direction.RIGHT))
            {
                newDirection = Direction.LEFT;
            }
            if ((Key.IsKeyDown(Keys.Right) && direction != Direction.LEFT))
            {
                newDirection = Direction.RIGHT;
            }

            xPosition = movement[0];
            yPosition = movement[1];

            if (Snake1.XPosition % 40 == 0 && Snake1.YPosition % 40 == 0)
            {
                direction = newDirection;                                    
                Snake1.LastXPosition = Snake1.XPosition;
                Snake1.LastYPosition = Snake1.YPosition;
            }

            if (GameOver == false)
            { 
                movement = Snake1.MoveHead((int)direction);
                GameOver = Snake1.SnakeCollision(BodyParts);

                Snake1.LastXPosition = Snake1.XPosition;
                Snake1.LastYPosition = Snake1.YPosition;

                Snake1.MoveBody(BodyParts);
            }
            
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            
            graphics.GraphicsDevice.Clear(Color.Black);
            Rectangle SnakeRectangle = new Rectangle(10, 10, 10, 10);
            Rectangle FoodRectangle = new Rectangle(10, 10, 10, 10);

            spriteBatch.Begin();

           
            spriteBatch.Draw(SnakeTop, SnakeHead, SnakeRectangle, Color.Red);

            SnakeHead = new Rectangle(BodyParts[0].XPosition, BodyParts[0].YPosition, 40, 40);

            Vector2 scorePos;
            scorePos.X = screenSize.X / 2;
            scorePos.Y = screenSize.Y - 580;

            spriteBatch.DrawString(font, gameScore.ToString(), scorePos, Color.White);

            for (int i = 1; i < BodyParts.Count; i++)
            { 
                Rectangle BodySquare = new Rectangle(BodyParts[i].XPosition, BodyParts[i].YPosition, 40, 40);
                spriteBatch.Draw(SnakeTop, BodySquare, SnakeRectangle, Color.Red);
            }

            spriteBatch.Draw(SnakeTop, FoodPosition, FoodRectangle, Color.LimeGreen); 
            
            if (GameOver)
            {
                Vector2 stringPos;
                stringPos.X = screenSize.X / 2;
                stringPos.Y = 50;
                spriteBatch.DrawString(font, "PRESS SPACE TO RESTART", stringPos, Color.Yellow);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
    enum Direction
    {
        UP = 1,
        RIGHT = 2,
        DOWN = 3,
        LEFT = 4
    }
}