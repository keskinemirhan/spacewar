using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class GameContext : IContext
{
    public string Username { get; set; }
    Spaceship spaceship;
    public GameContext(GameContextAssets assets, GraphicsDeviceManager device)
    {
        this.spaceship = new Spaceship();
    }
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spaceship.Draw(spriteBatch, gameTime);
    }

    public void Update(GameTime gameTime)
    {
        spaceship.Update(gameTime);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        Spaceship.LoadContent(content, device);
    }
}

public class GameContextAssets
{
    public SpaceshipAssets SpaceshipAssets { get; set; } = new SpaceshipAssets();
}
