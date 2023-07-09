using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEmblem.Graphics;
// TODO: get a more sensible name for this -morgan 2023-07-05
public readonly record struct BoundedTexture(Texture2D Texture, Rectangle? Rectangle = null)
{
	public int Width => Rectangle?.Width ?? Texture.Width;
	public int Height => Rectangle?.Height ?? Texture.Height;
	public Rectangle Bounds => Rectangle ?? Texture.Bounds;

	public static implicit operator BoundedTexture(Texture2D texture) => new BoundedTexture(texture, null);
	public BoundedTexture WithRectangle(Rectangle? rectangle) => new BoundedTexture(Texture, rectangle);

}