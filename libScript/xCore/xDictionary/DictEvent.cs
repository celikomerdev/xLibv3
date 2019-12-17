// #if xLibv3
// using System;
// using System.Collections.Generic;
// using xLib.xValueClass;

// namespace xLib.xDictionary
// {
// 	[Serializable]
// 	public class StringEvent
// 	{
// 		public StringEvent[] array;
// 		public Dictionary<string,t> dictionary = new Dictionary<string,ValueEvent>();
		
// 		private bool isInit;
// 		public void Init()
// 		{
// 			if(isInit) return;
// 			isInit = true;
			
// 			if(array == null) return;
// 			for(int i = 0; i < array.Length; i++)
// 			{
// 				dictionary[array[i].Key] = array[i];
// 			}
// 		}
// 	}
// }
// #endif