using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spacewar;

abstract class Context
{
    GraphicsDeviceManager device;
    protected Context(GraphicsDeviceManager device)
    {
        this.device = device;
        
    }
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime);
    public abstract void LoadContent(ContentManager content);
}

