using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Ally
    {
        /// <summary>
        /// The Player Constructor
        /// </summary>
        public Player(float x, float y, float z, float speed, string name = "Actor")
            : base(x, y, z, speed, name)
        { }

        /// <summary>
        /// Updates the Player
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = 0;
            if (IsActorGrounded)
                yDirection = Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE));
            int zDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            int zRotation = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));

            //Creates a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, yDirection, zDirection);
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            //Rotate and Move Player by input
            Rotate(0, zRotation * deltaTime, 0);
            Translate(Velocity.X, Velocity.Y, Velocity.Z);

            base.Update(deltaTime);
        }

        /// <summary>
        /// Perform action based on what the Player Collided with
        /// </summary>
        /// <param name="actor">What the Player Collided wtih</param>
        public override void OnCollision(Actor actor)
        {
            //If an the Player is Ghosted and not touching a Power Up
            if (Children[1].ShapeColor.a < 255 && !(actor is PowerUp))
                return;

            if (actor is Enemy && WorldPosition.Y != 0 && !IsTagger)
            {
                SceneManager.CurrentScene.RemoveActor(this);
                Engine.Camera.CameraTarget = Scene.Actors[0];
                Engine.Camera.TargetOrigin = true;
                UIText caught = new UIText(120, 100, 50, "You Were Caught", Color.BLACK, 400, 400, 50, "You Were Caught");
                SceneManager.CurrentScene.AddUIElement(caught);
                SceneManager.RemoveAlly(this);
            }
            else if (actor is Wall && WorldPosition.Y != 0)
                Translate(-Collider.CollisionNormal.X * 5, 0, -Collider.CollisionNormal.Z * 5);

            else if (actor is PowerUp && WorldPosition.Y != 0)
                HasPowerUp = true;
        }
    }
}
