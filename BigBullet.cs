using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BigBullet : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public BigBullet(Vector2 origin, Vector2 position, float scale, float direction, float speed, int damage)
         : base(origin, position, scale, direction, speed, speed, 0, 0, damage, 15, StaticAssets)
    {
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BigBullet.device = device;
        BigBullet.StaticAssets = new BulletAssets();
        BigBullet.StaticAssets.Bullet = content.Load<Texture2D>("ProjectileBig");
        BigBullet.StaticAssets.FrameCount = 10;
        BigBullet.StaticAssets.Fps = 10;
    }
}
