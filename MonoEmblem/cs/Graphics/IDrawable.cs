using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEmblem.Graphics;
public interface IDrawable
{
	void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}