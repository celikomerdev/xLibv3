#if xLibv3
using System;
using UnityEngine;

namespace xLib.xDictionary
{
	public class DictSpriteM : BaseInitM
	{
		public DictSprite dictSprite = new DictSprite();
		
		protected override void OnInit(bool init)
		{
			if(init) dictSprite.Init();
		}
	}
	
	[Serializable]public class DictSprite : DictKV<string,Sprite,StringSprite>{}
	[Serializable]public class StringSprite : ObjectKV<string,Sprite>{}
}
#endif