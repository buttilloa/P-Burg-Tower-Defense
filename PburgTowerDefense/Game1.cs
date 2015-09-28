using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PburgTowerDefense
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D map;
        public static Texture2D tower, EyeSheet, weather, RangeCircle, Football, PBurg;
        public static List<Bullet> shots = new List<Bullet>();
        List<Tower> Towers = new List<Tower>();
        public static List<Tile> turns = new List<Tile>();
        public static List<Enemy> eyes = new List<Enemy>();
        int SpawnCount = 0;
        public static int Wave = 0;
        float spawntimer;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.IsFixedTimeStep = false;
            //this.graphics.SynchronizeWithVerticalRetrace = false;
            this.IsMouseVisible = true;
        }
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = Content.Load<Texture2D>("map");
            tower = Content.Load<Texture2D>("tower");
            weather = Content.Load<Texture2D>("Weather");
            EyeSheet = Content.Load<Texture2D>("eye");
            RangeCircle = Content.Load<Texture2D>("Range");
            Football = Content.Load<Texture2D>("football");
            PBurg = Content.Load<Texture2D>("pburg");

            turns.Add(new Tile(new Rectangle(585, 91, 1, 1), "Left"));
            turns.Add(new Tile(new Rectangle(259, 91, 1, 1), "Down"));
            turns.Add(new Tile(new Rectangle(259, 391, 1, 1), "Right"));
            turns.Add(new Tile(new Rectangle(585, 391, 1, 1), "Up"));
            turns.Add(new Tile(new Rectangle(585, 167, 1, 1), "Left"));
            turns.Add(new Tile(new Rectangle(341, 167, 1, 1), "Down"));
            turns.Add(new Tile(new Rectangle(341, 317, 1, 1), "Right"));
            turns.Add(new Tile(new Rectangle(503, 317, 1, 1), "Up"));
            turns.Add(new Tile(new Rectangle(503, 241, 1, 1), "Right"));
            turns.Add(new Tile(new Rectangle(760, 222, 41, 39), "End"));
            Towers.Add(new SniperTower(200, 91));

            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            spawntimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (eyes.Count == 0)
                if (spawntimer >= 5)
                {
                    Wave++;
                    SpawnCount = 0;
                }
            if (spawntimer >= 1 && SpawnCount < Wave * 5)
            {
                eyes.Add(new Enemy());
                SpawnCount++;
                spawntimer = 0;
            }
            for (int i = shots.Count - 1; i >= 0; i--)
            {
                shots[i].update(gameTime);
                if (shots[i].Destroy) shots.RemoveAt(i);
            }
            for (int i = Towers.Count - 1; i >= 0; i--)
            {
                Towers[i].update(gameTime);
            }
            for (int i = eyes.Count - 1; i >= 0; i--)
            {
                eyes[i].update(gameTime);
                if (eyes[i].Finishes) eyes.RemoveAt(i);
            }

            base.Update(gameTime);

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(map, new Rectangle(0, 0, 800, 480), Color.White);
            foreach (Enemy eye in eyes)
                eye.draw(spriteBatch);
            foreach (Bullet bullet in shots)
                bullet.draw(spriteBatch);
            foreach (Tower tower in Towers)
                tower.draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
