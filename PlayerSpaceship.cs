using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

class PlayerSpaceship : Spaceship
{
    public PlayerSpaceship(Vector2 startingPosition, float startingDirection, float scale)
        : base(new Vector2(PlayerSpaceship.Assets.Full.Width / 2, PlayerSpaceship.Assets.Full.Height / 2),
                startingPosition, scale, startingDirection, 0, 200, 0, 10,
                new RocketWeapon(startingPosition, startingDirection, scale, scale))
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
        Assets = new SpaceshipAssets();
        Assets.Full = content.Load<Texture2D>("MainShipFull");
        Assets.Damaged = content.Load<Texture2D>("MainShipFull");
        Assets.SlightDamage = content.Load<Texture2D>("MainShipFull");
        Assets.VeryDamaged = content.Load<Texture2D>("MainShipFull");
    }
}
