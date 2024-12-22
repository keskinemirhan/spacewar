using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;
class FastEnemyShip : EnemyShip
{
    public static SpaceshipAssets StaticAssets;
    private HealthBar healthBar;
    private GameTimer evasionTimer;


    public FastEnemyShip(Vector2 spawn, float scale, float direction)
        : base(new Vector2(StaticAssets.Full.Width / 2, StaticAssets.Full.Height / 2),
                 spawn, scale, direction, 200, 300, 0, 0, 16, 100, 50, new BasicEnemyWeapon(spawn, direction, scale), StaticAssets)
    {
        this.healthBar = new HealthBar(scale, Color.Red, 60, 3, Position, Health, Health);
        this.evasionTimer = new GameTimer();
    }

    protected override void Attack(GameTime gameTime)
    {

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
        evasionTimer.StartTimer(gameTime);
        this.Direction = (PlayerPosition.Y >= Position.Y ? MathHelper.Pi : 0) - (float)System.Math.Atan((double)((PlayerPosition.X - Position.X) / (PlayerPosition.Y - Position.Y)));
        if (evasionTimer.Passed(5))
        {
            this.Direction += MathHelper.Pi / 2;
            this.Speed = 300;
            if (evasionTimer.Passed(7)) {
                this.Speed = 200;
                evasionTimer.Clear();
            }
        }
        this.healthBar.Update(gameTime);
    }

    public override void Move(float direction, float elapsed)
    {
        base.Move(Direction,elapsed);
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        FastEnemyShip.device = device;
        FastEnemyShip.StaticAssets = new SpaceshipAssets();
        FastEnemyShip.StaticAssets.Full = content.Load<Texture2D>("FastEnemy");
        FastEnemyShip.StaticAssets.SlightDamage = content.Load<Texture2D>("FastEnemy");
        FastEnemyShip.StaticAssets.Damaged = content.Load<Texture2D>("FastEnemy");
        FastEnemyShip.StaticAssets.VeryDamaged = content.Load<Texture2D>("FastEnemy");
    }
}
