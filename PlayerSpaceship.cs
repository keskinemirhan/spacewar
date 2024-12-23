using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

class PlayerSpaceship : Spaceship
{
    public static SpaceshipAssets StaticAssets { get; private set; }

    private CannonWeapon cannonWeapon;
    private BigWeapon bigWeapon;
    private RocketWeapon rocketWeapon;
    private HealthBar healthBar;
    public PlayerSpaceship(Vector2 startingPosition, float startingDirection, float scale)
        : base(new Vector2(PlayerSpaceship.StaticAssets.Full.Width / 2, PlayerSpaceship.StaticAssets.Full.Height / 2),
                startingPosition, scale, startingDirection, 0, 300, 0, 10, 12, 1000, new CannonWeapon(startingPosition, startingDirection, scale), PlayerSpaceship.StaticAssets)
    {

        cannonWeapon = (CannonWeapon)Weapon;
        rocketWeapon = new RocketWeapon(startingPosition, startingDirection, scale);
        bigWeapon = new BigWeapon(startingPosition, startingDirection, scale);
        rocketWeapon.Hidden = true;
        bigWeapon.Hidden = true;
        this.healthBar = new HealthBar(scale, Color.Blue, 80, 5, Position, Health, Health);
    }

    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Weapon.Position = Position;
        Weapon.Direction = Direction;
        bigWeapon.Draw(spriteBatch, gameTime);
        rocketWeapon.Draw(spriteBatch, gameTime);
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
        cannonWeapon.Draw(spriteBatch, gameTime);
        this.healthBar.Draw(spriteBatch, gameTime);
    }

    public override void Update(GameTime gameTime)
    {
        this.Move(Direction, (float)gameTime.ElapsedGameTime.TotalSeconds);
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.Right) ||
                 keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.E) ||
            keyboardState.IsKeyDown(Keys.D1) ||
            keyboardState.IsKeyDown(Keys.D2) ||
            keyboardState.IsKeyDown(Keys.D3))
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.Acceleration = 200;
            }
            else this.Acceleration = 0;
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this.Acceleration = -80;
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
            if (keyboardState.IsKeyDown(Keys.D1))
            {
                Weapon.Hidden = true;
                Weapon = cannonWeapon;
                Weapon.Hidden = false;
            }
            if (keyboardState.IsKeyDown(Keys.D2))
            {
                Weapon.Hidden = true;
                Weapon = rocketWeapon;
                Weapon.Hidden = false;
            }
            if (keyboardState.IsKeyDown(Keys.D3))
            {
                Weapon.Hidden = true;
                Weapon = bigWeapon;
                Weapon.Hidden = false;
            }
        }
        bigWeapon.Update(gameTime);
        rocketWeapon.Update(gameTime);
        cannonWeapon.Update(gameTime);
        cannonWeapon.Position = Position;
        bigWeapon.Position = Position;
        rocketWeapon.Position = Position;
        cannonWeapon.Direction =Direction;
        bigWeapon.Direction = Direction;
        rocketWeapon.Direction = Direction;
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
        BigWeapon.LoadContent(content, device);
        CannonWeapon.LoadContent(content, device);
        HealthBar.LoadContent(content, device);
        PlayerSpaceship.StaticAssets = new SpaceshipAssets();
        PlayerSpaceship.StaticAssets.Full = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.Damaged = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.SlightDamage = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.VeryDamaged = content.Load<Texture2D>("MainShipFull");
    }
}
