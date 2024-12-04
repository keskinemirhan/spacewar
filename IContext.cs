using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace spacewar;

interface IContext
{
    public void Update(GameTime gameTime);
    public void Draw(GraphicsDeviceManager device, SpriteBatch spriteBatch, GameTime gameTime);
    public void LoadContent(ContentManager content);
}

