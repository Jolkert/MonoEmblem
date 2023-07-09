using Microsoft.Xna.Framework.Graphics;
using MonoEmblem.Graphics;
using MonoEmblem.Tiles;

namespace MonoEmblem.Entities;
public class Cursor : MapBoundEntity
{
	public Cursor(BoundedTexture texture, Map map) : base(texture, map) { }
}