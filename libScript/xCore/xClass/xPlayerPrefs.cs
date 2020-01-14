#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class xPlayerPrefs
	{
		public static void SaveAll()
		{
			Debug.Log($"xPlayerPrefs:SaveAll");
			PlayerPrefs.Save();
		}
		
		public static void DeleteAll()
		{
			Debug.Log($"xPlayerPrefs:DeleteAll");
			PlayerPrefs.DeleteAll();
			SaveAll();
		}
	}
}
#endif