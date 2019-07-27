#if xLibv3
using System.Collections.Generic;

namespace xLib
{
	public class ValueMulti<T0> : ValueBase<T0>
	{
		private Dictionary<string,T0> valueMulti = new Dictionary<string,T0>();
		public override T0 Value
		{
			get
			{
				CreateId();
				return valueMulti[ViewCore.FinalId];
			}
			set
			{
				CreateId();
				valueMulti[ViewCore.FinalId] = value;
			}
		}
		
		private void CreateId()
		{
			ViewCore.FinalizeId();
			if(valueMulti.ContainsKey(ViewCore.FinalId)) return;
			valueMulti.Add(ViewCore.FinalId,valueDefault);
		}
	}
}
#endif