using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;
class Button : IContext
{
    Texture2D bgTexture;
    Texture2D bgTextureHover;
    Texture2D bgTextureClick;
    Texture2D bgTextureNormal;

    string bgTextureNormalStr;
    string bgTextureHoverStr;
    string bgTextureClickStr;

    Vector2 position = new Vector2(0, 0);

    bool isHovered = false;
    bool isClicked = false;

    bool hasText = false;
    Color colormask;
    SpriteFont spriteFont;
    string text;
    string fontName;

    float bgScale = 1.0f;
    float fontScale = 1.0f;

    public Button(string bgTextureNormalStr, string bgTextureHoverStr, string bgTextureClickStr)
    {
        this.bgTextureNormalStr = bgTextureNormalStr;
        this.bgTextureHoverStr = bgTextureHoverStr;
        this.bgTextureClickStr = bgTextureClickStr;
    }

    public void SetFont(string fontName, string text, Color colormask)
    {
        this.fontName = fontName;
        this.text = text;
        this.colormask = colormask;
        this.hasText = true;
    }

    public void SetScale(float bgScale, float fontScale)
    {
        this.fontScale = fontScale;
        this.bgScale = bgScale;
    }

    public void SetPosition(float X, float Y)
    {
        position = new Vector2(X, Y);
    }

    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        bgTexture = isHovered ? isClicked ? bgTextureClick : bgTextureHover : bgTextureNormal;
        spriteBatch.Draw(
                bgTexture,
                position,
                null,
                Color.White,
                0f,
                new Vector2(bgTexture.Width / 2, bgTexture.Height / 2),
                bgScale,
                SpriteEffects.None,
                0f);
        if (hasText)
        {
            Vector2 textpos = new Vector2(position.X,
                     isClicked ? position.Y + bgScale :position.Y);
            Vector2 textorigin = spriteFont.MeasureString(text) / 2;

            spriteBatch.DrawString(spriteFont, text,
                    textpos, colormask, 0, textorigin, fontScale, SpriteEffects.None, 0.5f);
        }

    }

    public void LoadContent(ContentManager content)
    {
        spriteFont = content.Load<SpriteFont>(fontName);
        bgTextureNormal = content.Load<Texture2D>(bgTextureNormalStr);
        bgTextureHover = content.Load<Texture2D>(bgTextureHoverStr);
        bgTextureClick = content.Load<Texture2D>(bgTextureClickStr);
        bgTexture = bgTextureNormal;
    }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();
        var XRight = position.X + bgTexture.Width * bgScale / 2;
        var XLeft = position.X - bgTexture.Width * bgScale / 2;
        var YBottom = position.Y + bgTexture.Height * bgScale / 2;
        var YTop = position.Y - bgTexture.Height * bgScale / 2;

        if (mouseState.X >= XLeft && mouseState.X <= XRight && mouseState.Y <= YBottom && mouseState.Y >= YTop)
        {
            isHovered = true;
            if (mouseState.LeftButton == ButtonState.Pressed) isClicked = true;
        }
        else
        {
            isHovered = false;
            isClicked = false;
        }
    }
}
