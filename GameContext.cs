using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class GameContext : IContext
{
    public string Username { get; set; }
    Spaceship spaceship;
    public GameContext()
    {
        this.spaceship = new Spaceship();
    }
    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        spaceship.Draw(device, spriteBatch, gameTime);
    }

    public void LoadContent(ContentManager content)
    {
        spaceship.LoadContent(content);
    }

    public void Update(GameTime gameTime)
    {
        spaceship.Update(gameTime);
    }
}
