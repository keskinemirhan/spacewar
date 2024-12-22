using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace spacewar;

class GameContext : IContext
{
    public string Username { get; set; }
    PlayerSpaceship playerSpaceship;
    List<EnemyShip> enemyShips = new List<EnemyShip>();
    private int playerScore = 0;
    private GraphicsDeviceManager device;
    public event EventHandler<EndGameEventArgs> endGame;
    public static SpriteFont ScoreFont;
    private ScoreboardManager scoreboardManager;
    private GameTimer basicEnemyTimer = new GameTimer();
    private GameTimer fastEnemyTimer = new GameTimer();
    private GameTimer strongEnemyTimer = new GameTimer();
    private GameTimer bossEnemyTimer = new GameTimer();

    public GameContext(ScoreboardManager scoreboardManager, GraphicsDeviceManager device)
    {
        this.playerSpaceship = new PlayerSpaceship(
                new Vector2(device.PreferredBackBufferWidth / 2, device.PreferredBackBufferHeight / 2),
                0,
                2);
        this.device = device;
        this.scoreboardManager = scoreboardManager;

    }
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {


        playerSpaceship.Draw(spriteBatch, gameTime);
        foreach (var enemyShip in enemyShips)
            enemyShip.Draw(spriteBatch, gameTime);

        Vector2 textpos = new Vector2(20, 20);
        string text = "Score : " + playerScore;
        Vector2 textorigin = new Vector2(0, 0);
        spriteBatch.DrawString(ScoreFont, text,
                textpos, Color.White, 0, textorigin, 1, SpriteEffects.None, 0.5f);
    }

    public void Update(GameTime gameTime)
    {
        SpawnStrategy(gameTime);
        playerSpaceship.Update(gameTime);
        var enemiesToRemove = new List<EnemyShip>();
        var orphanBulletsToRemove = new List<Bullet>();

        foreach (var enemyShip in enemyShips)
        {
            enemyShip.PlayerPosition = playerSpaceship.Position;
            enemyShip.Update(gameTime);
            if (CollisionDetector.CheckCollision(playerSpaceship, enemyShip))
            {
                playerSpaceship.TakeDamage(1000);
            }
            var bulletsToRemove = new List<Bullet>();
            var enemyBulletsToRemove = new List<Bullet>();
            foreach (var bullet in playerSpaceship.Weapon.Bullets)
            {
                if (CollisionDetector.CheckCollision(bullet, enemyShip))
                {
                    enemyShip.TakeDamage(bullet.Damage);
                    if (enemyShip.Health == 0) enemiesToRemove.Add(enemyShip);
                    bulletsToRemove.Add(bullet);
                }

            }
            foreach (var bullet in enemyShip.Weapon.Bullets)
            {
                if (CollisionDetector.CheckCollision(playerSpaceship, bullet))
                {
                    playerSpaceship.TakeDamage(bullet.Damage);
                    enemyBulletsToRemove.Add(bullet);
                }

            }
            foreach (var bullet in bulletsToRemove) playerSpaceship.Weapon.Bullets.Remove(bullet);
            foreach (var bullet in enemyBulletsToRemove) enemyShip.Weapon.Bullets.Remove(bullet);

        }
        foreach (var enemyShip in enemiesToRemove)
        {
            enemyShips.Remove(enemyShip);
            playerScore += enemyShip.ExperiencePrize;
        }
        if (playerSpaceship.Health <= 0)
        {
            this.EndGame();
        }


    }

    public void EndGame()
    {
        scoreboardManager.Add(Username, playerScore);
        var eventArgs = new EndGameEventArgs();
        eventArgs.PlayerScore = playerScore;
        endGame.Invoke(this, eventArgs);
    }

    public void SpawnStrategy(GameTime gameTime)
    {
        basicEnemyTimer.StartTimer(gameTime);
        fastEnemyTimer.StartTimer(gameTime);
        strongEnemyTimer.StartTimer(gameTime);
        bossEnemyTimer.StartTimer(gameTime);
        basicEnemyTimer.StartTimer(gameTime);

        if (basicEnemyTimer.Passed(10))
        {
            this.enemyShips.Add(new BasicEnemyShip(
                    SpawnSelector(),
                    2,
                    MathHelper.Pi
                    ));
            basicEnemyTimer.Clear();
        }

        if (fastEnemyTimer.Passed(20))
        {
            this.enemyShips.Add(new FastEnemyShip(
                    SpawnSelector(),
                    2,
                    MathHelper.Pi
                    ));
            fastEnemyTimer.Clear();
        }
        if (strongEnemyTimer.Passed(30))
        {
            this.enemyShips.Add(new StrongEnemyShip(
                    SpawnSelector(),
                    2,
                    MathHelper.Pi
                    ));
            strongEnemyTimer.Clear();
        }
        if (bossEnemyTimer.Passed(60))
        {
            this.enemyShips.Add(new BossEnemyShip(
                    SpawnSelector(),
                    2,
                    MathHelper.Pi
                    ));
            bossEnemyTimer.Clear();
        }

    }


    public Vector2 SpawnSelector()
    {
        var random = new Random();
        var playerPosition = playerSpaceship.Position;
        var isVertical = random.NextInt64(2) == 1;
        int X = 0;
        int Y = 0;
        if (isVertical)
        {
            X = (int)random.NextInt64(device.PreferredBackBufferWidth);
            var isTop = random.NextInt64(2) == 1;
            if (isTop) Y = 10;
            else Y = device.PreferredBackBufferHeight - 10;
        }
        else
        {
            Y = (int)random.NextInt64(device.PreferredBackBufferHeight);
            var isLeft = random.NextInt64(2) == 1;
            if (isLeft) X = 10;
            else X = device.PreferredBackBufferWidth - 10;
        }
        var distanceFromPlayer = Math.Sqrt(Math.Pow(playerPosition.X - X, 2) + Math.Pow(playerPosition.Y - Y, 2));
        if (distanceFromPlayer < 200) return SpawnSelector();
        return new Vector2(X, Y);
    }
    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        ScoreFont = content.Load<SpriteFont>("Menu");
        PlayerSpaceship.LoadContent(content, device);
        BasicEnemyShip.LoadContent(content, device);
        FastEnemyShip.LoadContent(content, device);
        StrongEnemyShip.LoadContent(content, device);
        BossEnemyShip.LoadContent(content, device);
    }
}

public class EndGameEventArgs
{
    public int PlayerScore { get; set; }
}
