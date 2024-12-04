using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class GameContext : Context
{
    public string Username { get; set; }
    Spaceship spaceship;
    public GameContext(GraphicsDeviceManager device) : base(device)
    {
        this.spaceship = new Spaceship(device);
    }
    public override void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        spaceship.Draw(device, spriteBatch, gameTime);
    }

    public override void LoadContent(ContentManager content)
    {
        spaceship.LoadContent(content);
    }

    public override void Update(GameTime gameTime)
    {
        spaceship.Update(gameTime);
    }
}
