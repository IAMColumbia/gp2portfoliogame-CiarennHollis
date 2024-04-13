using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGameLibrary.Util;
using System.Collections;
using System.Collections.Generic;

namespace BurnoutBuster.Physics
{
    public class CollisionManager : GameComponent
    {
        // P R O P E R T I E S
        private List<ICollidable> collisionObjects;

        //DEBUG
        private enum CM_DebugState { ShowCollisionBoxes, HideCollisionBoxes }  
        private CM_DebugState debugState;
        //Key to hide and show debug things
        public Keys ToggleDebugKey;
        private InputHandler input;


        // C O N S T R U C T O R 
        public CollisionManager(Game game) : base(game)
        {
            debugState = CM_DebugState.ShowCollisionBoxes;
            collisionObjects = new List<ICollidable>();
        }

        public CollisionManager(Game game, InputHandler input) : base(game)
        {
            debugState = CM_DebugState.ShowCollisionBoxes;
            collisionObjects = new List<ICollidable>();
            this.input = input;

            
        }

        // I N I T
        public override void Initialize()
        {
            if (input == null)
            {
                input = (InputHandler)Game.Services.GetService(typeof(IInputHandler));
            }

            base.Initialize();
        }

        // U P D A T E 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckCollision();

            ToggleDebugVisuals();
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
            foreach (ICollidable target in collisionObjects)
            {
                //ICollidable target = obj;
                if (target.IsCollisionOn)
                {

                    foreach (ICollidable otherObj in CollisionQuery(target, target.Bounds))
                    {
                        Collision collision = new Collision
                        {
                            OtherObject = otherObj,
                            PenetrationVector = CalculatePenetrationVector(target.Bounds, otherObj.Bounds)
                        };
                        target.OnCollisionEnter(collision);
                    }

                    CheckHitBox(target);
                }
            }
        }
        private void CheckHitBox(ICollidable obj)
        {
            IHasHitBox target = obj as IHasHitBox;

            if (target != null)
            {
                foreach (ICollidable otherObj in CollisionQuery(target, target.HitBox))
                {
                    Collision collision = new Collision
                    {
                        OtherObject = otherObj,
                        PenetrationVector = CalculatePenetrationVector(target.HitBox, otherObj.Bounds)
                    };
                    target.OnHitBoxEnter(collision);
                }
            }
            
        }
        private List<ICollidable> CollisionQuery(ICollidable target, Rectangle collisionBoxToCheck)
        {
            List<ICollidable> queryList = new List<ICollidable>();
            foreach (ICollidable obj in collisionObjects)
            {
                if (obj != target && obj.GameObject.Enabled) // making sure we don't check collision on ourselves nor on diasble objects
                {
                    if (obj.Bounds.Intersects(collisionBoxToCheck))
                        queryList.Add(obj);
                }
            }

            return queryList;
        }

        // DEBUG VISUALS
        public void ToggleDebugVisuals()
        {
            switch (debugState)
            {
                case CM_DebugState.ShowCollisionBoxes:
                    debugState = CM_DebugState.HideCollisionBoxes;
                    break;
                case CM_DebugState.HideCollisionBoxes:
                    debugState = CM_DebugState.ShowCollisionBoxes;
                    break;
            }
        }

        public void DrawCollisionRectangles(SpriteBatch _spritebatch)
        {
            switch(debugState)
            {
                case CM_DebugState.ShowCollisionBoxes:
                    foreach(ICollidable obj in collisionObjects)
                    {
                        if (obj.IsCollisionOn)
                            DrawOneRectangle(_spritebatch, obj);
                    }
                    break;
            }
        }

        private void DrawOneRectangle(SpriteBatch _spritebatch, ICollidable obj)
        {
            _spritebatch.DrawRectangle(obj.Bounds, Color.Red, 1, 0);

            IHasHitBox HBobj = obj as IHasHitBox;
            if (HBobj != null)
                _spritebatch.DrawRectangle(HBobj.HitBox, Color.Yellow, 1, 0);
        }

    }
}
