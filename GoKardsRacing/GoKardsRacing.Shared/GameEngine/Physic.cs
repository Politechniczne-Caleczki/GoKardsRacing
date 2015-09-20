using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common.TextureTools;
using FarseerPhysics.Common.Decomposition;

namespace GoKardsRacing.GameEngine
{
    class Physic : GameComponent
    {
        private World world;
        
        public World World
        {
            get { return world; }
        }

        public Physic(Game game, Vector2 gravitation) : base(game)
        {
            world = new World(gravitation);            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            world.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        public static List<Vertices> getVerticesList(Texture2D texture)
        {
            uint[] textData = new uint[texture.Width * texture.Height];
            texture.GetData<uint>(textData);

            Vertices verts = TextureConverter.DetectVertices(textData, texture.Width);
            return BayazitDecomposer.ConvexPartition(verts);
        }
    }
}
