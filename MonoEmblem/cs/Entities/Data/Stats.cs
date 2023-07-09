namespace MonoEmblem.Entities.Data;
public readonly record struct Stats(
		int MaxHp,
		int Str,
		int Mag,
		int Dex,
		int Spd,
		int Def,
		int Res,
		int Lck,
		int Con,
		int Mov
)
{
	public static Stats operator -(Stats stats) => new Stats(
		-stats.MaxHp,
		-stats.Str,
		-stats.Mag,
		-stats.Dex,
		-stats.Spd,
		-stats.Def,
		-stats.Res,
		-stats.Lck,
		-stats.Con,
		-stats.Mov
	);

	public static Stats operator +(Stats left, Stats right) => new Stats(
		left.MaxHp + right.MaxHp,
		left.Str + right.Str,
		left.Mag + right.Mag,
		left.Dex + right.Dex,
		left.Spd + right.Spd,
		left.Def + right.Def,
		left.Res + right.Res,
		left.Lck + right.Lck,
		left.Con + right.Con,
		left.Mov + right.Mov
	);
	public static Stats operator -(Stats left, Stats right) => left + -right;
	public static Stats operator *(Stats left, Stats right) => new Stats(
		left.MaxHp * right.MaxHp,
		left.Str * right.Str,
		left.Mag * right.Mag,
		left.Dex * right.Dex,
		left.Spd * right.Spd,
		left.Def * right.Def,
		left.Res * right.Res,
		left.Lck * right.Lck,
		left.Con * right.Con,
		left.Mov * right.Mov
	);
	public static Stats operator /(Stats left, Stats right) => new Stats(
		left.MaxHp / right.MaxHp,
		left.Str / right.Str,
		left.Mag / right.Mag,
		left.Dex / right.Dex,
		left.Spd / right.Spd,
		left.Def / right.Def,
		left.Res / right.Res,
		left.Lck / right.Lck,
		left.Con / right.Con,
		left.Mov / right.Mov
	);

	public static Stats operator *(Stats left, int right) => new Stats(
		left.MaxHp * right,
		left.Str * right,
		left.Mag * right,
		left.Dex * right,
		left.Spd * right,
		left.Def * right,
		left.Res * right,
		left.Lck * right,
		left.Con * right,
		left.Mov * right
	);
	public static Stats operator /(Stats left, int right) => new Stats(
		left.MaxHp / right,
		left.Str / right,
		left.Mag / right,
		left.Dex / right,
		left.Spd / right,
		left.Def / right,
		left.Res / right,
		left.Lck / right,
		left.Con / right,
		left.Mov / right
	);
}