using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace spacewar;

abstract class Weapon : GameObject
{
    public float FireRateSecs;
    public WeaponAssets Assets { get; private set; }
    public List<Bullet> Bullets;
    public bool fired = false;
    protected AnimatedTexture animatedTexture;
    protected BulletAssets bulletAssets;

    private GameTimer fireTimer = new GameTimer();

    protected Weapon(Vector2 origin, Vector2 position, float scale, float direction, float fireRateSecs, WeaponAssets assets, BulletAssets bulletAssets)
        : base(origin, position, scale, direction, 0, 0, 0, 0, 0)
    {
        this.Bullets = new List<Bullet>();
        this.bulletAssets = bulletAssets ?? throw new ArgumentNullException(nameof(bulletAssets));
        this.Assets = assets ?? throw new ArgumentNullException(nameof(assets));
        this.Direction = direction;
        this.Scale = scale;
        this.Position = position;
        this.Direction = direction;
        this.FireRateSecs = fireRateSecs;
        this.bulletAssets = bulletAssets;
        this.animatedTexture = new AnimatedTexture(origin, Assets.Weapon, Direction, scale, 0.5f, Assets.FrameCount, Assets.Fps, false);
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var bullet in Bullets)
        {
            bullet.Draw(spriteBatch, gameTime);
        }
        animatedTexture.DrawFrame(spriteBatch, Position);
    }

    public abstract void Shoot(GameTime gameTime);

    public override void Update(GameTime gameTime)
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
        foreach (var bullet in Bullets)
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
            Bullets.Remove(bullet);
        }
        animatedTexture.Rotation = Direction;
    }
}

public class WeaponAssets
{
    public Texture2D Weapon;
    public int FrameCount;
    public int Fps;
}
