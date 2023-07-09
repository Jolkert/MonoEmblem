using Microsoft.Xna.Framework;
using System;

namespace MonoEmblem.Extension;
public static class MathExtensions
{
	public static Point CoerceIn(this Point self, Rectangle bounds)
	{
		if (bounds.Contains(self))
			return self;
		else
			return new Point(Math.Clamp(self.X, bounds.Left, bounds.Right - 1), Math.Clamp(self.Y, bounds.Top, bounds.Bottom - 1));
	}
}
