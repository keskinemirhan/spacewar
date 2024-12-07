using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace spacewar;

public interface IGameObject {
    public void Update(GameTime gameTime);
    public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
}
