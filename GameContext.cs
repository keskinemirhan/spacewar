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
                0,
                2);
        this.basicEnemyShip = new BasicEnemyShip(
                new Vector2(device.PreferredBackBufferWidth / 4, device.PreferredBackBufferHeight / 2),
                2,
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
        basicEnemyShip.PlayerPosition = playerSpaceship.Position;
        basicEnemyShip.Update(gameTime);
        if (CollisionDetector.CheckCollision(playerSpaceship, basicEnemyShip)) System.Console.WriteLine("Collision" + System.DateTime.Now);
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        PlayerSpaceship.LoadContent(content, device);
        BasicEnemyShip.LoadContent(content, device);
    }
}

