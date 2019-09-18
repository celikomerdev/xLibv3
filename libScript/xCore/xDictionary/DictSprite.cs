#if xLibv3
using System;
using UnityEngine;

namespace xLib.xDictionary
{
	[Serializable]public class DictSprite : DictKV<string,Sprite,StringSprite>{}
	[Serializable]public class StringSprite : ObjectKV<string,Sprite>{}
}
#endif