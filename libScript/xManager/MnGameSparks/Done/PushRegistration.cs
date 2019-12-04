#if xLibv2
#if GameSparks
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine;

namespace xLib.xGameSparks
{
	public class PushRegistration : BaseWorkM
	{
		[SerializeField]private bool isDurable;
		
		#region Call
		public void Call(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call:{0}",value);
			if(string.IsNullOrEmpty(value)) return;
			
			PushRegistrationRequest request = new PushRegistrationRequest();
			request.SetDurable(isDurable);
			
			request.SetDeviceOS(DeviceOS);
			request.SetPushId(value);
			
			request.Send(Callback);
		}
		
		private void Callback(PushRegistrationResponse response)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",response.JSONString);
			
			if(response.HasErrors)
			{
				return;
			}
		}
		#endregion
		
		
		private string DeviceOS
		{
			get
			{
				#if UNITY_ANDROID
				return "android";
				#elif UNITY_IOS
				return "ios";
				#else
				return "test";
				#endif
				//ios, android, fcm, wp8, w8, kindle or viber
			}
		}
	}
}
#endif
#endif