#if xLibv3
namespace xLib.xDictionary
{
	public class DictSpriteM : BaseInitM
	{
		public DictSprite dict = new DictSprite();
		protected override void OnInit(bool init)
		{
			if(init) dict.Init();
		}
	}
}
#endif