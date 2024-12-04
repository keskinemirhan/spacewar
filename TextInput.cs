using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

class TextInput : IContext
{
    public string content = "";
    string title = "Text";

    string fontName;
    SpriteFont spriteFont;
    Color colormask;
    float fontScale;
    Vector2 position = new Vector2(0, 0);
    bool keydown = false;
    Keys pressedKey = Keys.None;

    public TextInput(string title, string fontName, Color colormask, float fontScale)
    {
        this.title = title;
        this.fontName = fontName;
        this.colormask = colormask;
        this.fontScale = fontScale;
    }

    public void SetPosition(float X, float Y)
    {
        this.position = new Vector2(X, Y);
    }

    public void SetColorMask(Color colormask)
    {
        this.colormask = colormask;
    }

    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime)
    {
        Vector2 textpos = new Vector2(position.X, position.Y);
        string text = title + " : " + content + "_";
        Vector2 textorigin = spriteFont.MeasureString(text) / 2;

        spriteBatch.DrawString(spriteFont, text,
                textpos, colormask, 0, textorigin, fontScale, SpriteEffects.None, 0.5f);
    }

    public void LoadContent(ContentManager content)
    {
        spriteFont = content.Load<SpriteFont>(fontName);
    }

    public void Update(GameTime gameTime)
    {
        var keys = Keyboard.GetState().GetPressedKeys();
        if (keys.Length == 0) keydown = false;
        else
        {
            Keys key = keys[0];
            if ((int)key >= 65 && (int)key <= 90 && (key != pressedKey || !keydown))
            {
                this.content += (char)key;
            }
            else if (key == Keys.Back && (key != pressedKey || !keydown))
            {
                if (this.content.Length > 0) this.content = this.content.Remove(this.content.Length - 1);
            }
            keydown = true;
            pressedKey = key;
        }
    }
}
