using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

class PlayerSpaceship : Spaceship
{
    public static SpaceshipAssets StaticAssets { get; private set; }

    private HealthBar healthBar;
    public PlayerSpaceship(Vector2 startingPosition, float startingDirection, float scale)
        : base(new Vector2(PlayerSpaceship.StaticAssets.Full.Width / 2, PlayerSpaceship.StaticAssets.Full.Height / 2),
                startingPosition, scale, startingDirection, 0, 300, 0, 10, 12, 1000, new RocketWeapon(startingPosition, startingDirection, scale), PlayerSpaceship.StaticAssets)
    {

        this.healthBar = new HealthBar(scale, Color.Blue, 80, 5, Position, Health, Health);
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Weapon.Position = Position;
        Weapon.Direction = Direction;
        Weapon.Draw(spriteBatch, gameTime);
        spriteBatch.Draw(
                currentTexture,
                Position,
                null,
                Color.White,
                Direction,
                Origin,
                Scale,
                SpriteEffects.None,
                0f
                );
        this.healthBar.Draw(spriteBatch, gameTime);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.Right) ||
                 keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.E))
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.Acceleration = 75;
            }
            else this.Acceleration = 0;
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this.Acceleration = -40;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Direction -= 2 * elapsed;
                float circle = MathHelper.Pi * 2;
                Direction %= circle;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Direction += 2 * elapsed;
                float circle = MathHelper.Pi * 2;
                Direction %= circle;
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                this.Shoot(gameTime);
            }
        }
        healthBar.Update(gameTime);
        healthBar.currentHealth = Health;
        healthBar.Position = new Vector2(Position.X, Position.Y - 30);
        if ((Position.X <= 0 || Position.X >= device.PreferredBackBufferWidth)
            || (Position.Y <= 0 || Position.Y >= device.PreferredBackBufferHeight))
        {
            Speed = 0f;
        }
    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Spaceship.device = device;
        RocketWeapon.LoadContent(content, device);
        HealthBar.LoadContent(content, device);
        PlayerSpaceship.StaticAssets = new SpaceshipAssets();
        PlayerSpaceship.StaticAssets.Full = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.Damaged = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.SlightDamage = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.VeryDamaged = content.Load<Texture2D>("MainShipFull");
    }
}
