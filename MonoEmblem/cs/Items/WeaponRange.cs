using System;
using System.Collections;
using System.Collections.Generic;

namespace MonoEmblem.Items;
public readonly record struct WeaponRange(int Min, int Max) : IEnumerable<int>
{
	public WeaponRange(int range) : this (range, range) { }

	public static implicit operator WeaponRange(int n) => new WeaponRange(n, n);

	public WeaponRange MaxUp(int amount = 1) => new WeaponRange(Min, Max + amount);
	public WeaponRange MaxDown(int amount = 1) => MaxUp(-amount);

	public WeaponRange MinUp(int amount = 1) => new WeaponRange(Min + amount, Max);
	public WeaponRange MinDown(int amount = 1) => MinUp(-amount);

	public IEnumerator<int> GetEnumerator()
	{
		for (int i = Min; i <= Max; i++)
			yield return i;
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}