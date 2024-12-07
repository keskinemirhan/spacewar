using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace spacewar;

class Spaceship : IContext
{
    Texture2D currentTexture;
    Vector2 position;
    List<Rocket> bullets;
    RocketWeapon rocketWeapon;
    private static SpaceshipAssets assets;
    private static GraphicsDeviceManager device;

    public int Health { get; private set; }
    public int Damage { get; private set; }
    float speed = 100.0f;
    float direction = MathHelper.Pi * 2;

    public Spaceship()
    {
        position.X = device.PreferredBackBufferWidth / 2;
        position.Y = device.PreferredBackBufferHeight / 2;
        bullets = new List<Rocket>();
        currentTexture = assets.Full;
        this.rocketWeapon = new RocketWeapon(direction, position);
    }
    public void Shoot(GameTime gameTime)
    {
        this.rocketWeapon.Shoot(gameTime);
    }

    public void Move(float rotation, float elapsed)
    {
        var speedIncrease = elapsed * speed;
        position.Y -= (float)Math.Sin(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        position.X -= (float)Math.Cos(direction + 2 * MathHelper.Pi / 4) * speedIncrease;

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        rocketWeapon.Position = position;
        rocketWeapon.Direction = direction;
        rocketWeapon.Draw(spriteBatch, gameTime);
        foreach (var bullet in bullets)
        {
            bullet.Draw(spriteBatch, gameTime);
        }
        spriteBatch.Draw(
                currentTexture,
                position,
                null,
                Color.White,
                direction,
                new Vector2(currentTexture.Width / 2, currentTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );

    }

    public void Update(GameTime gameTime)
    {
        foreach (var bullet in bullets)
        {
            bullet.Update(gameTime);

        }
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.Right) ||
                keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.E))
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var speedIncrease = speed * elapsed;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.Move(direction, elapsed);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                direction -= elapsed;
                float circle = MathHelper.Pi * 2;
                direction %= circle;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                direction += elapsed;
                float circle = MathHelper.Pi * 2;
                direction %= circle;
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                this.Shoot(gameTime);
            }
        }
        rocketWeapon.Update(gameTime);

    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Spaceship.device = device;
        RocketWeapon.LoadContent(content, device);
        assets = new SpaceshipAssets();
        assets.Full = content.Load<Texture2D>("MainShipFull");
        assets.Damaged = content.Load<Texture2D>("MainShipFull");
        assets.SlightDamage = content.Load<Texture2D>("MainShipFull");
        assets.VeryDamaged = content.Load<Texture2D>("MainShipFull");
    }
}

public class SpaceshipAssets
{
    public Texture2D Full { get; set; }
    public Texture2D SlightDamage { get; set; }
    public Texture2D Damaged { get; set; }
    public Texture2D VeryDamaged { get; set; }
}
