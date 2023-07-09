using Microsoft.Xna.Framework;

namespace MonoEmblem.Graphics;
public class Camera
{
	public int Width { get; }
	public int Height { get; }
	public Matrix TransformMatrix { get; private set; } = Matrix.Identity;

	public Camera(int width, int height)
	{
		Width = width;
		Height = height;
	}
	public Camera(Point pos) : this(pos.X, pos.Y) { }

	public void Translate(Vector2 direction)
	{
		Matrix transform = TransformMatrix;
		transform.Translation = TransformMatrix.Translation + new Vector3(direction, 0);
		TransformMatrix = transform;
	}
	public void CenterAt(Point pos)
	{
		Matrix transform = TransformMatrix;
		transform.Translation = Matrix.Identity.Translation + new Vector3(pos.ToVector2(), 0) - new Vector3(Width / 2, Height / 2, 0);
		TransformMatrix = transform;
	}
	public Point ScreenToWorldPos(Point pos) =>
		pos - new Point((int)TransformMatrix.Translation.X, (int)TransformMatrix.Translation.Y);
}