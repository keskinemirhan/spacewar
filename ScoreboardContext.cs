using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace spacewar;

class ScoreboardContext : IContext
{
    public static Texture2D ScoreboardPanel;
    public static SpriteFont ScoreFont;
    public static GraphicsDeviceManager device;
    private bool gameOver;
    private int playerScore;
    private string title;
    private string scores;
    private ScoreboardManager scoreboardManager;
    public event EventHandler<EventArgs> closeScoreboard;

    public ScoreboardContext(ScoreboardManager scoreboardManager, bool gameOver, int playerScore = 0)
    {
        this.gameOver = gameOver;
        this.playerScore = playerScore;
        this.scoreboardManager = scoreboardManager;
        var scoreList = scoreboardManager.GetSorted();
        scores = gameOver ? "Game Over\nYour Score: \n\n" + playerScore : "Scoreboard\n\n";
        var count = 0;
        foreach (var score in scoreList)
        {
            if (count > 5) break;
            scores = scores + "\n" + score.Username + " " + score.Score;
            count++;
        }
    }

    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        ScoreboardPanel = content.Load<Texture2D>("ScoreboardPanel");
        ScoreFont = content.Load<SpriteFont>("Menu");
        ScoreboardContext.device = device;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Vector2 scoresPosition = new Vector2(device.PreferredBackBufferWidth / 2, device.PreferredBackBufferHeight / 2);
        Vector2 scoresOrigin = ScoreFont.MeasureString(scores) / 2;

        spriteBatch.Draw(
                ScoreboardPanel,
                scoresPosition ,
                null,
                Color.White,
                0f,
                new Vector2(ScoreboardPanel.Width / 2, ScoreboardPanel.Height / 2),
                3,
                SpriteEffects.None,
                0f);
        spriteBatch.DrawString(ScoreFont, scores,
                scoresPosition, Color.White, 0, scoresOrigin, 1, SpriteEffects.None, 0.5f);
    }

    public void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            this.closeScoreboard.Invoke(this, new EventArgs());
        }
    }
}
