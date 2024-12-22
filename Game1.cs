using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

public class Game1 : Game
{
    IContext currentContext;
    GameContext gameContext;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ScoreboardManager scoreboardManager;
    private Texture2D backgroundImage;
    private AnimatedBackground background;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.IsFullScreen = true;
        Content.RootDirectory = "Content";
        this.scoreboardManager = new ScoreboardManager();
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Menu Init 


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        backgroundImage = Content.Load<Texture2D>("space");
        background = new AnimatedBackground(_graphics, 2, backgroundImage, 4, 1);
        MenuContext.LoadContent(Content, _graphics);
        GameContext.LoadContent(Content, _graphics);
        ScoreboardContext.LoadContent(Content, _graphics);
        var menuContext = new MenuContext();

        currentContext = menuContext;
        menuContext.startGame += (e, args) =>
        {
            gameContext = new GameContext(scoreboardManager, _graphics);
            gameContext.Username = args.Username;
            currentContext = gameContext;
            gameContext.endGame += (e, args) =>
            {
                var scoreboardContext = new ScoreboardContext(scoreboardManager, false);
                currentContext = scoreboardContext;
                scoreboardContext.closeScoreboard += (e, args) =>
                {
                    currentContext = menuContext;
                };

            };
        };

        menuContext.openScoreboard += (e, args) =>
        {
            var scoreboardContext = new ScoreboardContext(scoreboardManager, false);
            currentContext = scoreboardContext;
            scoreboardContext.closeScoreboard += (e, args) =>
            {
                currentContext = menuContext;
            };
        };



    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();
        background.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

        currentContext.Update(gameTime);

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
        background.DrawFrame(_spriteBatch);
        currentContext.Draw(_spriteBatch, gameTime);
        _spriteBatch.End();
        // TODO: Add your drawing code here
        //
        base.Draw(gameTime);
    }
}
