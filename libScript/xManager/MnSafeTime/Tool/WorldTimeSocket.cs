#if xLibv2
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace xLib.ToolWorldTime
{
	public class WorldTimeSocket : WorldTimeBase
	{
		[Header("Socket")]
		[SerializeField]private int timeOut = 5000;
		
		#region Refresh
		public override void Refresh()
		{
			base.Refresh();
			MnThread.StartThread(RefreshCore);
		}
		
		private void RefreshCore()
		{
			try
			{
				Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Dgram,ProtocolType.Udp);
				socket.SendTimeout = timeOut;
				socket.ReceiveTimeout = timeOut;
				
				IPAddress[] addresses = Dns.GetHostEntry(url).AddressList;
				// yield return new WaitForSecondsRealtime(0.2f);
				
				IPEndPoint ipEndPoint = new IPEndPoint(addresses[0],123); //NTP uses UDP The UDP port number assigned to NTP is 123
				socket.Connect(ipEndPoint);
				// yield return new WaitForSecondsRealtime(0.2f);
				
				byte[] ntpData = new byte[48];
				ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)
				socket.Send(ntpData);
				// yield return new WaitForSecondsRealtime(0.2f);
				
				socket.Receive(ntpData);
				// yield return new WaitForSecondsRealtime(0.2f);
				
				socket.Close();
				
				ulong intc = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
				ulong frac = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];
				long milliseconds = (long)((intc*1000) + ((frac*1000) / 0x100000000L));
				DateTime dateTime = dateTimeOrigin.dateTime.AddMilliseconds(milliseconds);
				
				MnThread.Register(delegate
				{
					if(CanDebug) Debug.LogFormat(this,this.name+":{0}",dateTime.ToString());
					MnWorldTime.ins.DateTimeUtc = dateTime;
				});
			}
			catch(Exception ex)
			{
				MnThread.Register(delegate
				{
					xDebug.LogExceptionFormat(this,this.name+":Exception:{0}",ex.ToString());
				});
			}
		}
		#endregion
	}
}
#endif