using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Factories;
using GoKardsRacing.GameEngine;

namespace GoKardsRacing
{          
    class WorldModel : DrawableGameComponent
    {
        private Body bodyCollisionBorder, bodyCollisionCenter;
        private Model model;
        private Vector3 position;
        private Physic physic;

        public WorldModel(Game game, Physic physic, Vector3 position) : base(game)
        {
            this.position = position;
            this.physic = physic;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>("Model/tor");

            Texture2D collisionCenter, collisionBorder;
            collisionBorder = Game.Content.Load<Texture2D>("Collision/tor_border");
            collisionCenter = Game.Content.Load<Texture2D>("Collision/tor_center");

            bodyCollisionBorder = BodyFactory.CreateCompoundPolygon(physic.World, Physic.getVerticesList(collisionBorder), 1);
            bodyCollisionBorder.BodyType = BodyType.Static;
            bodyCollisionCenter = BodyFactory.CreateCompoundPolygon(physic.World, Physic.getVerticesList(collisionCenter), 1);
            bodyCollisionCenter.BodyType = BodyType.Static;
            bodyCollisionCenter.Position = new Vector2(72, 74);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            Camera.DrawModel(model, position);
            base.Draw(gameTime);
        }

    }
}
