using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BurnoutBuster.Collision
{
    public class CollisionManager : GameComponent
    {
        // P R O P E R T I E S
        private List<ICollidable> collisionObjects;

        // C O N S T R U C T O R 
        public CollisionManager(Game game) : base(game)
        {
            collisionObjects = new List<ICollidable>();
        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }

        // U P D A T E 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckCollision();
        }


        // M I S C   M E T H O D S

        //MANAGING LIST OF COLLIDABLE OBJECTS
        public void AddObject(ICollidable obj) //adds object to list of collision objs
        {
            if (!collisionObjects.Contains(obj))
            {
                collisionObjects.Add(obj);
            }
        }
        public void RemoveObject(ICollidable obj)
        {
            if (collisionObjects.Contains(obj))
            {
                collisionObjects.Remove(obj);
            }
        }
        public bool Contains(ICollidable obj)
        {
            return collisionObjects.Contains(obj);
        }


        // VECTOR CALCULATION
        private static Vector2 CalculatePenetrationVector(Rectangle a, Rectangle b) // this will only work for rectangle collision
        {
            return PenetrationVector(a, b);
        }
        //Pulled from MonoGame.Extended.Collision.CollisionComponent.PenetrationVector(...);
        private static Vector2 PenetrationVector(Rectangle rect1,  Rectangle rect2)
        {
            Rectangle rectangle = Rectangle.Intersect(rect1, rect2);
            Vector2 result;
            if (rectangle.Width < rectangle.Height)
            {
                float x = ((rect1.Center.X < rect2.Center.X) ? rectangle.Width : (0f - rectangle.Width));
                result = new Vector2(x, 0f);
            }
            else
            {
                float y = ((rect1.Center.Y < rect2.Center.Y) ? rectangle.Height : (0f - rectangle.Height));
                result = new Vector2(0f, y);
            }

            return result;

        }

        // COLLISION CHECKS
        // adapted from MonoGame.Extended.Collision.CollisionComponent.Update(...);
        private void CheckCollision()
        {
            foreach (ICollidable obj in collisionObjects)
            {
                ICollidable target = obj;

                foreach (ICollidable otherObj in CollisionQuery(target))
                {
                    Collision collision = new Collision
                    {
                        OtherObject = otherObj,
                        PenetrationVector = CalculatePenetrationVector(target.Bounds, otherObj.Bounds)
                    };
                    target.OnCollisionEnter(collision);
                }
            }
        }
        private List<ICollidable> CollisionQuery(ICollidable target)
        {
            List<ICollidable> queryList = new List<ICollidable>();
            foreach (ICollidable obj in collisionObjects)
            {
                if (obj != target && obj.GameObject.Enabled) // making sure we don't check collision on ourselves nor on diasble objects
                {
                    if (obj.Bounds.Intersects(target.Bounds))
                        queryList.Add(obj);
                }
            }

            return queryList;
        }
    }
}
