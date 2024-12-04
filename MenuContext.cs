using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace spacewar;

class MenuContext : IContext
{

    Button startButton;
    Button scoreboardButton;
    TextInput usernameInput;
    public event EventHandler<StartGameEventArgs> startGame;

    public MenuContext()
    {
        // initialize start button
        startButton = new Button("Button", "Button_Hover", "Button_Click");
        startButton.SetFont("Menu", "Start Game", Color.White);
        startButton.SetScale(2.0f, 1.0f);

        scoreboardButton = new Button("Button", "Button_Hover", "Button_Click");
        scoreboardButton.SetFont("Menu", "Scoreboard", Color.White);
        scoreboardButton.SetScale(1.5f, 0.5f);

        usernameInput = new TextInput("Player Name", "Menu", Color.White, 1.0f);

    }

    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        var centerX = device.PreferredBackBufferWidth / 2;
        var centerY = device.PreferredBackBufferHeight / 2;
        startButton.SetPosition(centerX, centerY);
        scoreboardButton.SetPosition(centerX, centerY + 70);
        usernameInput.SetPosition(centerX, centerY - 120);
        startButton.Draw(device, spriteBatch, gameTime);
        scoreboardButton.Draw(device, spriteBatch, gameTime);
        usernameInput.Draw(device, spriteBatch, gameTime);


    }

    public void LoadContent(ContentManager content)
    {
        startButton.LoadContent(content);
        scoreboardButton.LoadContent(content);
        usernameInput.LoadContent(content);
    }

    public void Update(GameTime gameTime)
    {
        startButton.Update(gameTime);
        scoreboardButton.Update(gameTime);
        usernameInput.Update(gameTime);
        if (startButton.isClicked)
        {
            if (usernameInput.content.Length == 0)
            {
                usernameInput.SetColorMask(Color.Red);
            }
            else
            {
                var eventArgs = new StartGameEventArgs();
                eventArgs.Username = usernameInput.content;
                this.startGame?.Invoke(this, eventArgs);


            }
        }
    }
}

public class StartGameEventArgs
{
    public string Username { get; set; }
}
