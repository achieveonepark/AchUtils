using UnityEngine;

namespace AchUtils
{

	public static class GameObjectUtils
	{

		public static bool Exists(string name)
		{
			return GameObject.Find(name);
		}
	}
}