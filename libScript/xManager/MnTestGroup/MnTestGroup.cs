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
			if(CanDebug) Debug.Log($"{this.name}:Work",this);
			WorkInt();
		}
		
		[SerializeField]private NodeInt[] nodeInt = new NodeInt[0];
		private void WorkInt()
		{
			for (int i = 0; i < nodeInt.Length; i++)
			{
				nodeInt[i].Value = GetGroup(nodeInt[i].Key);
			}
		}
		
		public static int GetGroup(string key)
		{
			#if AnalyticsGuruTemp
			int valueTemp = Gameguru.Analytics.Analytics.GetGroupForABTest(key);
			if(valueTemp>0) return valueTemp;
			#endif
			
			return 0;
		}
		#endregion
	}
}
#endif