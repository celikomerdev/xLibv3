#if xLibv3
using System;
using UnityEngine;

namespace xLib.xDictionary
{
	//KeyValue
	[Serializable]public class StringBool		: ObjectKV<string,	bool>{}
	[Serializable]public class StringByte		: ObjectKV<string,	byte>{}
	[Serializable]public class StringFloat		: ObjectKV<string,	float>{}
	[Serializable]public class StringInt		: ObjectKV<string,	int>{}
	[Serializable]public class StringString		: ObjectKV<string,	string>{}
		
	[Serializable]public class StringColor		: ObjectKV<string,	Color>{}
	[Serializable]public class StringSprite		: ObjectKV<string,	Sprite>{}
	[Serializable]public class StringTexture	: ObjectKV<string,	Texture>{}
	[Serializable]public class StringTransform	: ObjectKV<string,	Transform>{}
	
	//Dict
	[Serializable]public class DictBool			: DictKV<string,	bool,		StringBool>{}
	[Serializable]public class DictByte			: DictKV<string,	byte,		StringByte>{}
	[Serializable]public class DictFloat		: DictKV<string,	float,		StringFloat>{}
	[Serializable]public class DictInt			: DictKV<string,	int,		StringInt>{}
	[Serializable]public class DictString		: DictKV<string,	string,		StringString>{}
		
	[Serializable]public class DictColor		: DictKV<string,	Color,		StringColor>{}
	[Serializable]public class DictSprite		: DictKV<string,	Sprite,		StringSprite>{}
	[Serializable]public class DictTexture		: DictKV<string,	Texture,	StringTexture>{}
	[Serializable]public class DictTransform	: DictKV<string,	Transform,	StringTransform>{}
}
#endif