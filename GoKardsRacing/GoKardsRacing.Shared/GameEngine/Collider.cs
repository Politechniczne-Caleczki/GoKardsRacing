using System;
using System.Collections.Generic;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace GoKardsRacing.GameEngine
{
    class Collider
    {
        private Body body;
        
        public Collider(Body body)
        {
            this.Body = body;
        }

        public Body Body
        {
            get { return body; }
            protected set { body = value; }
        }

        public Vector3 Position
        {
            get
            {
                return new Vector3(body.Position.X, 0, body.Position.Y);
            }
            set
            {
                body.Position = new Vector2(value.X, value.Z);
            }
        }

        public Vector3 Rotation
        {
            get { return new Vector3(0, body.Rotation, 0); }
            set { body.Rotation = value.Y; }
        }
              
    }
}
