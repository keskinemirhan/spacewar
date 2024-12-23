using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class StrongEnemyWeapon : Weapon
{
    public static WeaponAssets StaticAssets { get; private set; }
    public StrongEnemyWeapon(Vector2 position, float direction, float scale)
        : base(new Vector2(StaticAssets.Weapon.Width / 2, StaticAssets.Weapon.Height / 2), position, scale, direction, 10, StaticAssets, EnemyBullet.StaticAssets)
    {
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            fired = true;
            this.Bullets.Add(new StrongEnemyBullet(
                        new Vector2(StrongEnemyBullet.StaticAssets.Bullet.Width / 2, StrongEnemyBullet.StaticAssets.Bullet.Height / 2),
                        Position,
                        Scale,
                        Direction,
                        150,
                        200
                        ));
        }
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        StrongEnemyWeapon.StaticAssets = new WeaponAssets();
        StrongEnemyBullet.LoadContent(content, device);
        StrongEnemyWeapon.StaticAssets.Weapon = content.Load<Texture2D>("StrongEnemyWeapon");
        StrongEnemyWeapon.StaticAssets.FrameCount = 30;
        StrongEnemyWeapon.StaticAssets.Fps = 10;

    }
}
