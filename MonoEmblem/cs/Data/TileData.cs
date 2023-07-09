using System.ComponentModel;

namespace MonoEmblem.Data;
public readonly record struct TileData(
	[DefaultValue(1)]
	int MovementCost = 1,
	[DefaultValue(0)]
	int AvoBonus = 0,
	[DefaultValue(0)]
	int DefBonus = 0,
	[DefaultValue(0)]
	int ResBonus = 0,
	[DefaultValue(0)]
	int HealPercent = 0
)
{ }