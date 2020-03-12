#if xLibv3
using System;
using UnityEngine;
using xLib.EventClass;
using xLib.xValueClass;

namespace xLib
{
	public class MnSnapshot : SingletonM<MnSnapshot>
	{
		[SerializeField]private NodeLong playTime = null;
		[SerializeField]private NodeGroup saveGroup = null;
		[SerializeField]private EventUnity onLoadComplete = new EventUnity();
		
		public static Action<long> onLoadPlayTime = delegate{};
		public long PlayTime
		{
			get
			{
				return playTime.Value;
			}
			set
			{
				onLoadPlayTime(value);
				playTime.Value = value;
			}
		}
		
		public static Action<byte[]> onLoadBytes = delegate{};
		public byte[] SnapshotByte
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
				onLoadBytes(value);
				char[] chars = new char[value.Length / sizeof(char)];
				Buffer.BlockCopy(value, 0, chars, 0, value.Length);
				SnapshotString = new string(chars);
			}
		}
		
		public static Action<string> onLoadJson = delegate{};
		public string SnapshotString
		{
			get
			{
				return saveGroup.SerializedObject.ToJsonString();
			}
			set
			{
				if(string.IsNullOrWhiteSpace(value)) return;
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
					onLoadJson(value);
					saveGroup.ValueDefaultReset();
					saveGroup.SerializedObject = value;
					onLoadComplete.Invoke();
				}
				StPopupWindow.Show();
			}
		}
	}
}
#endif