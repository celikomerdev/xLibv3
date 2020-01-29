#if xLibv3
namespace xLib.xDictionary
{
	public class DictTransformM : BaseInitM
	{
		public DictTransform dict = new DictTransform();
		protected override void OnInit(bool init)
		{
			if(init) dict.Init();
		}
	}
}
#endif