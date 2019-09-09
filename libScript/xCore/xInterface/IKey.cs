#if xLibv3
namespace xLib
{
	public interface IKey
	{
		string Key
		{
			get;
		}
		
		#region Context
		void KeyName();
		void KeyGuid();
		#endregion
	}
}
#endif