using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace PburgTowerDefense
{
    public class Tower
    {
        protected float rotation;
        protected float X, Y;
        float shotTimer = 0;
        Vector2 Target = new Vector2();
        protected Vector2 Center = new Vector2();
        protected int Range = 0;
        protected float bulletSpeed = 250;
        bool hasTarget = false;
        protected float TimePerShot;
        public bool showRange = false;
        public Tower()
        {

        }
        public virtual void update(GameTime gametime)
        {
            shotTimer += (float)gametime.ElapsedGameTime.TotalSeconds;
            getEnemy();
            Vector2 calcRotate = Target - new Vector2(X, Y);
            calcRotate.Normalize();
            rotation = (float)Math.Atan2(calcRotate.Y, calcRotate.X);

            if (shotTimer >= TimePerShot && hasTarget)
            {
                shotTimer = 0;
                Vector2 vel = Target - new Vector2(X, Y);
                vel.Normalize();
                vel *= bulletSpeed;
                Game1.shots.Add(new Bullet(X, Y, vel, 50));
            }
        }
        public void ShowRange(bool Show)
        {
            showRange = Show;
        }
        public void getEnemy()
        {
            if (Game1.eyes.Count > 0)
            {
                int shortest = -1;
                float shortestDistance = 10000;
                for (int i = 0; i < Game1.eyes.Count; i++)
                {
                    float distance = Math.Abs(Vector2.Distance(Center, Game1.eyes[i].Center()));
                    if (distance <= Range)

                        if (distance < shortestDistance)
                        {
                            shortest = i;
                            shortestDistance = distance;
                        }
                }
                if (shortest != -1)
                {
                    hasTarget = true;
                    Target = GuessFactor(Game1.eyes[shortest]);//TestTarget.Center();
                }
                else hasTarget = false;
            }
            else Target = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        }
        private Vector2 GuessFactor(Enemy TestTarget)
        {
            float time = Math.Abs(Vector2.Distance(Center, TestTarget.Center())) / bulletSpeed;
            Vector2 predictedPos = (TestTarget.velocity) * (time * TestTarget.speed);
            Vector2 guess = TestTarget.Center() + predictedPos;
            return guess;
        }
        public virtual void draw(SpriteBatch batch)
        {

        }
    }
}
