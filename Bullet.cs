using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

abstract class Bullet : GameObject
{
    public BulletAssets Assets { get; private set; }
    public int Damage;
    protected AnimatedTexture animatedTexture;

    protected Bullet(Vector2 origin, Vector2 position, float scale, float direction, float speed, float maxSpeed, float acceleration, float deceleration, int damage, float collisionRange, BulletAssets assets)
        : base(origin, position, scale, direction, speed, maxSpeed, acceleration, deceleration, collisionRange)
    {
        this.Assets = assets ?? throw new ArgumentNullException(nameof(assets));
        this.animatedTexture = new AnimatedTexture(origin,
                Assets.Bullet, direction, scale, Assets.Depth, Assets.FrameCount, Assets.Fps);
        this.Damage = damage;
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        animatedTexture.DrawFrame(spriteBatch, Position);
    }

    public override void Update(GameTime gameTime)
    {
        var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Speed += Acceleration * elapsed;
        var speedIncrease = Speed * elapsed;
        Position.Y -= (float)Math.Sin(Direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        Position.X -= (float)Math.Cos(Direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        this.animatedTexture.UpdateFrame(elapsed);
    }
}

public class BulletAssets
{
    public Texture2D Bullet;
    public int FrameCount;
    public int Fps;
    public float Depth;
}
