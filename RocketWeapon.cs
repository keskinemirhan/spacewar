using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

class RocketWeapon : Weapon
{
    public static WeaponAssets StaticAssets { get; private set; }
    private GameTimer attackTimer = new GameTimer();
    private bool fired1 = false;
    private bool fired2 = false;
    private bool fired3 = false;
    private bool fired4 = false;
    private bool fired5 = false;
    private bool fired6 = false;

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Rocket.LoadContent(content, device);
        RocketWeapon.device = device;
        RocketWeapon.StaticAssets = new WeaponAssets();
        RocketWeapon.StaticAssets.Weapon = content.Load<Texture2D>("RocketWeapon");
        RocketWeapon.StaticAssets.FrameCount = 17;
        RocketWeapon.StaticAssets.Fps = 9;
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            attackTimer.Clear();
            fired = true;
            fired1 = false;
            fired2 = false;
            fired3 = false;
            fired4 = false;
            fired5 = false;
            fired6 = false;
        }
    }

    public override void Update(GameTime gameTime)
    {

        base.Update(gameTime);

        if (fired)
        {
            attackTimer.StartTimer(gameTime);
            if (attackTimer.Elapsed * (float)RocketWeapon.StaticAssets.Fps >= 13 && !fired6)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * +30),
                        (float)(+30 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired6 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.StaticAssets.Bullet.Width / 2, Rocket.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.StaticAssets.Fps >= 11 && !fired5)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * -30),
                        (float)(-30 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired5 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.StaticAssets.Bullet.Width / 2, Rocket.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.StaticAssets.Fps >= 9 && !fired4)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * +25),
                        (float)(+25 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired4 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.StaticAssets.Bullet.Width / 2, Rocket.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.StaticAssets.Fps >= 7 && !fired3)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * -25),
                        (float)(-25 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired3 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.StaticAssets.Bullet.Width / 2, Rocket.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.StaticAssets.Fps >= 5 && !fired2)
            {

                var pos = new Vector2((float)(Math.Cos(Direction) * +20),
                        (float)(+20 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired2 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.StaticAssets.Bullet.Width / 2, Rocket.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.StaticAssets.Fps >= 3 && !fired1)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * -20),
                        (float)(-20 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired1 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.StaticAssets.Bullet.Width / 2, Rocket.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            100f,
                            Direction,
                            150f,
                            100));
            }
        }
    }

    public RocketWeapon(Vector2 position, float direction, float scale = 1)
         : base(new Vector2(RocketWeapon.StaticAssets.Weapon.Width / 2, RocketWeapon.StaticAssets.Weapon.Height / 2),
                position, scale, direction, 3, RocketWeapon.StaticAssets, Rocket.StaticAssets)
    {

    }
}
