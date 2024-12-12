using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BasicEnemyWeapon : Weapon
{
    public static WeaponAssets StaticAssets { get; private set; }
    public BasicEnemyWeapon(Vector2 position, float direction, float scale)
        : base(new Vector2(StaticAssets.Weapon.Width / 2, StaticAssets.Weapon.Height / 2), position, scale, direction, 4, StaticAssets, EnemyBullet.StaticAssets)
    {
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            fired = true;
            this.bullets.Add(new EnemyBullet(
                        new Vector2(EnemyBullet.StaticAssets.Bullet.Width / 2, EnemyBullet.StaticAssets.Bullet.Height / 2),
                        Position,
                        Scale,
                        Direction,
                        100f,
                        20
                        ));
        }
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BasicEnemyWeapon.StaticAssets = new WeaponAssets();
        EnemyBullet.LoadContent(content, device);
        BasicEnemyWeapon.StaticAssets.Weapon = content.Load<Texture2D>("BasicEnemyWeapon");
        BasicEnemyWeapon.StaticAssets.FrameCount = 6;
        BasicEnemyWeapon.StaticAssets.Fps = 6;

    }
}
