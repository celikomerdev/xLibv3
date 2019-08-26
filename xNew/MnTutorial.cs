#if xLibv3
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnTutorial : SingletonM<MnTutorial>
	{
		public NodeString nodeMessageString;
		public NodeTransform tutorialTransformPoint;
		public NodeVoid onTutorialClick;
		
		
	}
}
#endif