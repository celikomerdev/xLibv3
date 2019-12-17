#if xLibv3
namespace xLib
{
	public interface ISerializableObject:IKey,IName
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