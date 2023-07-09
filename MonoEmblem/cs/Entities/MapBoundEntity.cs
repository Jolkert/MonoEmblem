using Microsoft.Xna.Framework;
using MonoEmblem.Extension;
using MonoEmblem.Graphics;
using MonoEmblem.Tiles;

namespace MonoEmblem.Entities;
public abstract class MapBoundEntity : Entity
{
	private Point _position = Point.Zero;

	public Map Map { get; }
	public Point Position
	{
		get => _position;
		set
		{
			_position = value.CoerceIn(Map.GridBounds);
			Sprite.Position = Map.GridToWorldPos(_position);
		}
	}

	protected MapBoundEntity(Sprite sprite, Map map) : base(sprite)
	{
		Map = map;
	}
	protected MapBoundEntity(BoundedTexture texture, Map map) : base(texture)
	{
		Map = map;
	}
}
