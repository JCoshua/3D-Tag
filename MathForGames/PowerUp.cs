using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    public enum ItemType
    {
        SIZEUP,
        SIZEDOWN,
        GHOST,
        SPEEDUP,
        SPEEDDOWN,
<<<<<<< HEAD
=======
        SHEILD
>>>>>>> RayLib3D
    }

    class PowerUp : Actor
    {
        private ItemType _itemType;
        public PowerUp(float x, float y, float z, ItemType itemType) : base(x, y, z, "Power Up")
        {
            _itemType = itemType;
        }

        public override void Start()
        {
            CreateSprite();
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            switch (_itemType)
            {
                case ItemType.SIZEUP:
                    if (Children[0].Size.Y <= 1)
                        Children[0].Scale(1.025f);
                    else if (Children[0].Size.Y >= 1)
                        Children[0].SetScale(0.5f, 0.5f, 0.5f);
                    break;
<<<<<<< HEAD
                case ItemType.SIZEDOWN:
                    if (Children[0].Size.Y >= 0.5f)
                        Children[0].Scale(0.975f);
                    else if (Children[0].Size.Y <= 0.5f)
                        Children[0].SetScale(1, 1, 1);
                    break;
                case ItemType.SPEEDUP:
                    Rotate(0, 0.2f, 0);
                    break;
                case ItemType.SPEEDDOWN:
                    Rotate(0, 0.002f, 0);
                    break;
                default:
                    break;
=======
>>>>>>> RayLib3D
            }

            base.Update(deltaTime);
        }

        public void CreateSprite()
        {
<<<<<<< HEAD
            switch (_itemType)
=======
            switch(_itemType)
>>>>>>> RayLib3D
            {
                case ItemType.SIZEUP:
                    Actor sprite = new Actor(0, 0, 0, "Sprite", Shape.CUBE);
                    sprite.SetScale(0.5f, 0.5f, 0.5f);
                    sprite.SetColor(20, 0, 0, 255);
                    sprite.Collider = new AABBCollider(sprite);
                    AddChild(sprite);
                    break;
<<<<<<< HEAD
                case ItemType.SIZEDOWN:
                    sprite = new Actor(0, 0, 0, "Sprite", Shape.CUBE);
                    sprite.SetScale(1, 1, 1);
                    sprite.SetColor(0, 0, 20, 255);
                    sprite.Collider = new AABBCollider(sprite);
                    AddChild(sprite);
                    break;
                case ItemType.GHOST:
                    Actor head = new Actor(0, 0.425f, 0, "Head", Shape.SPHERE);
                    Actor body = new Actor(0, 0, 0, "Body", Shape.CUBE);
                    head.SetScale(0.25f, 0.25f, 0.25f);
                    body.SetScale(0.375f, 0.5f, 0.375f);
                    head.SetColor(255, 100, 100, 100);
                    body.SetColor(10, 10, 255, 100);
                    head.Collider = new SphereCollider(head);
                    body.Collider = new AABBCollider(body);
                    AddChild(body);
                    AddChild(head);
                    break;
                case ItemType.SPEEDUP:
                    sprite = new Actor(0, 0, 1, "Sprite", Shape.SPHERE);
                    sprite.SetScale(1, 1, 1);
                    sprite.SetColor(20, 0, 0, 255);
                    sprite.Collider = new AABBCollider(sprite);
                    AddChild(sprite);
                    break;
                case ItemType.SPEEDDOWN:
                    sprite = new Actor(0, 0, 1, "Sprite", Shape.SPHERE);
                    sprite.SetScale(1, 1, 1);
                    sprite.SetColor(20, 0, 0, 255);
                    sprite.Collider = new AABBCollider(sprite);
                    AddChild(sprite);
                    break;
            }
        }

        public override void OnCollision(Actor actor)
        {
            if(actor.Parent != null)
                return;

            switch (_itemType)
            {
                case ItemType.SIZEUP:
                    actor.Scale(2);
                    for(int i = 0; i < actor.Children.Length; i++)
                    {
                        actor.Children[i].Scale(2);
                    }
                    SceneManager.CurrentScene.RemoveActor(this);
                    break;
                case ItemType.SIZEDOWN:
                    actor.Scale(0.5f);
                    for (int i = 0; i < actor.Children.Length; i++)
                    {
                        actor.Children[i].Scale(0.5f);
                    }
                    SceneManager.CurrentScene.RemoveActor(this);
                    break;
                case ItemType.GHOST:
                    if(actor is Player)
                    {
                        actor.Children[1].SetColor(255, 100, 100, 100);
                        actor.Children[2].SetColor(10, 10, 255, 100);
                    }
                    else if(actor is Ally)
                    {
                        actor.Children[0].SetColor(255, 100, 100, 100);
                        actor.Children[1].SetColor(10, 10, 255, 100);
                    }
                    else if (actor is Enemy)
                    {
                        actor.Children[0].SetColor(255, 100, 100, 100);
                        actor.Children[1].SetColor(255, 10, 10, 100);
                    }
                    SceneManager.CurrentScene.RemoveActor(this);
                    break;
                case ItemType.SPEEDUP:
                    if(actor is Ally)
                    {
                        Ally ally = (Ally)actor;
                        ally.Speed = 30;
                    }
                    if (actor is Enemy)
                    {
                        Enemy enemy = (Enemy)actor;
                        enemy.Speed = 30;
                    }
                    break;
                case ItemType.SPEEDDOWN:
                    if (actor is Ally)
                    {
                        Ally ally = (Ally)actor;
                        ally.Speed = 10;
                    }
                    if (actor is Enemy)
                    {
                        Enemy enemy = (Enemy)actor;
                        enemy.Speed = 10;
                    }
                    break;
=======
            }
        }

        public override void OnCollision(Actor actor)
        {
            if(actor.Parent != null)
                return;

            switch (_itemType)
            {
                case ItemType.SIZEUP:
                    actor.Scale(2);
                    for(int i = 0; i < actor.Children.Length; i++)
                    {
                        actor.Children[i].Scale(2);
                    }
                    SceneManager.CurrentScene.RemoveActor(this);
                    break;
>>>>>>> RayLib3D
            }
        }
    }
}
