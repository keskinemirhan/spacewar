using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

class PlayerSpaceship : Spaceship
{
    public static SpaceshipAssets StaticAssets { get; private set; }
    public PlayerSpaceship(Vector2 startingPosition, float startingDirection, float scale)
        : base(new Vector2(PlayerSpaceship.StaticAssets.Full.Width / 2, PlayerSpaceship.StaticAssets.Full.Height / 2),
                startingPosition, scale, startingDirection, 0, 200, 0, 10,
                new RocketWeapon(startingPosition, startingDirection, scale, scale), PlayerSpaceship.StaticAssets)
    {
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
                this.acceleration = 75;
            }
            else this.acceleration = 0;
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this.acceleration = -40;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                direction -= 2 * elapsed;
                float circle = MathHelper.Pi * 2;
                direction %= circle;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                direction += 2 * elapsed;
                float circle = MathHelper.Pi * 2;
                direction %= circle;
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                this.Shoot(gameTime);
            }
        }

    }

    public new static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Spaceship.device = device;
        RocketWeapon.LoadContent(content, device);
        PlayerSpaceship.StaticAssets = new SpaceshipAssets();
        PlayerSpaceship.StaticAssets.Full = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.Damaged = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.SlightDamage = content.Load<Texture2D>("MainShipFull");
        PlayerSpaceship.StaticAssets.VeryDamaged = content.Load<Texture2D>("MainShipFull");
    }
}
