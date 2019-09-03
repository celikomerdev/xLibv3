#if xLibv3
using System;

namespace xLib
{
	public class MnSnapshot : SingletonM<MnSnapshot>
	{
		public static SnapshotBase snapshot = new SnapshotBase();
		
		public static long PlayTime
		{
			get
			{
				return snapshot.PlayTime;
			}
		}
		
		public static byte[] SnapshotByte
		{
			get
			{
				string tempString = SnapshotString;
				byte[] bytes = new byte[tempString.Length*sizeof(char)];
				Buffer.BlockCopy(tempString.ToCharArray(), 0, bytes, 0, bytes.Length);
				return bytes;
			}
			set
			{
				char[] chars = new char[value.Length / sizeof(char)];
				Buffer.BlockCopy(value, 0, chars, 0, value.Length);
				SnapshotString = new string(chars);
			}
		}
		
		public static string SnapshotString
		{
			get
			{
				return snapshot.JsonString;
			}
			set
			{
				if(string.IsNullOrWhiteSpace(value)) return;
				StPopupWindow.Reset();
				StPopupWindow.Header(MnLocalize.GetValue("warning").ToTitleExt());
				StPopupWindow.Body(MnLocalize.GetValue("everything will be deleted").ToUpperExt());
				StPopupWindow.Accept(MnLocalize.GetValue("reset").ToUpperExt());
				StPopupWindow.Decline(MnLocalize.GetValue("cancel").ToUpperExt());
				StPopupWindow.Listener(Listener,true);
				void Listener(bool result)
				{
					StPopupWindow.Listener(Listener,false);
					if(!result) return;
					snapshot.JsonString = value;
				}
				StPopupWindow.Show();
			}
		}
	}
	
	public class SnapshotBase
	{
		public virtual long PlayTime
		{
			get
			{
				return SafeTime.UtcNow.Ticks;
			}
		}
		
		public virtual string JsonString
		{
			get
			{
				return "";
			}
			set{}
		}
	}
}
#endif