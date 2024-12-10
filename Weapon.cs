using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace spacewar;

abstract class Weapon : IContext
{
    public Vector2 Position;
    public float Direction;
    public float Scale;
    public float BulletScale;
    public float FireRateSecs;

    protected List<Bullet> bullets;
    protected static GraphicsDeviceManager device;
    protected static WeaponAssets assets;
    protected bool fired = false;
    protected AnimatedTexture animatedTexture;
    protected BulletAssets bulletAssets;

    private GameTimer fireTimer = new GameTimer();

    protected Weapon(Vector2 origin, Vector2 position, float direction, float scale, float bulletScale, float fireRateSecs, BulletAssets bulletAssets)
    {
        this.bullets = new List<Bullet>();
        this.bulletAssets = bulletAssets;
        this.Direction = direction;
        this.Scale = scale;
        this.Position = position;
        this.Direction = direction;
        this.BulletScale = bulletScale;
        this.FireRateSecs = fireRateSecs;
        this.bulletAssets = bulletAssets;
        this.animatedTexture = new AnimatedTexture(origin, assets.Weapon, Direction, scale, 0.5f, assets.FrameCount, assets.Fps);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var bullet in bullets)
        {
            bullet.Draw(spriteBatch, gameTime);
        }
        animatedTexture.DrawFrame(spriteBatch, Position);
    }

    public abstract void Shoot(GameTime gameTime);

    public virtual void Update(GameTime gameTime)
    {
        animatedTexture.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
        if (fired)
        {
            animatedTexture.Play();
            fireTimer.StartTimer(gameTime);
            if (fireTimer.Passed(FireRateSecs))
            {
                animatedTexture.Reset();
                fired = false;
                fireTimer.Clear();
            }
        }
        else animatedTexture.Stop();
        var toBeRemoved = new List<Bullet>();
        foreach (var bullet in bullets)
        {
            if (bullet.Position.X < -bulletAssets.Bullet.Width / bulletAssets.FrameCount
                    || bullet.Position.X > device.PreferredBackBufferWidth + bulletAssets.Bullet.Width / bulletAssets.FrameCount
                    || bullet.Position.Y < -bulletAssets.Bullet.Height
                    || bullet.Position.Y > device.PreferredBackBufferHeight + bulletAssets.Bullet.Height)
            {
                toBeRemoved.Add(bullet);

            }
            bullet.Update(gameTime);
        }

        foreach (var bullet in toBeRemoved)
        {
            bullets.Remove(bullet);
        }
        animatedTexture.Rotation = Direction;
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        throw new System.NotImplementedException();
    }
}

public class WeaponAssets
{
    public Texture2D Weapon;
    public int FrameCount;
    public int Fps;
}
