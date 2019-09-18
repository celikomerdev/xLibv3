#if xLibv3
using System;

namespace xLib.xDictionary
{
	[Serializable]public class DictString : DictKV<string,string,StringString>{}
	[Serializable]public class StringString : ObjectKV<string,string>{}
}
#endif