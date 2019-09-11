#if xLibv3
using System;
using Newtonsoft.Json.Linq;

namespace xLib
{
	public class MnConfig : SingletonM<MnConfig>
	{
		public static JObject data = null;
		public static Action onUpdateData;
	}
}
#endif