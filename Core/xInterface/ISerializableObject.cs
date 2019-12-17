#if xLibv3
namespace xLib
{
	public interface ISerializableObject:IInit,IKey,IName
	{
		object SerializedObjectRaw
		{
			get;
			set;
		}
		
		object SerializedObjectName
		{
			get;
		}
	}
}
#endif