using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class Rocket : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public Rocket(
            Vector2 origin,
            Vector2 position,
            float acceleration,
            float direction,
            float speed,
            int damage,
            float scale = 1.0f
            ) : base(origin, position, acceleration, direction, speed, damage, StaticAssets, scale)
    {
    }

    public static new void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Rocket.StaticAssets = new BulletAssets();
        Rocket.device = device;
        Rocket.StaticAssets.Bullet = content.Load<Texture2D>("ProjectileRocket");
        Rocket.StaticAssets.FrameCount = 3;
        Rocket.StaticAssets.Fps = 3;
    }
}
