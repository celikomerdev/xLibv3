#if xLibv3
using System.IO;
using UnityEngine;

namespace xLib
{
	public static class xPersistentData
	{
		#region Path
		private static string PathKey(string key)
		{
			return xApp.xPath(key+".save");
		}
		#endregion
		
		
		#region HasKey
		public static bool HasKey(string key)
		{
			if(File.Exists(PathKey(key))) return true;
			return false;
		}
		#endregion
		
		
		#region Delete
		public static void DeleteKey(string key)
		{
			Debug.Log($"xPersistentData:DeleteKey:{key}");
			File.Delete(PathKey(key));
		}
		
		public static void DeleteAll()
		{
			Debug.Log("xPersistentData:DeleteAll");
			DirectoryInfo directoryInfo = new DirectoryInfo(xApp.xPath(""));
			directoryInfo.Delete(true);
		}
		#endregion
		
		
		#region String
		public static void SetString(string key,string data,bool useThread=true)
		{
			MnThread.StartThread(useThread:useThread,call:delegate
			{
				File.WriteAllText(PathKey(key),data);
			});
		}
		
		public static string GetString(string key)
		{
			string data = "";
			if(HasKey(key)) data = File.ReadAllText(PathKey(key));
			return data;
		}
		#endregion
		
		
		#region Bytes
		public static void SetBytes(string key,byte[] data,bool useThread=true)
		{
			MnThread.StartThread(useThread:useThread,call:delegate
			{
				File.WriteAllBytes(PathKey(key),data);
			});
		}
		
		public static byte[] GetBytes(string key)
		{
			byte[] data = new byte[0];
			if(HasKey(key)) data = File.ReadAllBytes(PathKey(key));
			return data;
		}
		#endregion
		
		
		#region Texture
		public static void SetTexture(string file,Texture data,bool useThread=true)
		{
			MnThread.StartThread(useThread:useThread,call:delegate
			{
				Texture2D texture2D = (Texture2D)data;
				byte[] bytes = texture2D.xEncodeToPNG();
				
				File.WriteAllBytes(xApp.xPath(file),bytes);
			});
		}
		
		public static Texture GetTexture(string file,Texture data)
		{
			if(!File.Exists(xApp.xPath(file))) return data;
			Texture2D texture2D = new Texture2D(2,2);
			texture2D.Load(File.ReadAllBytes(xApp.xPath(file)));
			return texture2D;
		}
		#endregion
	}
}
#endif