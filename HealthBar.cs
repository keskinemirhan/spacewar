using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

class HealthBar : IContext
{
    public Vector2 Position;
    public int MaxHealth;
    public int currentHealth;
    public Texture2D bar;
    public Texture2D bgBar;

    private Color color;
    private static GraphicsDeviceManager device;
    private int height;
    private int width;

    public HealthBar(float scale, Color color, int width, int height, Vector2 position, int maxHealth, int currentHealth)
    {
        this.Position = position;
        this.color = color;
        this.bar = new Texture2D(device.GraphicsDevice, 1, 1);
        this.bar.SetData(new Color[] { color });
        this.currentHealth = currentHealth;
        this.bgBar = new Texture2D(device.GraphicsDevice, 1, 1);
        this.bgBar.SetData(new Color[] { Color.Gray });
        this.MaxHealth = maxHealth;
        this.width = (int)((float)width * scale);
        this.height = (int)((float)height * scale);
    }
    public static void LoadContent(ContentManager content, GraphicsDeviceManager device)
    {
        HealthBar.device = device;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        var barWidth = (int)((float)width * (float)currentHealth / (float)MaxHealth);
        var bgBarWidth = width;
        spriteBatch.Draw(bgBar, new Vector2(this.Position.X - width / 2, this.Position.Y - height / 2), new Rectangle(0, 0, bgBarWidth, height), Color.White);
        spriteBatch.Draw(bar, new Vector2(this.Position.X - width / 2, this.Position.Y - height / 2), new Rectangle(0, 0, barWidth, height), Color.White);
    }

    public void Update(GameTime gameTime)
    {
    }
}
