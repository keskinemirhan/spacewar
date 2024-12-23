using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class CannonBullet : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public CannonBullet(Vector2 origin, Vector2 position, float scale, float direction, float speed, int damage)
         : base(origin, position, scale, direction, speed, speed, 0, 0, damage, 2, StaticAssets)
    {
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        CannonBullet.device = device;
        CannonBullet.StaticAssets = new BulletAssets();
        CannonBullet.StaticAssets.Bullet = content.Load<Texture2D>("ProjectileCanon");
        CannonBullet.StaticAssets.FrameCount =4 ;
        CannonBullet.StaticAssets.Fps = 4;
    }
}
