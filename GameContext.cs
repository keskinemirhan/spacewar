using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class GameContext : IContext
{
    public string Username { get; set; }
    PlayerSpaceship playerSpaceship;
    public GameContext(GraphicsDeviceManager device)
    {
        this.playerSpaceship = new PlayerSpaceship(
                new Vector2(device.PreferredBackBufferWidth / 2, device.PreferredBackBufferHeight / 2),
                MathHelper.Pi * 2,
                1);
    }
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        playerSpaceship.Draw(spriteBatch, gameTime);
    }

    public void Update(GameTime gameTime)
    {
        playerSpaceship.Update(gameTime);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        PlayerSpaceship.LoadContent(content, device);
    }
}

