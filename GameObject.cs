using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

abstract class GameObject : IContext
{
    public Rectangle CollisionRect;
    public float Speed;
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float Direction;
    public Vector2 Position;

    protected static GraphicsDeviceManager device;
    protected Vector2 origin;
    protected float scale;

    protected GameObject(Vector2 origin,
                         Vector2 position,
                         float scale,
                         float direction,
                         float speed,
                         float maxSpeed,
                         float acceleration,
                         float deceleration,
                         Rectangle collisionRect
                         )
    {
        this.origin = origin;
        this.scale = scale;
        this.Position = position;
        this.Direction = direction;
        this.Speed = speed;
        this.MaxSpeed = maxSpeed;
        this.Acceleration = acceleration;
        this.Deceleration = deceleration;
        this.Deceleration = deceleration;
        this.CollisionRect = collisionRect;
    }

    public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    public abstract void Update(GameTime gameTime);

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        throw new System.NotImplementedException();
    }

}
