using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BigWeapon : Weapon
{
    public static WeaponAssets StaticAssets { get; private set; }
    private bool firedBullet = false;
    public BigWeapon(Vector2 position, float direction, float scale)
        : base(new Vector2(StaticAssets.Weapon.Width / 2, StaticAssets.Weapon.Height / 2), position, scale, direction, 4, StaticAssets, EnemyBullet.StaticAssets)
    {
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            firedBullet = false;
            fired = true;
        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (fired && !firedBullet)
        {
            if (animatedTexture.CurrentFrame == 7)
            {
                firedBullet = true;
                this.Bullets.Add(new BigBullet(
                            new Vector2(BigBullet.StaticAssets.Bullet.Width / 2, BigBullet.StaticAssets.Bullet.Height / 2),
                            Position,
                            Scale,
                            Direction,
                            450f,
                            400
                            ));
            }
        }
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BigWeapon.StaticAssets = new WeaponAssets();
        BigBullet.LoadContent(content, device);
        BigWeapon.StaticAssets.Weapon = content.Load<Texture2D>("BigWeapon");
        BigWeapon.StaticAssets.FrameCount = 12;
        BigWeapon.StaticAssets.Fps = 12;

    }
}
