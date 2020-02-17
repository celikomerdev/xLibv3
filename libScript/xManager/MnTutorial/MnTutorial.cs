#if xLibv3
using xLib.xValueClass;

namespace xLib
{
	public class MnTutorial : SingletonM<MnTutorial>
	{
		public NodeString nodeMessageString = null;
		public NodeTransform tutorialTransformPoint = null;
		public NodeVoid onTutorialClick = null;
	}
}
#endif