using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace spacewar;

class RocketWeapon : IContext
{
    List<Rocket> rockets;
    public Vector2 Position { get; set; }
    public float Direction { get; set; }
    private static GraphicsDeviceManager device;
    private static RocketWeaponAssets assets;
    private float accumulatedSecs = 0f;
    private bool fired = false;

    public RocketWeapon(float direction, Vector2 position)
    {
        this.rockets = new List<Rocket>();
        this.Position = position;
        this.Direction = direction;
    }

    public RocketWeapon(RocketWeaponAssets assets, GraphicsDeviceManager device)
    {
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var rocket in rockets)
        {
            rocket.Draw(spriteBatch, gameTime);
        }
        foreach (var rocket in rockets)
        {
            rocket.Draw(spriteBatch, gameTime);
        }
        spriteBatch.Draw(
                assets.Weapon,
                Position,
                null,
                Color.White,
                Direction,
                new Vector2(assets.Weapon.Width / 2, assets.Weapon.Height / 2),
                1.0f,
                SpriteEffects.None,
                0f);
    }

    public void Shoot(GameTime gameTime)
    {
        if (!fired)
        {
            rockets.Add(new Rocket(new Vector2(Rocket.Assets.Rocket.Width / 2 - 20, Rocket.Assets.Rocket.Height / 2), Position, Direction, 100.0f, 100));
            rockets.Add(new Rocket(new Vector2(Rocket.Assets.Rocket.Width / 2 + 20, Rocket.Assets.Rocket.Height / 2), Position, Direction, 100.0f, 100));
            accumulatedSecs = 0f;
            fired = true;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (fired)
        {
            accumulatedSecs += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (accumulatedSecs >= 1.0f)
            {
                fired = false;
            }
        }
        var toBeRemoved = new List<Rocket>();
        foreach (var rocket in rockets)
        {
            if (rocket.Position.X < -Rocket.Assets.Rocket.Width
                    || rocket.Position.X > device.PreferredBackBufferWidth + Rocket.Assets.Rocket.Width
                    || rocket.Position.Y < -Rocket.Assets.Rocket.Height
                    || rocket.Position.Y > device.PreferredBackBufferWidth + Rocket.Assets.Rocket.Height)
            {
                toBeRemoved.Add(rocket);

            }
            rocket.Update(gameTime);
        }

        foreach (var rocket in toBeRemoved)
        {
            rockets.Remove(rocket);
        }
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        RocketWeapon.device = device;
        Rocket.LoadContent(content, device);
        assets = new RocketWeaponAssets();
        assets.Weapon = content.Load<Texture2D>("RocketWeapon");
        assets.FrameCount = 17;
        assets.Fps = 5;
    }
}

public class RocketWeaponAssets
{
    public Texture2D Weapon { get; set; }
    public int FrameCount { get; set; }
    public int Fps { get; set; }
}
