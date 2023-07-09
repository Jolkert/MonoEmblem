using Microsoft.Xna.Framework;
using MonoEmblem.Entities.Data;
using MonoEmblem.Graphics;
using MonoEmblem.Tiles;

namespace MonoEmblem.Entities;
public class Unit : MapBoundEntity
{
	public Team Team { get; private set; }
	public Stats Stats { get; private set; }

	public Unit(BoundedTexture sprite, Map map, Point startPos, Team team) : base(sprite, map)
	{
		Position = startPos;
		Team = team;
	}

	public Unit(BoundedTexture sprite, Map map, Point startPos, Team team, Stats stats) :
		this(sprite, map, startPos, team)
	{
		Stats = stats;
	}
}