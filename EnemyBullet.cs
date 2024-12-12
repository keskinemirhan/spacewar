using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class EnemyBullet : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public EnemyBullet(Vector2 origin, Vector2 position, float acceleration, float direction, float speed, int damage, float scale = 1)
         : base(origin, position, acceleration, direction, speed, damage, StaticAssets, scale)
    {
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        EnemyBullet.device = device;
        EnemyBullet.StaticAssets = new BulletAssets();
        EnemyBullet.StaticAssets.Bullet = content.Load<Texture2D>("EnemyBullet");
        EnemyBullet.StaticAssets.FrameCount = 4;
        EnemyBullet.StaticAssets.Fps = 4;
    }
}
