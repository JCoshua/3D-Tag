using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Ally
    {
        public Player(float x, float y, float z, float speed, string name = "Actor")
            : base(x, y, z, speed, name)
        {

        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = 0;
            if (IsActorGrounded)
                yDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE));
            int zDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            int zRotation = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            //Creates a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, yDirection, zDirection);
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Rotate(0, zRotation * deltaTime, 0);
            Translate(Velocity.X, Velocity.Y, Velocity.Z);

            if(Size.Y < 10)
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnCollision(Actor other)
        {
            if(other is Wall)
            {
                Console.WriteLine("Collision Normal: " + Collider.CollisionNormal.X + ", " + Collider.CollisionNormal.Y + ", " + Collider.CollisionNormal.Z);
            }


            base.OnCollision(other);
        }
    }
}
