#if xLibv3
using System;
using UnityEngine;

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
	
	[Serializable]public class DictTransform : DictKV<string,Transform,StringTransform>{}
	[Serializable]public class StringTransform : ObjectKV<string,Transform>{}
}
#endif