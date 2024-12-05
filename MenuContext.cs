using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace spacewar;

class MenuContext : IContext
{

    Button startButton;
    Button scoreboardButton;
    TextInput usernameInput;
    private GraphicsDeviceManager device;
    public event EventHandler<StartGameEventArgs> startGame;

    public MenuContext(MenuContextAssets assets, GraphicsDeviceManager device)
    {
        this.device = device;
        // initialize start button
        var centerX = device.PreferredBackBufferWidth / 2;
        var centerY = device.PreferredBackBufferHeight / 2;
        startButton = new Button(assets.StartGameBtn, new Vector2(centerX, centerY), device, Color.White, "Start Game");
        startButton.BackgroundScale = 2.0f;

        scoreboardButton = new Button(assets.ScoreboardBtn, new Vector2(centerX, centerY + 70), device, Color.White, "Scoreboard");
        scoreboardButton.BackgroundScale = 1.5f;
        scoreboardButton.FontScale = 0.5f;

        usernameInput = new TextInput("Player Name", assets.UsernameInput, new Vector2(centerX, centerY - 120), device);

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        startButton.Draw(spriteBatch, gameTime);
        scoreboardButton.Draw(spriteBatch, gameTime);
        usernameInput.Draw(spriteBatch, gameTime);
    }


    public void Update(GameTime gameTime)
    {
        startButton.Update(gameTime);
        scoreboardButton.Update(gameTime);
        usernameInput.Update(gameTime);
        if (startButton.IsClicked)
        {
            if (usernameInput.Content.Length == 0)
            {
                usernameInput.Colormask = Color.Red;
            }
            else
            {
                var eventArgs = new StartGameEventArgs();
                eventArgs.Username = usernameInput.Content;
                this.startGame?.Invoke(this, eventArgs);


            }
        }
    }
}

public class StartGameEventArgs
{
    public string Username { get; set; }
}

public class MenuContextAssets
{
    public ButtonAssets StartGameBtn { get; set; } = new ButtonAssets();
    public ButtonAssets ScoreboardBtn { get; set; } = new ButtonAssets();
    public TextInputAssets UsernameInput { get; set; } = new TextInputAssets();
}
