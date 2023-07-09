using System;
using System.Collections;
using System.Collections.Generic;

namespace MonoEmblem.Items;
public readonly record struct WeaponRange(int Min, int Max) : IEnumerable<int>
{
	public WeaponRange(int range) : this (range, range) { }

	public static implicit operator WeaponRange(int n) => new WeaponRange(n, n);

	public IEnumerator<int> GetEnumerator()
	{
		for (int i = Min; i <= Max; i++)
			yield return i;
	}
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}