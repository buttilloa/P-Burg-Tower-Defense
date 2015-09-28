using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PburgTowerDefense
{
    public class Enemy
    {
        private int currentFrame;
        private float frameTime = 0.1f;
        private float timeForCurrentFrame = 0.0f;
        public float health = 100, maxHealth;
        public int x, y;
        public Vector2 velocity;
        Rectangle[] frames = new Rectangle[2];
        public bool Finishes = false;
        int checkpoint = 0;
        public float speed = 100f;
        public Enemy()
        {
            speed += (.1f * new Random().Next(-5, 5));
            x = 570;
            y = 10;
            frames[0] = new Rectangle(0, 0, 118, 118);
            frames[1] = new Rectangle(118, 0, 118, 118);
            //frames[2] = new Rectangle(236, 0, 118, 118);
            //frames[3] = new Rectangle(354, 0, 118, 118);
            velocity = new Vector2(0, 1);
            health = 300 + (Game1.Wave * 50);
            maxHealth = health;

        }
        public void update(GameTime time)
        {

            float elapsed = (float)time.ElapsedGameTime.TotalSeconds;
            timeForCurrentFrame += elapsed;
            x += (int)(velocity.X * elapsed * speed);
            y += (int)(velocity.Y * elapsed * speed);
            if (timeForCurrentFrame >= frameTime)
            {
                currentFrame = (currentFrame + 1) % (frames.Length);
                timeForCurrentFrame = 0.0f;
            }
            if (new Rectangle(x + 15, y + 15, 1, 1).Intersects(Game1.turns[checkpoint].boudningBox))
            {
                switch (Game1.turns[checkpoint].Direction)
                {
                    case "Left": velocity = new Vector2(-1, 0); break;
                    case "Right": velocity = new Vector2(1, 0); break;
                    case "Up": velocity = new Vector2(0, -1); break;
                    case "Down": velocity = new Vector2(0, 1); break;
                    default: break;
                }
                checkpoint++;
                if (checkpoint == Game1.turns.Count)
                    Finishes = true;
            }
            foreach (Bullet shot in Game1.shots)
                if (shot.hit(new Vector2(x + 15, y + 15)))
                    health -= shot.Damage;
            if (health <= 0) Finishes = true;
        }
        public int handleDamage()
        {
            int drawdamage = 0;
            for (float i = 0; i < 10; i += .35f)
            {
                if (health >= (.1f * i) * maxHealth) drawdamage++;
            }
            return drawdamage;
        }
        public Vector2 Center()
        {
            return new Vector2(this.x + 15, this.y + 15);
        }
        public void draw(SpriteBatch batch)
        {
            batch.Draw(Game1.PBurg, new Rectangle(x, y, 30, 30), new Rectangle(0, 0, 120, 122), Color.White);
            batch.Draw(Game1.weather, new Rectangle(x, y + 30, handleDamage(), 3), health > maxHealth / 2 ? Color.Green : health > maxHealth / 4 ? Color.Orange : Color.Red);

        }
    }
}
