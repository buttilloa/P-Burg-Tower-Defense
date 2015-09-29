using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        public static float SpeedMultiplyer = 1;
        bool running = true;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            //Towers.Add(new SniperTower(200, 300));

            base.LoadContent();
        }
        KeyboardState oldState;
        protected override void Update(GameTime gameTime)
        {
            if (keyPressed(Keys.Space))
                if (eyes.Count == 0)
                {
                    Wave++;
                    SpawnCount = 0;
                    spawntimer = 1;
                }
                else
                    running = !running;
            if (keyPressed(Keys.F))
                if (SpeedMultiplyer == 1) SpeedMultiplyer = 2;
                else if (SpeedMultiplyer == 2) SpeedMultiplyer = 1;
            if (running)
            {
                spawntimer += (float)gameTime.ElapsedGameTime.TotalSeconds * Game1.SpeedMultiplyer;
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
            }
            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }
        protected bool keyPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key) && oldState.IsKeyDown(key);
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
