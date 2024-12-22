using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class BossEnemyShip : EnemyShip
{
    public static SpaceshipAssets StaticAssets;
    private HealthBar healthBar;
    private BossEnemyWeapon ownWeapon;


    public BossEnemyShip(Vector2 spawn, float scale, float direction)
        : base(new Vector2(StaticAssets.Full.Width / 2, StaticAssets.Full.Height / 2),
                 spawn, scale, direction, 10, 10, 0, 0, 50, 500, 500, new BossEnemyWeapon(spawn, direction, scale, new Vector2(0, 0)), StaticAssets)
    {
        this.healthBar = new HealthBar(scale, Color.Red, 60, 3, Position, Health, Health);
        ownWeapon = (BossEnemyWeapon)Weapon;

    }

    protected override void Attack(GameTime gameTime)
    {
        this.Shoot(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        base.Draw(spriteBatch, gameTime);
        this.healthBar.Draw(spriteBatch, gameTime);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        this.healthBar.Position = new Vector2(Position.X, Position.Y - 30);
        this.healthBar.currentHealth = Health;
        this.Direction = (PlayerPosition.Y >= Position.Y ? MathHelper.Pi : 0) - (float)System.Math.Atan((double)((PlayerPosition.X - Position.X) / (PlayerPosition.Y - Position.Y)));
        ownWeapon.PlayerPosition = PlayerPosition;
        this.healthBar.Update(gameTime);
    }

    public override void Move(float direction, float elapsed)
    {
        base.Move(Direction, elapsed);
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        BossEnemyShip.device = device;
        BossEnemyWeapon.LoadContent(content, device);
        BossEnemyShip.StaticAssets = new SpaceshipAssets();
        BossEnemyShip.StaticAssets.Full = content.Load<Texture2D>("BossEnemy");
        BossEnemyShip.StaticAssets.SlightDamage = content.Load<Texture2D>("BossEnemy");
        BossEnemyShip.StaticAssets.Damaged = content.Load<Texture2D>("BossEnemy");
        BossEnemyShip.StaticAssets.VeryDamaged = content.Load<Texture2D>("BossEnemy");
    }
}
