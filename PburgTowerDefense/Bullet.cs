using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PburgTowerDefense
{
    public class Bullet
    {
        Vector2 Location, Velocity;
        public Boolean Destroy = false;
        private float damage;

        public Bullet(float x, float y, Vector2 velocity, float damage)
        {
            Location = new Vector2(x, y);
            Velocity = velocity;
            this.damage = damage;
        }
        public float Damage
        {
            get { return damage; }
        }
        public void update(GameTime time)
        {
            if (Location.X > 800 || Location.X < 0 || Location.Y > 500 || Location.Y < 0)
                Destroy = true;
            Location += Velocity * (float)time.ElapsedGameTime.TotalSeconds;
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
