#if xLibv2
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xNew
{
	//TODO move to classes
	public class MnNew : SingletonM<MnNew>
	{
		[Header("NEW")]
		public NodeFloat smooth;
		public NodeFloat prediction;
		public static float sendInterval = 0.05f;
		
		public NodeFloat lag;
		public NodeFloat delta;
		public NodeFloat deltaSent;
		
		
		//TODO try adding Nodes on the same list to use in for loop
		public NodeString roomMap;
		public NodeInt roomTier;
		public NodeInt maxPlayers;
	}
}
#endif