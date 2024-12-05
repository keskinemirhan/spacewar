using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;
class Button : IContext
{
    public float BackgroundScale { get; set; } = 1.0f;
    public float FontScale { get; set; } = 1.0f;
    public Vector2 Position { get; set; }
    public bool IsClicked { get; private set; } = false;
    public Color Colormask { get; set; }

    private ButtonAssets assets;
    private Texture2D currentTexture;
    private bool isHovered = false;
    private bool hasText = false;
    private string text;
    private GraphicsDeviceManager device;


    public Button(ButtonAssets assets, Vector2 position, GraphicsDeviceManager device, Color? colormask = null, string text = null)
    {
        this.assets = assets;
        this.device = device;
        this.Position = position;
        if (colormask == null) this.Colormask = Color.White;
        else this.Colormask = (Color)colormask;
        if (this.assets.Font != null || text != null)
        {
            this.hasText = true;
            this.text = text;
        }
        currentTexture = assets.Normal;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        currentTexture = isHovered ? IsClicked ? assets.Pressed : assets.Pressed : assets.Normal;
        spriteBatch.Draw(
                currentTexture,
                Position,
                null,
                Color.White,
                0f,
                new Vector2(currentTexture.Width / 2, currentTexture.Height / 2),
                BackgroundScale,
                SpriteEffects.None,
                0f);
        if (hasText)
        {
            Vector2 textpos = new Vector2(Position.X,
                     IsClicked ? Position.Y + BackgroundScale : Position.Y);
            Vector2 textorigin = assets.Font.MeasureString(text) / 2;

            spriteBatch.DrawString(assets.Font, text,
                    textpos, Colormask, 0, textorigin, FontScale, SpriteEffects.None, 0.5f);
        }

    }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        var XRight = Position.X + currentTexture.Width * BackgroundScale / 2;
        var XLeft = Position.X - currentTexture.Width * BackgroundScale / 2;
        var YBottom = Position.Y + currentTexture.Height * BackgroundScale / 2;
        var YTop = Position.Y - currentTexture.Height * BackgroundScale / 2;

        if (mouseState.X >= XLeft && mouseState.X <= XRight && mouseState.Y <= YBottom && mouseState.Y >= YTop)
        {
            isHovered = true;
            if (mouseState.LeftButton == ButtonState.Pressed) IsClicked = true;
        }
        else
        {
            isHovered = false;
            IsClicked = false;
        }
    }
}

public class ButtonAssets
{
    public Texture2D Normal { get; set; }
    public Texture2D Pressed { get; set; }
    public Texture2D Hover { get; set; }
    public SpriteFont Font { get; set; }
}
