using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

abstract class Context
{
    protected SpriteBatch _spriteBatch;

    protected Context(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
    }

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime);
    public abstract void LoadContent();
}

