using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace spacewar;

class Spaceship : IContext
{
    Texture2D fullHealthTexture;
    Vector2 position = new Vector2(120, 120);

    public int Health { get; private set; }
    public int Damage { get; private set; }
    float speed = 100.0f;
    float direction = MathHelper.Pi * 2;


    public void Move(float rotation, float elapsed)
    {
        var speedIncrease = elapsed * speed;
        position.Y -= (float)Math.Sin(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        position.X -= (float)Math.Cos(direction + 2 * MathHelper.Pi / 4) * speedIncrease;

    }

    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Draw(
                fullHealthTexture,
                position,
                null,
                Color.White,
                direction,
                new Vector2(fullHealthTexture.Width / 2, fullHealthTexture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f
                );

    }

    public void LoadContent(ContentManager content)
    {
        fullHealthTexture = content.Load<Texture2D>("MainShipFull");

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
