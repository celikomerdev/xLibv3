#if xLibv2
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnPlayer : SingletonM<MnPlayer>
	{
		[SerializeField]private NodeBool nodeIsMy;
		
		#region Mono
		protected override void Awaked()
		{
			canDebug = CanDebug;
			
			CurrentId = string.Empty;
			isMy = true;
			nodeIsMy.Value = true;
		}
		
		protected override void OnDestroyed()
		{
			CurrentId = string.Empty;
			isMy = false;
			nodeIsMy.Value = false;
			
			canDebug = false;
		}
		#endregion
	}
}
#endif