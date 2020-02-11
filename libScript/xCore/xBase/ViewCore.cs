#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ViewCore
	{
		#region IsMy
		private static bool isMy = false;
		public static bool IsMy
		{
			get
			{
				return isMy;
			}
		}
		
		private static void CheckIsMy()
		{
			isMy = (myId == currentId);
			if(currentId == string.Empty) isMy = true;
			
			// if(ins) ins.nodeIsMy.Value = isMy;
		}
		#endregion
		
		
		#region CurrentId
		internal static bool canDebug = false;
		private static string myId = string.Empty;
		public static string MyId
		{
			get
			{
				return myId;
			}
			set
			{
				if(myId == value) return;
				if(canDebug) Debug.LogFormat("MyId:{0}:{1}",myId,value);
				myId = value;
				CheckIsMy();
			}
		}
		
		private static string currentId = string.Empty;
		public static string CurrentId
		{
			get
			{
				return currentId;
			}
			set
			{
				if(currentId == value) return;
				currentId = value;
				CheckIsMy();
			}
		}
		#endregion
		
		
		public static bool inRoom = false;
		public static bool inRpc = false;
		public static void RPC(string target,string key,string data){}
	}
}
#endif