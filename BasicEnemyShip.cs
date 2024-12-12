using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BasicEnemyShip : EnemyShip
{
    public static SpaceshipAssets StaticAssets;
    public BasicEnemyShip(Vector2 spawn, float scale, float direction)
        : base(new Vector2(StaticAssets.Full.Width / 2, StaticAssets.Full.Height / 2),
                 spawn, scale, direction, 100, 100, 0, 0, new BasicEnemyWeapon(spawn, direction, scale), StaticAssets)
    {
    }

    protected override void Attack(GameTime gameTime)
    {
        this.Shoot(gameTime);
    }

    public override void Move(float direction, float elapsed)
    {
        if (Position.X + origin.X >= device.PreferredBackBufferWidth) Speed = -Speed;
        else if (Position.X - origin.X <= 0) Speed = -Speed;
        Position.X += Speed * elapsed;
        this.Direction = (PlayerPosition.Y >= Position.Y ? MathHelper.Pi : 0) - (float)System.Math.Atan((double)((PlayerPosition.X - Position.X) / (PlayerPosition.Y - Position.Y)));
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BasicEnemyShip.device = device;
        BasicEnemyWeapon.LoadContent(content, device);
        BasicEnemyShip.StaticAssets = new SpaceshipAssets();
        BasicEnemyShip.StaticAssets.Full = content.Load<Texture2D>("BasicEnemy");
        BasicEnemyShip.StaticAssets.SlightDamage = content.Load<Texture2D>("BasicEnemy");
        BasicEnemyShip.StaticAssets.Damaged = content.Load<Texture2D>("BasicEnemy");
        BasicEnemyShip.StaticAssets.VeryDamaged = content.Load<Texture2D>("BasicEnemy");
    }
}
