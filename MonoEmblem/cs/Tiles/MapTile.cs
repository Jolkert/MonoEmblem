using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoEmblem.Data;
using MonoEmblem.Entities;
using MonoEmblem.Entities.Data;
using MonoEmblem.Graphics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MonoEmblem.Tiles;
public class MapTile : Graphics.IDrawable
{
	private Color? _highlightColor = null;

	public string TypeId { get; }
	public TileData Data { get; }
	public Sprite Sprite { get; }
	public Sprite HighlightSprite { get; }
	public IEnumerable<Sprite> Sprites
	{
		get
		{
			yield return Sprite;
			yield return HighlightSprite;
		}
	}
	public Color? HighlightColor
	{
		get => _highlightColor;
		set
		{
			_highlightColor = value is Color color ?
				new Color(color.R, color.G, color.B, 0.3f) :
				null;
			HighlightSprite.Tint = _highlightColor ?? Color.White;
			HighlightSprite.IsVisible = IsHighlighted;
		}
	}
	[MemberNotNullWhen(true, nameof(HighlightColor))]
	public bool IsHighlighted => HighlightColor is not null;

	public Unit? OccupyingUnit { get; set; } = null!;
	public Team OccupyingTeam => OccupyingUnit?.Team ?? Team.None;


	public MapTile(string id, TileData data, Sprite sprite, Texture2D highlightSprite)
	{
		TypeId = id;
		Data = data;
		Sprite = sprite;
		HighlightSprite = new Sprite(sprite)
		{
			Texture = highlightSprite,
			IsVisible = IsHighlighted
		};
	}

	public void Highlight(Color color) => HighlightColor = color;
	public void ClearHighlight() => HighlightColor = null;

	public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		Sprite.Draw(gameTime, spriteBatch);
		OccupyingUnit?.Draw(gameTime, spriteBatch);
		HighlightSprite.Draw(gameTime, spriteBatch);
	}

	public override string ToString() => TypeId;
}