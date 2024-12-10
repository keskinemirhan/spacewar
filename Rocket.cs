using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class Rocket : Bullet
{
    public Rocket(
            Vector2 origin,
            Vector2 position,
            float acceleration,
            float direction,
            float speed,
            int damage,
            float scale = 1.0f
            ) : base(origin, position, acceleration, direction, speed, damage, scale)
    {
    }

    public static new void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Rocket.Assets = new BulletAssets();
        Rocket.device = device;
        Rocket.Assets.Bullet = content.Load<Texture2D>("ProjectileRocket");
        Rocket.Assets.FrameCount = 3;
        Rocket.Assets.Fps = 3;
    }
}
