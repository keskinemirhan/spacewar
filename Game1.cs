using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

public class Game1 : Game
{
    IContext currentContext;
    GameContext gameContext;
    MenuContextAssets menuAssets;
    GameContextAssets gameAssets;
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
        // Menu Init 


        base.Initialize();
    }

    protected override void LoadContent()
    {
        menuAssets = new MenuContextAssets();
        gameAssets = new GameContextAssets();
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Menu Assets
        var menuFont = Content.Load<SpriteFont>("Menu");
        menuAssets.UsernameInput.Font = menuFont;
        menuAssets.ScoreboardBtn.Font = menuFont;
        menuAssets.ScoreboardBtn.Normal = Content.Load<Texture2D>("Button");
        menuAssets.ScoreboardBtn.Pressed = Content.Load<Texture2D>("Button_Click");
        menuAssets.ScoreboardBtn.Hover = Content.Load<Texture2D>("Button_Hover");
        menuAssets.StartGameBtn = menuAssets.ScoreboardBtn;
        menuAssets.UsernameInput.Font = menuFont;

        // Game Assets
        gameAssets.SpaceshipAssets.Full = Content.Load<Texture2D>("MainShipFull");
        gameAssets.SpaceshipAssets.Damaged = Content.Load<Texture2D>("MainShipFull");
        gameAssets.SpaceshipAssets.SlightDamage = Content.Load<Texture2D>("MainShipFull");
        gameAssets.SpaceshipAssets.VeryDamaged = Content.Load<Texture2D>("MainShipFull");

        var menuContext = new MenuContext(menuAssets, _graphics);
        currentContext = menuContext;
        gameContext = new GameContext(gameAssets, _graphics);
        menuContext.startGame += (e, args) =>
        {
            gameContext.Username = args.Username;
            currentContext = gameContext;
        };
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
        currentContext.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();
        // TODO: Add your drawing code here
        //
        base.Draw(gameTime);
    }
}
