#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnTestGroup : SingletonM<MnTestGroup>
	{
		protected override void Started()
		{
			Work();
		}
		
		#region Work
		private void Work()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Work");
			WorkInt();
		}
		
		[SerializeField]private NodeInt[] nodeInt = new NodeInt[0];
		private void WorkInt()
		{
			for (int i = 0; i < nodeInt.Length; i++)
			{
				#if GuruAnalytics
				nodeInt[i].Value = GetGroup(nodeInt[i].Key);
				#endif
			}
		}
		
		public static int GetGroup(string key)
		{
			#if GuruAnalytics
			return Gameguru.Analytics.Analytics.GetGroupForABTest(key);
			#endif
			return 0;
		}
		#endregion
	}
}
#endif