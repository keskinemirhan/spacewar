using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace spacewar;
class HomingBullet : Bullet
{
    public static BulletAssets StaticAssets { get; private set; }
    public Vector2 PlayerPosition { get; set; }
    private GameTimer homingTimer;
    public HomingBullet(Vector2 origin, Vector2 position, float scale, float direction, float speed, int damage, Vector2 playerPosition)
         : base(origin, position, scale, direction, speed, speed, 0, 0, damage, 2, StaticAssets)
    {
        PlayerPosition = playerPosition;
        homingTimer = new GameTimer();
    }

    public override void Update(GameTime gameTime)
    {
        homingTimer.StartTimer(gameTime);
        if (!homingTimer.Passed(5))
        {
            this.Direction = (PlayerPosition.Y >= Position.Y ? MathHelper.Pi : 0) - (float)System.Math.Atan((double)((PlayerPosition.X - Position.X) / (PlayerPosition.Y - Position.Y)));
            animatedTexture.Rotation = this.Direction;
        }
        base.Update(gameTime);
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        HomingBullet.device = device;
        HomingBullet.StaticAssets = new BulletAssets();
        HomingBullet.StaticAssets.Bullet = content.Load<Texture2D>("HomingBullet");
        HomingBullet.StaticAssets.FrameCount = 3;
        HomingBullet.StaticAssets.Fps = 3;
    }
}
