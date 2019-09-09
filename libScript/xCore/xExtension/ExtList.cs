#if xLibv3
using System.Collections.Generic;

namespace xLib
{
	public static class ExtList
	{
		/// <summary>
		/// Set object even if null;
		/// </summary>
		public static List<T> Set<T>(this List<T> target,T obj,int index)
		{
			if(target.Count<(index+1))
			{
				T _temp = default(T);
				
				while(target.Count<(index+1))
				{
					target.Add(_temp);
				}
			}
			
			target[index] = obj;
			return target;
		}
		
		/// <summary>
		/// Get object even if null;
		/// </summary>
		public static T Get<T>(this List<T> source,int index,T defaultValue=default(T))
		{
			if(source==null) return defaultValue;
			if(source.Count>index) return source[index];
			return defaultValue;
		}
	}
}
#endif