using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmblem.Graphics;

namespace MonoEmblem.Entities;
public abstract class Entity : Graphics.IDrawable
{
	public Sprite Sprite { get; protected set; }

	public Entity(Sprite sprite)
	{
		Sprite = sprite;
	}
	public Entity(BoundedTexture texture)
	{
		Sprite = new Sprite(texture);
	}

	public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) => Sprite.Draw(gameTime, spriteBatch);
}