using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Ally : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private Enemy _target;
        private bool _isTagger = false;
        private bool _hasPowerUp = false;
        private float _powerUpTimer = 0;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        /// <summary>
        /// True if the Ally is a Tagger
        /// </summary>
        public bool IsTagger
        {
            get { return _isTagger; }
            set { _isTagger = value; }
        }

        /// <summary>
        /// True if the Ally has a PowerUp
        /// </summary>
        public bool HasPowerUp
        {
            get { return _hasPowerUp; }
            set { _hasPowerUp = value; }
        }

        /// <summary>
        /// The Base Ally Constructor
        /// </summary>
        /// <param name="speed">The Spped of the Ally</param>
        public Ally(float x, float y, float z, float speed, string name = "Actor")
            : base(x, y, z, name)
        {
            _speed = speed;
            Forward = new Vector3(0, 0, 1);
        }

        /// <summary>
        /// Initializes the Ally
        /// </summary>
        public override void Start()
        {
            Actor head = new Actor(0, 0.85f, 0, "Head", Shape.SPHERE);
            head.SetScale(0.5f, 0.5f, 0.5f);
            head.Collider = new SphereCollider(head);
            head.SetColor(255, 100, 100, 255);
            AddChild(head);

            Actor body = new Actor(0, 0, 0, "Body", Shape.CUBE);
            body.SetScale(0.75f, 1, 0.75f);
            Collider = new AABBCollider(this);
            Collider = new AABBCollider(0.75f, 1, 0.75f, this);
            body.SetColor(10, 10, 255, 255);
            AddChild(body);

            base.Start();
        }

        /// <summary>
        /// Updates the Ally
        /// </summary>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //If they are to far away from the play area
            if (WorldPosition.X >= 145 || WorldPosition.Z >= 145 || WorldPosition.X <= -145 || WorldPosition.Z <= -145)
                //Return them to the center of the map
                SetTranslation(0, 0.5f, 0);

            //If an ALLY is a tagger
            if (IsTagger && !(this is Player))
            {
                //If a target is close enough to tag
                if (GetTargetOffense())
                {
                    //Chase them
                    Vector3 moveDirection = (_target.WorldPosition - WorldPosition).Normalized;
                    Forward = moveDirection;
                    Velocity = moveDirection * Speed * deltaTime;
                    Translate(Velocity);
                }
                else
                {
                    //Wander and check if running into wall
                    for (int i = 0; i < Scene.Actors.Length; i++)
                        if (Scene.Actors[i] is Wall)
                        {
                            Wall checkWall = (Wall)Scene.Actors[i];
                            checkWall.CheckMovement(this);
                        }
                    Velocity = Forward * Speed * deltaTime;
                    Translate(Velocity);
                }
            }
            //If an ALLY is not a Tagger
            else if (!IsTagger && !(this is Player))
            {
                //Check if they are in danger of being tagged
                if (GetTargetDefense())
                {
                    //Check if running into wall
                    for (int i = 0; i < Scene.Actors.Length; i++)
                        if (Scene.Actors[i] is Wall)
                        {
                            Wall checkWall = (Wall)Scene.Actors[i];
                            checkWall.CheckMovement(this);
                        }
                    //Flee from enemy
                    Vector3 moveDirection = new Vector3(-(_target.WorldPosition.X - WorldPosition.X), 0, -(_target.WorldPosition.Z - WorldPosition.Z)).Normalized;
                    Forward = moveDirection;
                    Velocity = moveDirection * Speed * deltaTime;
                    Translate(Velocity);
                }
                else
                {
                    //Wander and check if running into wall
                    for (int i = 0; i < Scene.Actors.Length; i++)
                        if (Scene.Actors[i] is Wall)
                        {
                            Wall checkWall = (Wall)Scene.Actors[i];
                            checkWall.CheckMovement(this);
                        }
                    Velocity = Forward * Speed * deltaTime;
                    Translate(Velocity);
                }
            }

            //If an Ally Picked up a power up
            if (_hasPowerUp)
            {
                //Check how long they have been powered up for
                _powerUpTimer += deltaTime;
                //if the Player has been powered up for too long
                if (_powerUpTimer >= 20 && this is Player)
                {
                    //reset them
                    SetScale(1, 1, 1);
                    Children[0].SetScale(1, 1, 1);
                    Children[1].SetScale(0.5f, 0.5f, 0.5f);
                    Children[1].SetColor(255, 100, 100, 255);
                    Children[2].SetScale(0.75f, 1, 0.75f);
                    Children[2].SetColor(10, 10, 255, 255);
                    Speed = 20;
                    _powerUpTimer = 0;
                    _hasPowerUp = false;
                }
                //If an ally has powered up for too long
                else if (_powerUpTimer >= 15 && !(this is Player))
                {
                    //Reset them
                    SetScale(1, 1, 1);
                    Children[0].SetScale(0.5f, 0.5f, 0.5f);
                    Children[0].SetColor(255, 100, 100, 255);
                    Children[1].SetScale(0.75f, 1, 0.75f);
                    Children[1].SetColor(10, 10, 255, 255);
                    Speed = 10;
                    _powerUpTimer = 0;
                    _hasPowerUp = false;
                }

                //If ally in in the Air
                if (!IsActorGrounded)
                    //Apply Gravity
                    Acceleration -= new Vector3(0, 0.00981f, 0);
            }

                //If the Actor falls below the Ground
                if (WorldPosition.Y < 0.5f && WorldPosition.Y != 0)
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
        }

        /// <summary>
        /// Get the target and sees if they are in range
        /// </summary>
        /// <returns>If the ally should approach the target</returns>
        public bool GetTargetOffense()
        {
            //Gets a the closest target that can be tagged
            for (int i = 0; i < SceneManager.Enemies.Length; i++)
                if(!SceneManager.Enemies[i].IsTagger)
                    if(_target == null)
                        _target = SceneManager.Enemies[i];
                else if(Vector3.Distance(_target.WorldPosition, WorldPosition) > Vector3.Distance(SceneManager.Enemies[i].WorldPosition, WorldPosition))
                            _target = SceneManager.Enemies[i];
            
            //If there is a target
            if (_target != null)
            {
                //Check the direction and distance of the target
                Vector3 directionOfTarget = (WorldPosition - _target.WorldPosition).Normalized;

                //Return if the Ally is facing the target, and if they are close enough to the target
                return (Vector3.GetRadian(directionOfTarget, Forward) > 2 & Vector3.Distance(_target.WorldPosition, WorldPosition) < 200);
            }

            //Return false by default
            return false;
        }

        /// <summary>
        /// Gets the closet target that can tag them
        /// </summary>
        /// <returns>If the ally is in danger of being tagged</returns>
        public bool GetTargetDefense()
        {
            //Gets a the closest target that can be tagged
            for (int i = 0; i < SceneManager.Enemies.Length; i++)
                if (SceneManager.Enemies[i].IsTagger)
                    if (_target == null)
                        _target = SceneManager.Enemies[i];
                    else if (Vector3.Distance(_target.WorldPosition, WorldPosition) > Vector3.Distance(SceneManager.Enemies[i].WorldPosition, WorldPosition))
                        _target = SceneManager.Enemies[i];

            //If there is a target
            if (_target != null)
            {
                //Check the direction of the target
                Vector3 directionOfTarget = (WorldPosition - _target.WorldPosition).Normalized;

                //Return if the Ally is facing the target, and if they are close enough to the target
                return (Vector3.GetRadian(directionOfTarget, Forward) > 2 & Vector3.Distance(_target.WorldPosition, WorldPosition) < 200);
            }

            //Return false by default
            return false;
        }

        /// <summary>
        /// Performs actions based on what the actor collided with
        /// </summary>
        /// <param name="actor">What the Actor Collided with</param>
        public override void OnCollision(Actor actor)
        {
            //If an Ally is Ghosted and not touching a Power Up
            if (Children[0].ShapeColor.a < 255 && !(actor is PowerUp))
                return;

            //If collided with an enemy while the ally is not a tagger
            if (actor is Enemy && WorldPosition.Y != 0 && !IsTagger)
            {
                //The are remove from the scene and allies
                SceneManager.CurrentScene.RemoveActor(this);
                SceneManager.RemoveAlly(this);
            }
            else if (actor is Ally && WorldPosition.Y != 0)
                Translate(-Collider.CollisionNormal.X, 0, -Collider.CollisionNormal.Z);
            //IF collided with a wall
            else if (actor is Wall && WorldPosition.Y != 0)
            {
                //Turn them around
                Translate(-Forward.X * 2, 0, -Forward.Z * 2);
                Forward = new Vector3(-Forward.X, 0, -Forward.Z);
            }
            //If collided with a power up
            else if (actor is PowerUp)
                _hasPowerUp = true;
        }
    }
}
