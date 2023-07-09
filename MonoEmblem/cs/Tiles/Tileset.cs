using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmblem.Content;
using MonoEmblem.Data;
using System.Collections.Generic;

namespace MonoEmblem.Tiles;
public class Tileset : IDeserializable<Tileset>
{
	private static readonly Point UnitX = new Point(1, 0);
	private static readonly Point UnitY = new Point(0, 1);

	private readonly IReadOnlyDictionary<string, Tile> _tiles;
	public Texture2D TextureMap { get; }
	public Point TileResolution { get; }

	public Tileset(TilesetData data, Texture2D textureMap)
	{
		TextureMap = textureMap;
		TileResolution = data.TileResolution;

		var tiles = new Dictionary<string, Tile>(data.TileIds.Length);
		var tileTopLeft = new Point(0, 0);
		for (int i = 0; i < data.TileIds.Length; i++)
		{
			tiles.TryAdd(data.TileIds[i], new Tile(new Rectangle(tileTopLeft, data.TileResolution), this));

			if (tileTopLeft.X + data.TileResolution.X >= TextureMap.Width)
				tileTopLeft = (tileTopLeft + data.TileResolution) * UnitY;
			else
				tileTopLeft += data.TileResolution * UnitX;
		}
		_tiles = tiles;
	}
	private Tileset(TilesetData data, ResourceManager resources) :
		this(data, resources.LoadAsset<Texture2D>($"textures/tilesets/{data.TextureId}"))
	{ }

	public Tile this[string key] => _tiles[key];

	public record class Tile(Rectangle Rectangle, Tileset Tileset) { }

	public static Tileset FromJson(string id, ResourceManager resources)
	{
		var tileset = new Tileset(resources.LoadJsonData<TilesetData>(id), resources);
		resources.RegisterDeserializable(id, tileset);
		return tileset;
	}
}