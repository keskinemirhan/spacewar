using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class GameContext : IContext
{
    public string Username { get; set; }
    Spaceship spaceship;
    public GameContext(GameContextAssets assets, GraphicsDeviceManager device)
    {
        this.spaceship = new Spaceship(assets.SpaceshipAssets, device);
    }
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spaceship.Draw(spriteBatch, gameTime);
    }

    public void Update(GameTime gameTime)
    {
        spaceship.Update(gameTime);
    }
}

public class GameContextAssets
{
    public SpaceshipAssets SpaceshipAssets { get; set; } = new SpaceshipAssets();
}
