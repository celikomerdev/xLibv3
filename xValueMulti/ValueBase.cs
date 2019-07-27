#if xLibv3
namespace xLib
{
	public abstract class ValueBase<T0>
	{
		public T0 valueDefault;
		
		public abstract T0 Value
		{
			get;
			set;
		}
	}
}
#endif