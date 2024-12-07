using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

class Rocket : IContext
{
    public static RocketAssets Assets;

    public float speed;
    private static GraphicsDeviceManager device;
    public int damage;
    public float direction;
    AnimatedTexture animatedTexture;
    Vector2 position;

    public Rocket(
            Vector2 origin,
            Vector2 position,
            float direction,
            float speed,
            int damage
            )
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        this.position = position;
        this.animatedTexture = new AnimatedTexture(origin,
                Assets.Rocket, direction, 1.0f, 0.5f, Assets.FrameCount, Assets.Fps);

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        animatedTexture.DrawFrame(spriteBatch, position);
    }

    public void Update(GameTime gameTime)
    {
        var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        var speedIncrease = speed * elapsed;
        position.Y -= (float)Math.Sin(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        position.X -= (float)Math.Cos(direction + 2 * MathHelper.Pi / 4) * speedIncrease;
        this.animatedTexture.UpdateFrame(elapsed);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Assets = new RocketAssets();
        Rocket.device = device;
        Assets.Rocket = content.Load<Texture2D>("ProjectileRocket");
        Assets.FrameCount = 3;
        Assets.Fps = 3;
    }
}

public class RocketAssets
{
    public Texture2D Rocket { get; set; }
    public int FrameCount { get; set; }
    public int Fps { get; set; }
}
