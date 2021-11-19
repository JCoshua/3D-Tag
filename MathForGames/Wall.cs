using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Wall : Actor
    {
        /// <summary>
        /// The Collider that Allies and Enemies use to check for Rotation
        /// </summary>
        public AABBCollider RotationCollider
        {
            get {
                    AABBCollider rotationCollider = (AABBCollider)Collider;
                    return new AABBCollider(rotationCollider.Width + 50, rotationCollider.Height, rotationCollider.Length + 50, this);
                }
        }

        /// <summary>
        /// The Wall Constructor
        /// </summary>
        /// <param name="length">The Length of the Wall</param>
        /// <param name="height">The Height of the Wall</param>
        /// <param name="width">The width of the Wall</param>
        /// <param name="scene">The Scene to add the Wall to</param>
        public Wall(float posX, float posY, float posZ, float length, float height, float width, Scene scene) : base(posX, posY, posZ, "Wall", Shape.CUBE)
        {
            SetScale(length, height, width);
            scene.AddActor(this);
            Collider = new AABBCollider(width, height, length, this);
            SetColor(Color.GRAY);
        }

        /// <summary>
        /// Check if an Actor should rotate to avoid hitting the Wall.
        /// </summary>
        /// <param name="actor">The Actor to check</param>
        public void CheckMovement(Actor actor)
        {
            if (RotationCollider.CheckCollision(actor.Children[0]) && WorldPosition.Y != 0)
            {
                if((actor.Forward.Z + actor.WorldPosition.Z) - WorldPosition.Z > actor.WorldPosition.Z - WorldPosition.Z ||
                   (actor.Forward.X + actor.WorldPosition.X) - WorldPosition.X > actor.WorldPosition.X - WorldPosition.X)
                //Rotate
                actor.Rotate(0, 0.02f, 0);
            }
        }
    }
}
