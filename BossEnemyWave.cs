using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BossEnemyWave : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public BossEnemyWave(Vector2 origin, Vector2 position, float scale, float direction, float speed, int damage)
         : base(origin, position, scale, direction, speed, speed, 0, 0, damage, 10, StaticAssets)
    {
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BossEnemyWave.device = device;
        BossEnemyWave.StaticAssets = new BulletAssets();
        BossEnemyWave.StaticAssets.Bullet = content.Load<Texture2D>("ProjectileWave");
        BossEnemyWave.StaticAssets.FrameCount = 6;
        BossEnemyWave.StaticAssets.Fps = 6;
    }
}
