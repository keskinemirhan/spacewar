using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace spacewar;
class BossEnemyWeapon : Weapon
{
    public static WeaponAssets StaticAssets { get; private set; }
    public Vector2 PlayerPosition { get; set; }
    private List<HomingBullet> homingBullets = new List<HomingBullet>();
    public BossEnemyWeapon(Vector2 position, float direction, float scale, Vector2 playerPosition)
        : base(new Vector2(StaticAssets.Weapon.Width / 2, StaticAssets.Weapon.Height / 2), position, scale, direction, 8, StaticAssets, EnemyBullet.StaticAssets)
    {
        PlayerPosition = playerPosition;
    }

    public override void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            fired = true;
            this.Bullets.Add(new BossEnemyWave(
                        new Vector2(BossEnemyWave.StaticAssets.Bullet.Width / 2, BossEnemyWave.StaticAssets.Bullet.Height / 2),
                        Position,
                        Scale,
                        Direction,
                        400f,
                        50
                        ));
            var homingBullet = new HomingBullet(
                        new Vector2(HomingBullet.StaticAssets.Bullet.Width / 2, HomingBullet.StaticAssets.Bullet.Height / 2),
                        Position,
                        Scale,
                        Direction,
                        200f,
                        600,
                        PlayerPosition
                        );
            Bullets.Add(homingBullet);
            homingBullets.Add(homingBullet);

        }
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach (var homingBullet in homingBullets)
        {
            homingBullet.PlayerPosition = PlayerPosition;
        }

    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BossEnemyWeapon.StaticAssets = new WeaponAssets();
        BossEnemyWave.LoadContent(content, device);
        HomingBullet.LoadContent(content, device);
        BossEnemyWeapon.StaticAssets.Weapon = content.Load<Texture2D>("BossEnemyWeapon");
        BossEnemyWeapon.StaticAssets.FrameCount = 60;
        BossEnemyWeapon.StaticAssets.Fps = 60;

    }
}
