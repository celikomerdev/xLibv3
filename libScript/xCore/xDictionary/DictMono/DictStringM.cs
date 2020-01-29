#if xLibv3
namespace xLib.xDictionary
{
	public class DictStringM : BaseInitM
	{
		public DictString dict = new DictString();
		protected override void OnInit(bool init)
		{
			if(init) dict.Init();
		}
	}
}
#endif