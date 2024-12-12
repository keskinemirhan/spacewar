using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BasicEnemyShip : EnemyShip
{
    public static SpaceshipAssets StaticAssets;
    public BasicEnemyShip(Vector2 spawn, float scale, float direction)
        : base(new Vector2(StaticAssets.Full.Width / 2, StaticAssets.Full.Height / 2),
                spawn, scale, direction, 100, 100, 0, 0, new BasicEnemyWeapon(spawn, direction, scale, scale), StaticAssets)
    {
    }

    protected override void Attack(GameTime gameTime)
    {
        this.Shoot(gameTime);

    }

    public override void Move(float direction, float elapsed)
    {
        if (position.X + origin.X >= device.PreferredBackBufferWidth) speed = -speed;
        else if (position.X - origin.X <= 0) speed = -speed;
        position.X += speed * elapsed;
        this.direction = (PlayerPosition.Y >= position.Y ? MathHelper.Pi : 0) - (float)System.Math.Atan((double)((PlayerPosition.X - position.X) / (PlayerPosition.Y - position.Y)));
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
