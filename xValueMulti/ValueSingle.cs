#if xLibv3
namespace xLib
{
	public class ValueSingle<T0> : ValueBase<T0>
	{
		private T0 valueSingle;
		public override T0 Value
		{
			get
			{
				return valueSingle;
			}
			set
			{
				valueSingle = value;
			}
		}
	}
}
#endif