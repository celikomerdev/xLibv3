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
		
		public NodeInt[] nodeInt;
		private void WorkInt()
		{
			for (int i = 0; i < nodeInt.Length; i++)
			{
				#if GuruAnalytics
				nodeInt[i].Value = Gameguru.Analytics.Analytics.GetGroupForABTest(nodeInt[i].Key);
				#endif
			}
		}
		#endregion
	}
}
#endif