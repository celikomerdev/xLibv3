#if xLibv3
using xLib.xDictionary;

namespace xLib
{
	public class MnAtlas : SingletonM<MnAtlas>
	{
		public DictString dictString;
		public DictTexture dictTexture;
		public DictSprite dictSprite;
		
		#region Mono
		protected override void Awaked()
		{
			dictString.Init();
			dictSprite.Init();
			dictTexture.Init();
		}
		#endregion
	}
}
#endif