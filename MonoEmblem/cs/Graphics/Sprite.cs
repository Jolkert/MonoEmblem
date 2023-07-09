using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoEmblem.Graphics;
public class Sprite : IDrawable
{
	public BoundedTexture Texture { get; init; }
	public bool IsVisible { get; set; } = true;
	public Point Position { get; set; } = new Point(0, 0);
	public Vector2 Scale { get; set; } = new Vector2(1f, 1f);
	public Color Tint { get; set; } = Color.White;
	public float Rotation { get; set; } = 0f;
	public SpriteEffects Effects { get; set; } = SpriteEffects.None;

	public int Width => (int)(Scale.X * Texture.Width);
	public int Height => (int)(Scale.Y * Texture.Height);
	public Rectangle PositionRectangle => new Rectangle(Position.X, Position.Y, Width, Height);

	public Sprite(BoundedTexture texture)
	{
		Texture = texture;
	}
	public Sprite(Texture2D texture, Rectangle? sourceRectangle) :
		this(new BoundedTexture(texture, sourceRectangle))
	{ }

	public Sprite(Sprite other)
	{
		Texture = other.Texture;
		IsVisible = other.IsVisible;
		Position = other.Position;
		Scale = other.Scale;
		Tint = other.Tint;
		Rotation = other.Rotation;
		Effects = other.Effects;
	}

	public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		if (IsVisible)
			spriteBatch.Draw(Texture.Texture, PositionRectangle, Texture.Rectangle, Tint, Rotation, Vector2.Zero, Effects, 0);
	}

	public void SetDimensions(int width, int height) =>
		Scale = new Vector2(width, height) / Texture.Bounds.Size.ToVector2();
	public void SetDimensions(Point size) => SetDimensions(size.X, size.Y);
}
