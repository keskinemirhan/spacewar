using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

class CannonWeapon : Weapon
{
    public static WeaponAssets StaticAssets { get; private set; }
    private GameTimer attackTimer = new GameTimer();
    private bool fired1 = false;
    private bool fired2 = false;
    private int bulletDamage = 50;

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        CannonBullet.LoadContent(content, device);
        CannonWeapon.device = device;
        CannonWeapon.StaticAssets = new WeaponAssets();
        CannonWeapon.StaticAssets.Weapon = content.Load<Texture2D>("CannonWeapon");
        CannonWeapon.StaticAssets.FrameCount = 7;
        CannonWeapon.StaticAssets.Fps = 14;
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            attackTimer.Clear();
            fired = true;
            fired1 = false;
            fired2 = false;
        }
    }

    public override void Update(GameTime gameTime)
    {

        base.Update(gameTime);

        if (fired)
        {
            attackTimer.StartTimer(gameTime);
            if (animatedTexture.CurrentFrame == 3 && !fired2)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * +30),
                        (float)(+30 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired2 = true;
                this.Bullets.Add(new CannonBullet(
                            new Vector2(CannonBullet.StaticAssets.Bullet.Width / 2, CannonBullet.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            Direction,
                            600f,
                            bulletDamage));
            }
            else if (animatedTexture.CurrentFrame == 2 && !fired1)
            {
                var pos = new Vector2((float)(Math.Cos(Direction) * -30),
                        (float)(-30 * Math.Sin(Direction)));
                pos = new Vector2(Position.X + pos.X, Position.Y + pos.Y);
                fired1 = true;
                this.Bullets.Add(new CannonBullet(
                            new Vector2(CannonBullet.StaticAssets.Bullet.Width / 2, CannonBullet.StaticAssets.Bullet.Height / 2),
                            pos,
                            Scale,
                            Direction,
                            600f,
                            bulletDamage));
            }
        }
    }

    public CannonWeapon(Vector2 position, float direction, float scale)
         : base(new Vector2(CannonWeapon.StaticAssets.Weapon.Width / 2, RocketWeapon.StaticAssets.Weapon.Height / 2),
                position, scale, direction, 0.5f, CannonWeapon.StaticAssets, Rocket.StaticAssets)
    {

    }
}
