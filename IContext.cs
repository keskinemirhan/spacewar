using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spacewar;

public interface IContext
{
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    public abstract static void LoadContent(ContentManager content, GraphicsDeviceManager device);
}

