#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnSocial : SingletonM<MnSocial>
	{
		#region Init
		private bool isInit;
		public EventUnity eventInit;
		public override void Init()
		{
			if(isInit) return;
			isInit = true;
			base.Init();
			eventInit.Invoke();
		}
		#endregion
		
		
		#region LoginOut
		public NodeBool inLogin;
		public NodeBool isLogin;
		public NodeBool isSilent;
		
		public NodeString userID;
		public NodeString personName;
		public NodeTexture personPicture;
		
		
		public void Login()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Login");
			if(isLogin.Value) return;
			if(inLogin.Value) return;
			inLogin.Value = true;
			Social.localUser.Authenticate(OnLogin);
		}
		
		public void LoginSilent()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LoginSilent");
			if(isSilent.Value) Login();
		}
		
		private void OnLogin(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLogin:{0}",value);
			
			if(value)
			{
				userID.Value = Social.localUser.id;
				personName.Value = Social.localUser.userName;
				personPicture.Value = Social.localUser.image;
			}
			else
			{
				userID.Value = userID.ValueDefault;
				personName.Value = personName.ValueDefault;
				personPicture.Value = personPicture.ValueDefault;
			}
			
			isLogin.Value = value;
			isSilent.Value = value;
			inLogin.Value = false;
		}
		#endregion
		
		
		#region Leaderboard
		public EventString eventShowLeaderboard;
		public void ShowLeaderBoardUI(string key)
		{
			if(!isLogin.Value) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":ShowLeaderBoardUI:{0}",key);
			
			if(string.IsNullOrEmpty(key))
			{
				Social.ShowLeaderboardUI();
				return;
			}
			
			string idPlatform = MnKey.GetValue(key);
			eventShowLeaderboard.Invoke(idPlatform);
		}
		
		public void ReportScore(string key,long value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ReportScore:{0}:{1}",key,value);
			if(!isLogin.Value) return;
			if(Application.isEditor) return;
			Social.ReportScore(value,MnKey.GetValue(key),OnReportScore);
		}
		
		private void OnReportScore(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnReportScore:{0}",value);
		}
		#endregion
		
		
		#region Achievement
		public EventString eventShowAchievement;
		public void ShowAchievementUI(string key)
		{
			if(!isLogin.Value) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":ShowAchievementUI:{0}",key);
			
			if(string.IsNullOrEmpty(key))
			{
				Social.ShowAchievementsUI();
				return;
			}
			
			string idPlatform = MnKey.GetValue(key);
			eventShowAchievement.Invoke(idPlatform);
		}
		
		public void ReportProgress(string key,float value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ReportProgress:{0}:{1}",key,value);
			if(!isLogin.Value) return;
			if(Application.isEditor) return;
			Social.ReportProgress(MnKey.GetValue(key),value,OnReportProgress);
		}
		
		private void OnReportProgress(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnReportProgress:{0}",value);
		}
		#endregion
	}
}
#endif