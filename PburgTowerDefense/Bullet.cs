using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PburgTowerDefense
{
    public class Bullet
    {
        Vector2 Location, Velocity;
        public Boolean Destroy = false;
        private float damage;
        private Enemy Target;
        private float bulletSpeed;
        public Bullet(float x, float y, float BulletSpeed, float damage,Enemy target)
        {
            Location = new Vector2(x, y);
            this.damage = damage;
            Target = target;
            bulletSpeed = BulletSpeed;
        }
        public float Damage
        {
            get { return damage; }
        }
        public void update(GameTime time)
        {
            Velocity = Target.Center() - Location;
            Velocity.Normalize();
            Velocity *= bulletSpeed;
            if (Math.Abs(Vector2.Distance(Location, Target.Center())) <= 15)
            {
                Target.health -= damage;
                damage = 0;
                Destroy = true;
            }
            if (Location.X > 800 || Location.X < 0 || Location.Y > 500 || Location.Y < 0)
                Destroy = true;
            Location += Velocity * (float)time.ElapsedGameTime.TotalSeconds * Game1.SpeedMultiplyer;
            // EffectManager.Effect("Enemy Cannon Fire").Trigger(new Vector2(Location.X, Location.Y));

        }
        public void draw(SpriteBatch batch)
        {
            float rotation = (float)Math.Atan2(Velocity.Y, Velocity.X) - 15;
            batch.Draw(Game1.Football, Location, new Rectangle(0, 0, 482, 482), Color.White, rotation, new Vector2(241, 241), 0.04f, SpriteEffects.None, 1);
        }
        public Boolean hit(Vector2 coords)
        {
            if (Vector2.Distance(coords, Location) <= 16)
                return true;
            return false;
        }
    }
}
