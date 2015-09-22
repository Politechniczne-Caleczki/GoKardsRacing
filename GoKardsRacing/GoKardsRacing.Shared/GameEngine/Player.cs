using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using GoKardsRacing.GameEngine;
using FarseerPhysics.Factories;

 namespace GoKardsRacing.GameEngine
{
    public class Player: DrawableGameComponent
    {
        private Body body;
        private Model model;
        private Physic physic;
        private Vector3 position;
        private float speed;
        private float scale;


        public Vector3 Position
        {
            get
            {
                position = new Vector3(body.Position.X / 10, position.Y, body.Position.Y / 10);
                return position;
            }
        }

        public float Scale
        {
            get { return scale; }
            set
            {
                if (scale > 0)
                    scale = value;
            }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Player(Game game, Vector3 position,float speed, float scale, Physic physic ):base(game)
        {
            this.physic = physic;
            this.position = position;
            this.speed = speed;
            this.scale = scale;
        }

        public override void Initialize()
        {
            body = BodyFactory.CreateCircle(physic.World, 5, 1);
            body.BodyType = BodyType.Dynamic;
            body.LinearDamping = 0.4f;
            body.Friction = 0;
            body.Restitution = 0.8f;
            body.Position = new Vector2(position.X, position.Z);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            model = Game.Content.Load<Model>("Model/cokart");
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            body.ApplyLinearImpulse(new Vector2(-speed * (float)Math.Sin(body.Rotation), -speed * (float)Math.Cos(body.Rotation)));
            Camera.Position = new Vector3(Position.X, 2.7f, Position.Z);
            body.Rotation = Camera.Rotation.Y;
            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            Camera.DrawModel(model, Position, new Vector3(0,body.Rotation,0), scale);
            base.Draw(gameTime);
        }
    }
}
