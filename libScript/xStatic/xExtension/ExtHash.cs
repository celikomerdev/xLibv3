#if xLibv3
using System.Security.Cryptography;
using System.Text;

namespace xLib
{
	public static class ExtHash
	{
		#region ToStringx2
		public static string ToStringx2(this byte[] input)
		{
			var stringBuilder = new StringBuilder();
			foreach (byte element in input)
			{
				stringBuilder.Append(element.ToString("x2"));
			}
			return stringBuilder.ToString();
		}
		#endregion
		
		
		#region SHA256
		public static string HashSHA256(this byte[] input)
		{
			byte[] bytes = SHA256.Create().ComputeHash(input);
			return bytes.ToStringx2();
		}
		
		public static string HashSHA256UTF8(this string value)
		{
			return Encoding.UTF8.GetBytes(value).HashSHA256();
		}
		
		public static string HashSHA256ASCII(this string value)
		{
			return Encoding.ASCII.GetBytes(value).HashSHA256();
		}
		#endregion
		
		
		#region MD5
		public static string HashMD5(this byte[] input)
		{
			byte[] bytes = MD5.Create().ComputeHash(input);
			return bytes.ToStringx2();
		}
		
		public static string HashMD5UTF8(this string value)
		{
			return Encoding.UTF8.GetBytes(value).HashMD5();
		}
		
		public static string HashMD5ASCII(this string value)
		{
			return Encoding.ASCII.GetBytes(value).HashMD5();
		}
		#endregion
	}
}
#endif