using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spacewar;

class TextInput : IGameObject
{
    public string Content { get; set; } = "";
    public string Title { get; set; } = "Text";
    public Color Colormask { get; set; }
    public float FontScale { get; set; }
    public Vector2 Position { get; set; } = new Vector2(0, 0);

    private bool keydown = false;
    private Keys pressedKey = Keys.None;
    private TextInputAssets assets;


    public TextInput(
            string title,
            Vector2 position,
            TextInputAssets assets,
            float fontScale = 1.0f,
            Color? colormask = null
            )
    {
        this.Title = title;
        if (colormask == null) this.Colormask = Color.White;
        else this.Colormask = (Color)colormask;
        this.FontScale = fontScale;
        this.assets = assets;
        this.Position = position;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        Vector2 textpos = new Vector2(Position.X, Position.Y);
        string text = Title + " : " + Content + "_";
        Vector2 textorigin = assets.Font.MeasureString(text) / 2;

        spriteBatch.DrawString(assets.Font, text,
                textpos, Colormask, 0, textorigin, FontScale, SpriteEffects.None, 0.5f);
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
                this.Content += (char)key;
            }
            else if (key == Keys.Back && (key != pressedKey || !keydown))
            {
                if (this.Content.Length > 0) this.Content = this.Content.Remove(this.Content.Length - 1);
            }
            keydown = true;
            pressedKey = key;
        }
    }
}

public class TextInputAssets
{
    public SpriteFont Font { get; set; }
}
