using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Ally : Actor
    {
        public Ally(float x, float y, float z, string name = "Ally", Shape shape = Shape.NONE) : base(x, y, z, name, shape)
        {

        }

        public override void Start()
        {
            Actor head = new Actor(0, 0.85f, 0, "Head", Shape.SPHERE);
            head.SetScale(0.5f, 0.5f, 0.5f);
            head.Collider = new SphereCollider(head);
            head.SetColor(255, 100, 100, 255);
            AddChild(head);

            Actor body = new Actor(0, 0, 0, "Body", Shape.CUBE);
            body.SetScale(0.75f, 1, 0.5f);
            body.Collider = new AABBCollider(body);
            body.SetColor(10, 10, 255, 255);
            AddChild(body);
            
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            if (WorldPosition.Y <= 0.5f)
            {
                //Sets the Vertical Acceleration to 0
                Acceleration = new Vector3(Acceleration.X, 0, Acceleration.Z);
                //Set their position to be on the Ground
                SetTranslation(WorldPosition.X, 0.5f, WorldPosition.Z);
                //Declare them grounded
                IsActorGrounded = true;
            }
            else
                //They are not grounded
                IsActorGrounded = false;

            base.Update(deltaTime);
        }
    }
}
