#if xLibv3
using System;
using UnityEngine;

namespace xLib.xDictionary
{
	public class DictSpriteM : BaseInitM
	{
		[UnityEngine.Serialization.FormerlySerializedAs("dictSprite")]
		public DictSprite dict = new DictSprite();
		
		protected override void OnInit(bool init)
		{
			if(init) dict.Init();
		}
	}
	
	[Serializable]public class DictSprite : DictKV<string,Sprite,StringSprite>{}
	[Serializable]public class StringSprite : ObjectKV<string,Sprite>{}
}
#endif