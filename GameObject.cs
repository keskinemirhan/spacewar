using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

public abstract class GameObject : IContext
{
    public float CollisionRange;
    public float Speed;
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float Direction;
    public Vector2 Position;
    public float Scale;
    public Vector2 Origin;

    protected static GraphicsDeviceManager device;

    protected GameObject(Vector2 origin,
                         Vector2 position,
                         float scale,
                         float direction,
                         float speed,
                         float maxSpeed,
                         float acceleration,
                         float deceleration,
                         float collisionRange
                         )
    {
        this.Origin = origin;
        this.Scale = scale;
        this.Position = position;
        this.Direction = direction;
        this.Speed = speed;
        this.MaxSpeed = maxSpeed;
        this.Acceleration = acceleration;
        this.Deceleration = deceleration;
        this.Deceleration = deceleration;
        this.CollisionRange = collisionRange * scale;
    }

    public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    public abstract void Update(GameTime gameTime);


    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        throw new System.NotImplementedException();
    }

}
