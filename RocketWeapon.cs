using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class RocketWeapon : Weapon
{
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
        RocketWeapon.assets = new WeaponAssets();
        RocketWeapon.assets.Weapon = content.Load<Texture2D>("RocketWeapon");
        RocketWeapon.assets.FrameCount = 17;
        RocketWeapon.assets.Fps = 9;
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
            if (attackTimer.Elapsed * (float)RocketWeapon.assets.Fps >= 13 && !fired6)
            {
                fired6 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.Assets.Bullet.Width / 2 - 30, Rocket.Assets.Bullet.Height / 2),
                            Position,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.assets.Fps >= 11 && !fired5)
            {
                fired5 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.Assets.Bullet.Width / 2 + 30, Rocket.Assets.Bullet.Height / 2),
                            Position,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.assets.Fps >= 9 && !fired4)
            {
                fired4 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.Assets.Bullet.Width / 2 - 25, Rocket.Assets.Bullet.Height / 2),
                            Position,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.assets.Fps >= 7 && !fired3)
            {
                fired3 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.Assets.Bullet.Width / 2 + 25, Rocket.Assets.Bullet.Height / 2),
                            Position,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.assets.Fps >= 5 && !fired2)
            {
                fired2 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.Assets.Bullet.Width / 2 - 20, Rocket.Assets.Bullet.Height / 2),
                            Position,
                            100f,
                            Direction,
                            150f,
                            100));
            }
            else if (attackTimer.Elapsed * (float)RocketWeapon.assets.Fps >= 3 && !fired1)
            {
                fired1 = true;
                this.bullets.Add(new Rocket(
                            new Vector2(Rocket.Assets.Bullet.Width / 2 + 20, Rocket.Assets.Bullet.Height / 2),
                            Position,
                            100f,
                            Direction,
                            150f,
                            100));
            }
        }
    }

    public RocketWeapon(Vector2 position, float direction, float scale = 1, float bulletScale = 1)
        : base(new Vector2(RocketWeapon.assets.Weapon.Width / 2, RocketWeapon.assets.Weapon.Height / 2),
                position, direction, scale, bulletScale, 2, Rocket.Assets)
    {

    }
}
