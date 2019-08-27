#if xLibv3
using System;

namespace xLib
{
	public static class ExtObject
	{
		public static int xHashCode(this object value)
		{
			if(value==null) return 0;
			return value.GetHashCode();
		}
		
		public static bool IsNumeric(this object value)
		{
			switch (Type.GetTypeCode(value.GetType()))
			{
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
				case TypeCode.DateTime:
					return true;
				default:
					return false;
			}
		}
	}
}
#endif