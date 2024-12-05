using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace spacewar;

class Spaceship : IContext
{
    Texture2D currentTexture;
    SpaceshipAssets assets;
    Vector2 position;
    List<Bullet> bullets;
    GraphicsDeviceManager device;

    public int Health { get; private set; }
    public int Damage { get; private set; }
    float speed = 100.0f;
    float direction = MathHelper.Pi * 2;

    public Spaceship(SpaceshipAssets assets, GraphicsDeviceManager device)
    {
        position.X = device.PreferredBackBufferWidth / 2;
        position.Y = device.PreferredBackBufferHeight / 2;
        bullets = new List<Bullet>();
        this.device = device;
        currentTexture = assets.Full;
        this.assets = assets;
    }

    public void Move(float rotation, float elapsed)
    {
        var speedIncrease = elapsed * speed;
        position.Y -= (float)Math.Sin(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        position.X -= (float)Math.Cos(direction + 2 * MathHelper.Pi / 4) * speedIncrease;

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
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
        var keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.Left))
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var speedIncrease = speed * elapsed;
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
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.Move(direction, elapsed);
            }
        }

    }
}

public class SpaceshipAssets
{
    public Texture2D Full { get; set; }
    public Texture2D SlightDamage { get; set; }
    public Texture2D Damaged { get; set; }
    public Texture2D VeryDamaged { get; set; }
}
