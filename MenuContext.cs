using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spacewar;

class MenuContext : IContext
{

    Button startButton;
    Button scoreboardButton;

    public MenuContext()
    {
        // initialize start button
        startButton = new Button("Button", "Button_Hover", "Button_Click");
        startButton.SetFont("Menu", "Start Game", Color.White);
        startButton.SetScale(2.0f, 1.0f);

        scoreboardButton = new Button("Button", "Button_Hover", "Button_Click");
        scoreboardButton.SetFont("Menu", "Scoreboard", Color.White);
        scoreboardButton.SetScale(1.5f, 0.5f);

    }

    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        var centerX = device.PreferredBackBufferWidth / 2;
        var centerY = device.PreferredBackBufferHeight / 2;
        startButton.SetPosition(centerX, centerY);
        scoreboardButton.SetPosition(centerX, centerY + 70);
        startButton.Draw(device, spriteBatch, gameTime);
        scoreboardButton.Draw(device, spriteBatch, gameTime);

    }

    public void LoadContent(ContentManager content)
    {
        startButton.LoadContent(content);
        scoreboardButton.LoadContent(content);
    }

    public void Update(GameTime gameTime)
    {
        startButton.Update(gameTime);
        scoreboardButton.Update(gameTime);
    }
}
