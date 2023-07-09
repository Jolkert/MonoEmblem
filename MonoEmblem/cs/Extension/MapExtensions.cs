using Microsoft.Xna.Framework;
using MonoEmblem.Graphics;
using MonoEmblem.Tiles;

namespace MonoEmblem.Extension;
public static class MapExtensions
{
	public static void CenterInScreen(this Map map, Point screenDimensions)
	{
		Point totalDimensions = map.MapDimensions * map.TileDimensions;
		map.TopLeftPos = ((screenDimensions - totalDimensions).ToVector2() / 2).ToPoint();
	}

	public static Point ScreenToGridPos(this Map map, Point screenPos, Camera camera) =>
		map.WorldToGridPos(camera.ScreenToWorldPos(screenPos));
}
