#if xLibv3
namespace xLib
{
	public class ValueSingle<T0> : ValueBase<T0>
	{
		private T0 valueSingle;
		
		public override T0 ValueGet(string viewId)
		{
			return valueSingle;
		}
		
		public override void ValueSet(T0 value,string viewId)
		{
			valueSingle = value;
		}
	}
}
#endif