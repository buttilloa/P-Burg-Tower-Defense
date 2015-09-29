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
        protected float shotTimer = 0;
        Enemy Target;
        protected Vector2 Center = new Vector2();
        protected int Range = 0;
        protected float bulletSpeed = 250;
        bool hasTarget = false;
        protected float TimePerShot;
        public bool showRange = false;
        public Tower()
        {

        }
        Vector2 calcRotate = new Vector2(1, 1);
        protected float Damage = 50;
        public virtual void update(GameTime gametime)
        {
            shotTimer += (float)gametime.ElapsedGameTime.TotalSeconds * Game1.SpeedMultiplyer;
            getEnemy();

            if (Target != null) calcRotate = Target.Center() - new Vector2(X, Y);
            calcRotate.Normalize();
            rotation = (float)Math.Atan2(calcRotate.Y, calcRotate.X);

            if (shotTimer >= TimePerShot && hasTarget)
            {
                shotTimer = 0;
                FireShot();
            }
        }
        public void ShowRange(bool Show)
        {
            showRange = Show;
        }
        public void FireShot()
        {
            Game1.shots.Add(new Bullet(X, Y, bulletSpeed, Damage, Target));
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
                    Target = Game1.eyes[shortest];
                }
                else hasTarget = false;
            }
            else hasTarget = false;
        }
        public int handleDamage()
        {
            int drawdamage = 0;
            for (float i = 0; i < 10; i += .35f)
            {
                if (shotTimer >= (.1f * i) * TimePerShot) drawdamage++;
            }
            return drawdamage;
        }
        public virtual void draw(SpriteBatch batch)
        {
            
        }
    }
}
