#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public class MnSocial : SingletonM<MnSocial>
	{
		#region Init
		private bool isInit = false;
		public static readonly System.Action actionInit = delegate{};
		protected override void Inited()
		{
			if(isInit) return;
			isInit = true;
			actionInit();
		}
		#endregion
		
		
		#region LoginOut
		public NodeBool inLogin = null;
		public NodeBool isLogin = null;
		public NodeBool isSilent = null;
		
		public static System.Action<bool> actionLoginOut = delegate{};
		public void LoginOut(bool value)
		{
			Init();
			if(CanDebug) Debug.Log($"{this.name}:LoginOut:{value}",this);
			if(isLogin.Value == value) return;
			actionLoginOut(value);
		}
		
		public void LoginSilent()
		{
			if(CanDebug) Debug.Log($"{this.name}:LoginSilent",this);
			if(isSilent.Value) LoginOut(true);
		}
		
		private void IsLogin(bool value)
		{
			if(CanDebug) Debug.Log($"{this.name}:IsLogin:{value}",this);
			isLogin.Value = value;
			isSilent.Value = value;
		}
		#endregion
		
		
		#region Leaderboard
		public static System.Action<string> actionShowLeaderboard = delegate{};
		public void ShowLeaderBoard(string key)
		{
			LoginOut(true);
			if(!isLogin.Value) return;
			if(CanDebug) Debug.Log($"{this.name}:ShowLeaderBoard:{key}",this);
			actionShowLeaderboard(key);
		}
		
		public static System.Action<string,long> actionReportLeaderboard = delegate{};
		public void ReportLeaderboard(string key,long value)
		{
			if(CanDebug) Debug.Log($"{this.name}:ReportLeaderboard:{key}:{value}",this);
			if(!isLogin.Value) return;
			if(Application.isEditor) return;
			actionReportLeaderboard(key,value);
		}
		#endregion
		
		
		#region Achievement
		public static System.Action<string> actionShowAchievement = delegate{};
		public void ShowAchievement(string key)
		{
			LoginOut(true);
			if(!isLogin.Value) return;
			if(CanDebug) Debug.Log($"{this.name}:ShowAchievement:{key}",this);
			actionShowAchievement.Invoke(key);
		}
		
		public static System.Action<string,float> actionReportAchievement = delegate{};
		public void ReportAchievement(string key,float value)
		{
			if(CanDebug) Debug.Log($"{this.name}:ReportAchievement:{key}:{value}",this);
			if(!isLogin.Value) return;
			if(Application.isEditor) return;
			actionReportAchievement(key,value);
		}
		#endregion
	}
}
#endif