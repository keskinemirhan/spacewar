using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

abstract class Bullet : IContext
{
    public BulletAssets Assets { get; private set; }
    public float Speed;
    public float Acceleration;
    public int Damage;
    public float Direction;
    public Vector2 Position;

    protected static GraphicsDeviceManager device;
    protected AnimatedTexture animatedTexture;

    protected Bullet(
            Vector2 origin,
            Vector2 position,
            float acceleration,
            float direction,
            float speed,
            int damage,
            BulletAssets assets,
            float scale = 1.0f
            )
    {
        this.Acceleration = acceleration;
        this.Speed = speed;
        this.Damage = damage;
        this.Direction = direction;
        this.Position = position;
        this.Assets = assets ?? throw new ArgumentNullException(nameof(assets));
        this.animatedTexture = new AnimatedTexture(origin,
                Assets.Bullet, direction, scale, Assets.Depth, Assets.FrameCount, Assets.Fps);

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        animatedTexture.DrawFrame(spriteBatch, Position);
    }

    public void Update(GameTime gameTime)
    {
        var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Speed += Acceleration * elapsed;
        var speedIncrease = Speed * elapsed;
        Position.Y -= (float)Math.Sin(Direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        Position.X -= (float)Math.Cos(Direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        this.animatedTexture.UpdateFrame(elapsed);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        throw new NotImplementedException();
    }
}

public class BulletAssets
{
    public Texture2D Bullet;
    public int FrameCount;
    public int Fps;
    public float Depth;
}
