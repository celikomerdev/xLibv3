#if xLibv2
using System.Collections;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnRemote : SingletonM<MnRemote>
	{
		#region Mono
		protected override void Started()
		{
			MnCoroutine.ins.WaitForSecondsRealtime(3,Init);
		}
		
		
		protected override void OnDestroyed()
		{
			RemoteSettings.Updated -= Updated;
		}
		#endregion
		
		
		#region Init
		public override void Init()
		{
			base.Init();
			RemoteSettings.Updated += Updated;
			RemoteSettings.ForceUpdate();
		}
		#endregion
		
		
		#region Public
		public void ForceUpdate()
		{
			RemoteSettings.ForceUpdate();
		}
		#endregion
		
		
		#region Updated
		private void Updated()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Updated");
			UpdateBool();
			UpdateInt();
			UpdateFloat();
			UpdateString();
		}
		
		public NodeBool[] nodeBool;
		private void UpdateBool()
		{
			for (int i = 0; i < nodeBool.Length; i++)
			{
				nodeBool[i].Value = RemoteSettings.GetBool(nodeBool[i].Key,nodeBool[i].Value);
			}
		}
		
		public NodeInt[] nodeInt;
		private void UpdateInt()
		{
			for (int i = 0; i < nodeInt.Length; i++)
			{
				nodeInt[i].Value = RemoteSettings.GetInt(nodeInt[i].Key,nodeInt[i].Value);
			}
		}
		
		public NodeFloat[] nodeFloat;
		private void UpdateFloat()
		{
			for (int i = 0; i < nodeFloat.Length; i++)
			{
				nodeFloat[i].Value = RemoteSettings.GetFloat(nodeFloat[i].Key,nodeFloat[i].Value);
			}
		}
		
		public NodeString[] nodeString;
		private void UpdateString()
		{
			for (int i = 0; i < nodeString.Length; i++)
			{
				nodeString[i].Value = RemoteSettings.GetString(nodeString[i].Key,nodeString[i].Value);
			}
		}
		#endregion
	}
}
#endif