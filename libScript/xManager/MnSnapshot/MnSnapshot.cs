#if xLibv3
using System;
using UnityEngine;

namespace xLib
{
	public class MnSnapshot : SingletonM<MnSnapshot>
	{
		public static SnapshotBase m_snapshot = new SnapshotBase();
		public static SnapshotBase snapshot
		{
			get
			{
				return m_snapshot;
			}
			set
			{
				if(ins && ins.CanDebug) Debug.Log("OverrideSnaphot:SnapshotCustom");
				m_snapshot = value;
			}
		}
		
		public static long PlayTime
		{
			get
			{
				long value = snapshot.PlayTime;
				Debug.Log($"PlayTime:Get:{value}");
				return value;
			}
			set
			{
				Debug.Log($"PlayTime:Set:{value}");
				snapshot.PlayTime = value;
			}
		}
		
		public static byte[] SnapshotByte
		{
			get
			{
				string snapshotString = SnapshotString;
				byte[] bytes = new byte[snapshotString.Length*sizeof(char)];
				Buffer.BlockCopy(snapshotString.ToCharArray(), 0, bytes, 0, bytes.Length);
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
				string value = snapshot.JsonString;
				Debug.Log($"SnapshotString:Get:{value}");
				return value;
			}
			set
			{
				if(string.IsNullOrWhiteSpace(value)) return;
				Debug.Log($"SnapshotString:Set:{value}");
				
				StPopupWindow.Reset();
				StPopupWindow.Header(MnLocalize.GetValue("Warning"));
				StPopupWindow.Body(MnLocalize.GetValue("Everything Will Be Deleted"));
				StPopupWindow.Accept(MnLocalize.GetValue("Accept"));
				StPopupWindow.Decline(MnLocalize.GetValue("Decline"));
				StPopupWindow.Listener(true,Listener);
				void Listener(bool result)
				{
					StPopupWindow.Listener(false,Listener);
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
				return SafeTime.NowUtc.Ticks;
			}
			set{}
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