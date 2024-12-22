using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class StrongEnemyBullet : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public StrongEnemyBullet(Vector2 origin, Vector2 position, float scale, float direction, float speed, int damage)
         : base(origin, position, scale, direction, speed, speed, 0, 0, damage, 2, StaticAssets)
    {
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        StrongEnemyBullet.device = device;
        StrongEnemyBullet.StaticAssets = new BulletAssets();
        StrongEnemyBullet.StaticAssets.Bullet = content.Load<Texture2D>("ProjectileBig");
        StrongEnemyBullet.StaticAssets.FrameCount = 10;
        StrongEnemyBullet.StaticAssets.Fps = 10;
    }
}
