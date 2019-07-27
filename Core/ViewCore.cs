#if xLibv3
using UnityEngine;

namespace xLib
{
	public static class ViewCore
	{
		#region IsMy
		private static bool isMy;
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
			if(currentId == "0") isMy = true;
			
			// if(ins) ins.nodeIsMy.Value = isMy;
		}
		#endregion
		
		
		#region CurrentId
		internal static bool canDebug = false;
		private static string myId = "0";
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
		
		private static string currentId = "0";
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
		
		
		public static bool inRoom;
		public static bool inRpc;
		public static void RPC(string target,string key,string data){}
		
		private static string finalId;
		public static string FinalId
		{
			get
			{
				return finalId;
			}
			private set
			{
				finalId = value;
			}
		}
		
		public static void FinalizeId()
		{
			if(isMy) FinalId = "0";
			else FinalId = CurrentId;
		}
	}
}
#endif