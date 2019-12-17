#if xLibv3
namespace xLib
{
	public abstract class ValueBase<T0>
	{
		public T0 valueDefault;
		public abstract T0 ValueGet(string viewId);
		public abstract void ValueSet(T0 value,string viewId);
	}
}
#endif