using System.Collections.Generic;

namespace MonoEmblem.Data;
public class MapData
{
	public string TilesetId { get; }
	public IReadOnlyDictionary<string, TileData> TileData { get; }
	public string[] Map { get; }
	public IReadOnlyDictionary<string, string> Key { get; }

	public MapData(
		string tilesetId,
		IReadOnlyDictionary<string, TileData> tileData,
		string[] map,
		IReadOnlyDictionary<string, string> key
	)
	{
		TilesetId = tilesetId;
		TileData = tileData;
		Map = map;
		Key = key;
	}
}