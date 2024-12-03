using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

abstract class Context { 
    private SpriteBatch _spriteBatch;

    public Context(SpriteBatch spriteBatch)
    {
       _spriteBatch = spriteBatch; 
    }

    public abstract void Update(GameTime gameTime); 
    public abstract void Draw(GameTime gameTime); 
    public abstract void LoadContent(); 
}
    
