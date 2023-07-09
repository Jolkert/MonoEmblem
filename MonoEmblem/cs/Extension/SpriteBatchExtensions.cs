using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoEmblem.Extension;
public static class SpriteBatchExtensions
{
	public static void Use(this SpriteBatchExt self, SamplerState samplerState, Action<SpriteBatchExt> action)
	{
		self.Begin(samplerState: samplerState);
		action(self);
		self.End();
	}
	public static void Use(this SpriteBatchExt self, Action<SpriteBatchExt> action) => Use(self, SamplerState.PointClamp, action);
}
