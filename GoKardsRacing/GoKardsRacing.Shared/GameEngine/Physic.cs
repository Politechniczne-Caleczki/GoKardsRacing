using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
    

namespace GoKardsRacing.GameEngine
{
    class Physic : GameComponent
    {
        private World world;
        private long time;   

        public World World
        {
            get { return world; }
        }

        public Physic(Game game, Vector2 gravitation) : base(game)
        {
            world = new World(gravitation);            
        }
        
        public override void Update(GameTime gameTime)
        {
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }
    }
}
