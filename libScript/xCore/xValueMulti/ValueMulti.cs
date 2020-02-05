#if xLibv3
using System.Collections.Generic;

namespace xLib
{
	public class ValueMulti<T0> : ValueBase<T0>
	{
		private Dictionary<string,T0> valueMulti = new Dictionary<string,T0>();
		
		public override T0 ValueGet(string viewId)
		{
			CreateId(viewId);
			return valueMulti[viewId];
		}
		
		public override void ValueSet(T0 value,string viewId)
		{
			CreateId(viewId);
			valueMulti[viewId] = value;
		}
		
		private void CreateId(string viewId)
		{
			if(valueMulti.ContainsKey(viewId)) return;
			valueMulti.Add(viewId,valueDefault);
		}
	}
}
#endif