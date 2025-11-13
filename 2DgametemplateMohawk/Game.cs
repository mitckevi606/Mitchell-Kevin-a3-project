// Include the namespaces (code libraries) you need below.
using Raylib_cs;
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        float playerx = 80;
        float playery = 320;
        float movementspeed = 300;

        float gravity = 300f; // pixels/sec^2
        float objSize = 40f;

        int position1 = Random.Integer(360);
        int position2 = Random.Integer(360);
        int position3 = Random.Integer(360);
        int position4 = Random.Integer(360);

        float object1Y = -40;
        float object2Y = -70;
        float object3Y = -100;
        float object4Y = -50;

        float object1Vel = 0;
        float object2Vel = 0;
        float object3Vel = 0;
        float object4Vel = 0;

        bool gameOver = false;

        public void Setup()
        {
            Window.SetTitle("Why are you reading this???");
            Window.SetSize(400, 400);
            Window.TargetFPS = 60;
        }

        public void Update()
        {
            Window.ClearBackground(Color.Black);
            float dt = Raylib.GetFrameTime();

            if (!gameOver)
            {
                // Player movement (A, D)
                if (Raylib.IsKeyDown(KeyboardKey.A))
                    playerx -= movementspeed * dt;
                if (Raylib.IsKeyDown(KeyboardKey.D))
                    playerx += movementspeed * dt;

                // Gravity for each falling object
                object1Vel += gravity * dt;
                object2Vel += gravity * dt;
                object3Vel += gravity * dt;
                object4Vel += gravity * dt;

                object1Y += object1Vel * dt;
                object2Y += object2Vel * dt;
                object3Y += object3Vel * dt;
                object4Y += object4Vel * dt;

                float groundLevel = 360 - objSize;

                // Reset objects when they hit the ground
                if (object1Y >= groundLevel) { object1Y = -40; object1Vel = 0; }
                if (object2Y >= groundLevel) { object2Y = -40; object2Vel = 0; }
                if (object3Y >= groundLevel) { object3Y = -40; object3Vel = 0; }
                if (object4Y >= groundLevel) { object4Y = -40; object4Vel = 0; }

                // Collision detection
                Rectangle playerRect = new Rectangle(playerx, playery, 40, 40);
                Rectangle obj1Rect = new Rectangle(200, object1Y, objSize, objSize);
                Rectangle obj2Rect = new Rectangle(300, object2Y, objSize, objSize);
                Rectangle obj3Rect = new Rectangle(150, object3Y, objSize, objSize);
                Rectangle obj4Rect = new Rectangle(50, object4Y, objSize, objSize);

                if (Raylib.CheckCollisionRecs(playerRect, obj1Rect) ||
                    Raylib.CheckCollisionRecs(playerRect, obj2Rect) ||
                    Raylib.CheckCollisionRecs(playerRect, obj3Rect) ||
                    Raylib.CheckCollisionRecs(playerRect, obj4Rect))
                {
                    gameOver = true;
                }

                // Draw player and objects
                Raylib.DrawRectangle((int)playerx, (int)playery, 40, 40, Color.Green);
                Raylib.DrawRectangle(0, 360, 400, 20, Color.White);
                Raylib.DrawRectangle(position1, (int)object1Y, (int)objSize, (int)objSize, Color.Red);
                Raylib.DrawRectangle(position2, (int)object2Y, (int)objSize, (int)objSize, Color.Red);
                Raylib.DrawRectangle(position3, (int)object3Y, (int)objSize, (int)objSize, Color.Red);
                Raylib.DrawRectangle(position4, (int)object4Y, (int)objSize, (int)objSize, Color.Red);
            }
            else
            {
                // Game Over
                Raylib.DrawText("GAME OVER", 100, 150, 40, Color.Red);
                Raylib.DrawText("Press R to Restart", 100, 200, 20, Color.White);

                if (Raylib.IsKeyPressed(KeyboardKey.R))
                {
                    RestartGame();
                }
            }
        }

        void RestartGame()
        {
            playerx = 80;
            playery = 320;
            object1Y = -40;
            object2Y = -70;
            object3Y = -100;
            object4Y = -50;
            object1Vel = object2Vel = object3Vel = object4Vel = 0;
            gameOver = false;
        }
    }

}
