using Microsoft.Xna.Framework;

namespace MonoEmblem.Data;
public class TilesetData
{
	public string TextureId { get; }
	public Point TileResolution { get; }
	public string[] TileIds { get; }

	public TilesetData(string textureId, Point tileResolution, params string[] tileIds)
	{
		TextureId = textureId;
		TileResolution = tileResolution;

		TileIds = new string[tileIds.Length];
		tileIds.CopyTo(TileIds, 0);
	}
}