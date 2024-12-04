﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

public class Game1 : Game
{
    IContext currentContext;
    GameContext gameContext;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        var menuContext = new MenuContext();
        currentContext = menuContext;
        gameContext = new GameContext();
        menuContext.startGame += (e, args) =>
        {
            gameContext.Username = args.Username;
            currentContext = gameContext;
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        currentContext.LoadContent(Content);
        gameContext.LoadContent(Content);


        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        currentContext.Update(gameTime);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        currentContext.Draw(_graphics, _spriteBatch, gameTime);
        _spriteBatch.End();
        // TODO: Add your drawing code here
        //
        base.Draw(gameTime);
    }
}
