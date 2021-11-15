using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Wall : Actor
    {
        public AABBCollider RotationCollider
        {
            get {
                    AABBCollider rotationCollider = (AABBCollider)Collider;
                    return new AABBCollider(rotationCollider.Width + 50, rotationCollider.Height, rotationCollider.Length + 50, this);
                }
        }

        public Wall(float posX, float posY, float posZ, float lenght, float height, float width, Scene scene) : base(posX, posY, posZ, "Wall", Shape.CUBE)
        {
            SetScale(lenght, height, width);
            scene.AddActor(this);
            Collider = new AABBCollider(this);
            SetColor(Color.GRAY);
        }

        public void CheckMovement(Actor actor)
        {
            if (RotationCollider.CheckCollision(actor.Children[0]))
            {
                if((actor.Forward.Z + actor.WorldPosition.Z) - WorldPosition.Z > actor.WorldPosition.Z - WorldPosition.Z ||
                   (actor.Forward.X + actor.WorldPosition.X) - WorldPosition.X > actor.WorldPosition.X - WorldPosition.X)
                //Rotate
                actor.Rotate(0, 0.02f, 0);
            }
        }
    }
}
