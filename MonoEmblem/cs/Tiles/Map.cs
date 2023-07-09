using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmblem.Content;
using MonoEmblem.Data;
using MonoEmblem.Entities;
using MonoEmblem.Entities.Data;
using MonoEmblem.Graphics;
using MonoEmblem.Items;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MonoEmblem.Tiles;
public class Map : IEnumerable<MapTile>, IDeserializable<Map>, Graphics.IDrawable
{
	private readonly MapTile[] _tiles;
	private readonly List<Unit> _units = new List<Unit>();

	private Point _topLeftPos = Point.Zero;
	private Point _tileDimensions;

	public Tileset Tileset { get; }
	public Point MapDimensions { get; }
	public Point TopLeftPos
	{
		get => _topLeftPos;
		set
		{
			_topLeftPos = value;
			UpdateSpriteScale();
		}
	}
	public Point TileDimensions
	{
		get => _tileDimensions;
		set
		{
			_tileDimensions = value;
			UpdateSpriteScale();
		}
	}
	public Vector2 ScaleFactor => TileDimensions.ToVector2() / Tileset.TileResolution.ToVector2();

	public MapTile this[int x, int y] => _tiles[(y * Width) + x];
	public MapTile this[Point pos] => GetTile(pos);
	public int Width => MapDimensions.X;
	public int Height => MapDimensions.Y;
	public Rectangle GridBounds => new Rectangle(0, 0, Width, Height);
	public Rectangle Bounds => new Rectangle(TopLeftPos, new Point(Width * TileDimensions.X, Height * TileDimensions.X));

	public MapTile? SelectedTile { get; private set; } = null;

	public IReadOnlyList<Unit> Units => _units;

	public Map(MapData data, Tileset tileset, Texture2D highlightSprite)
	{
		Tileset = tileset;
		MapDimensions = new Point(data.Map[0].Length, data.Map.Length);
		_tileDimensions = Tileset.TileResolution;

		_tiles = new MapTile[Width * Height];
		for (int i = 0; i < _tiles.Length; i++)
		{
			int x = i % Width,
				y = i / Width;
			string id = data.Key[data.Map[y][x].ToString()];

			_tiles[i] = new MapTile(id, data.TileData[id], new Sprite(Tileset.TextureMap, Tileset[id].Rectangle)
			{
				Position = new Point(x * TileDimensions.X, y * TileDimensions.Y),
				Scale = ScaleFactor
			}, highlightSprite);
		}
	}
	private Map(MapData data, ResourceManager resources) :
		this(data, resources.LoadDeserializable<Tileset>(data.TilesetId), resources.LoadAsset<Texture2D>("textures/overlay_tiles/highlight"))
	{ }
	public static Map FromJson(string id, ResourceManager resources)
	{
		var map = new Map(resources.LoadJsonData<MapData>(id), resources);
		resources.RegisterDeserializable(id, map);
		return map;
	}

	public void Interact(Point pos)	
	{
		if (!GridBounds.Contains(pos))
			return;

		var tile = GetTile(pos);
		if (SelectedTile is null && tile.OccupyingTeam == Team.Player)
		{
			SelectedTile = tile;
			Program.GameInstance.Logger.Debug($"Selected tile at ({pos})");
		}

		ClearAllHighlights();
		foreach (var tile in GetPointsInRangeOf(pos, new WeaponRange(1, 2)))
	}
	public void Deselect()
	{
		SelectedTile = null;
		ClearAllHighlights();
	}

	private IReadOnlyDictionary<MapTile, RangeType> CalculateMovementArea(Point tilePos)
	{
		var visited = new Dictionary<MapTile, int>();
		var attackRange = new HashSet<MapTile>();

		// CalculateMovementArea(GetTile(tilePos), )

		return null!;

	}
	private void CalculateMovementArea(MapTile tile, int remainingMove, Dictionary<MapTile, int> visited, HashSet<MapTile> attackRange)
	{
		if (remainingMove < 0)
			return;

		if (tile.OccupyingTeam == Team.None)
		{
			visited[tile] = remainingMove;
			// foreach 
		}
	}

	private List<Point> GetPointsInRangeOf(Point pos, WeaponRange range)
	{
		List<Point> points = new List<Point>(); // TODO: initial capacity -morgan 2023-07-07
		foreach (int distance in range)
		{
			if (distance == 0)
				continue;

			for (int a = 0; a <= distance / 2; a++)
			{
				points.AddRange(
					AllPointsPlusOrMinus(a, distance - a)
					.Select(it => it + pos)
					.Where(it => GridBounds.Contains(it))
				);
			}
		}

		return points;
	}

	private void ClearAllHighlights()
	{
		foreach (var tile in _tiles)
			tile.ClearHighlight();
	}
	public bool AddUnit(Unit unit)
	{
		if (_units.Exists(u => u.Position == unit.Position))
			return false;

		unit.Sprite.SetDimensions(TileDimensions);
		_units.Add(unit);
		GetTile(unit.Position).OccupyingUnit = unit;
		return true;
	}

	public Point WorldToGridPos(Point worldPos) =>
		!Bounds.Contains(worldPos) ? new Point(-1, -1) : (worldPos - TopLeftPos) / TileDimensions;
	public Point GridToWorldPos(Point gridPos) =>
		!GridBounds.Contains(gridPos) ? new Point(0, 0) : gridPos * TileDimensions + TopLeftPos;

	public MapTile GetTile(Point pos) => _tiles[(pos.Y * Width) + pos.X];

	public void UpdateSpriteScale()
	{// ungly -morgan 2023-07-01
		for (int i = 0; i < Width; i++)
		{
			for (int j = 0; j < Height; j++)
			{
				foreach (var sprite in this[i, j].Sprites)
				{
					sprite.Position = TopLeftPos + new Point(i * TileDimensions.X, j * TileDimensions.Y);
					sprite.Scale = ScaleFactor;
				}
			}
		}
	}
	public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		foreach (var tile in this)
			tile.Draw(gameTime, spriteBatch);
	}

	private IEnumerable<Point> AllPointsPlusOrMinus(int a, int b)
	{// TODO: this is kinda ugly. probably find a nicer way to do this -morgan 2023-07-07
		if (a == 0 && b == 0)
		{
			yield return Point.Zero;
			yield break;
		}

		if (a == 0)
		{
			yield return new Point(b, 0);
			yield return new Point(0, b);
			yield return new Point(-b, 0);
			yield return new Point(0, -b);
			yield break;
		}

		if (b == 0)
		{
			yield return new Point(a, 0);
			yield return new Point(0, a);
			yield return new Point(-a, 0);
			yield return new Point(0, -a);
			yield break;
		}

		yield return new Point(a, b);
		yield return new Point(b, a);
		yield return new Point(a, -b);
		yield return new Point(b, -a);
		yield return new Point(-a, b);
		yield return new Point(-b, a);
		yield return new Point(-a, -b);
		yield return new Point(-b, -a);
	}

	public IEnumerator<MapTile> GetEnumerator() => ((IEnumerable<MapTile>)_tiles).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}