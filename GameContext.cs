using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class GameContext : IContext
{
    public string Username { get; set; }
    PlayerSpaceship playerSpaceship;
    BasicEnemyShip basicEnemyShip;
    public GameContext(GraphicsDeviceManager device)
    {
        this.playerSpaceship = new PlayerSpaceship(
                new Vector2(device.PreferredBackBufferWidth / 2, device.PreferredBackBufferHeight / 2),
                MathHelper.Pi * 2,
                1);
        this.basicEnemyShip = new BasicEnemyShip(
                new Vector2(device.PreferredBackBufferWidth / 4, device.PreferredBackBufferHeight / 2),
                1,
                MathHelper.Pi
                );

    }
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        playerSpaceship.Draw(spriteBatch, gameTime);
        basicEnemyShip.Draw(spriteBatch, gameTime);
    }

    public void Update(GameTime gameTime)
    {
        playerSpaceship.Update(gameTime);
        basicEnemyShip.PlayerPosition = playerSpaceship.position;
        basicEnemyShip.Update(gameTime);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        PlayerSpaceship.LoadContent(content, device);
        BasicEnemyShip.LoadContent(content, device);
    }
}

