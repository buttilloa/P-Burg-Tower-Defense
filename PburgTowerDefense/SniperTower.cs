using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PburgTowerDefense
{
    public class SniperTower : Tower
    {
        public SniperTower(float x, float y)
        {
            X = x;
            Y = y; 
            Center = new Vector2(X + 19, Y + 16);
            TimePerShot = .5f;
            Range = 100;
           }
		public override void update(GameTime time){

			
            base.update(time);
		}
        public override void draw(SpriteBatch batch)
        {
            if(showRange)batch.Draw(Game1.RangeCircle, new Rectangle((int)X - Range, (int)Y - Range, Range * 2, Range * 2), Color.White);
            batch.Draw(Game1.tower, new Vector2(X, Y), new Rectangle(0, 0, 39, 32), Color.White, rotation, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
        }

    }
}
