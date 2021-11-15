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
        SHEILD
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
            }

            base.Update(deltaTime);
        }

        public void CreateSprite()
        {
            switch(_itemType)
            {
                case ItemType.SIZEUP:
                    Actor sprite = new Actor(1, 0, 0, "Sprite", Shape.CUBE);
                    sprite.SetScale(0.5f, 0.5f, 0.5f);
                    sprite.SetColor(20, 0, 0, 255);
                    sprite.Collider = new AABBCollider(sprite);
                    AddChild(sprite);
                    break;
            }
        }
    }
}
