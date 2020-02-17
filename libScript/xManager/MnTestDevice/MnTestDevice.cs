#if xLibv2
using System.Collections;
using UnityEngine;

namespace xLib
{
	public class MnTestDevice : SingletonM<MnTestDevice>
	{
		public NodeBool isTestDevice;
		
		#region Work
		public void Work()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Work");
			
			bool value = false;
			xSimpleJSON.JSONNode arrayDevice = MnRemote.jsonData["arrayTestDevice"];
			for (int i = 0; i < arrayDevice.Count; i++)
			{
				if (SystemInfo.deviceUniqueIdentifier != arrayDevice[i]) continue;
				value = true;
				break;
			}
			isTestDevice.Value = value;
		}
		#endregion
	}
}
#endif