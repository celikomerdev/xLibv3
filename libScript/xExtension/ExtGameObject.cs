#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ExtGameObject
	{
		public static void ToggleActive(this GameObject gameObject)
		{
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}
#endif