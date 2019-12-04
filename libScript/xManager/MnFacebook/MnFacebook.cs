#if xLibv2
#if Facebook
using System;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using xLib.xNode.NodeObject;
using xLib.xSimpleJSON;
using System.Collections;

namespace xLib
{
	public class MnFacebook : SingletonM<MnFacebook>
	{
		[SerializeField]private string[] permission;
		
		#region Init
		private bool inInit;
		public override void Init()
		{
			base.Init();
			
			isInit.Value = FB.IsInitialized;
			if(isInit.Value)
			{
				Activate();
				return;
			}
			
			if(inInit) return;
			inInit = true;
			FB.Init(OnInit);
		}
		
		[SerializeField]private NodeBool isInit;
		private void OnInit()
		{
			inInit = false;
			
			isInit.Value = FB.IsInitialized;
			if(!isInit.Value) return;
			
			Activate();
		}
		#endregion
		
		#region Activate
		[SerializeField]private NodeBool isActive;
		private void Activate()
		{
			if(isActive.Value) return;
			
			FB.ActivateApp();
			isActive.Value = true;
		}
		#endregion
		
		#region Login
		private bool inLogin;
		public void Login()
		{
			if(!isActive.Value)
			{
				Init();
				return;
			}
			
			if(FB.IsLoggedIn)
			{
				IsLogin(true);
				return;
			}
			
			if(inLogin) return;
			inLogin = true;
			FB.LogInWithReadPermissions(permission,LoginResult);
		}
		
		private void LoginResult(ILoginResult result)
		{
			IsLogin(FB.IsLoggedIn);
		}
		
		[SerializeField]private NodeBool isLogin;
		[SerializeField]private NodeString token;
		private void IsLogin(bool value)
		{
			inLogin = false;
			
			if(value) token.Value = AccessToken.CurrentAccessToken.TokenString;
			else token.Value = "";
			
			isLogin.Value = value;
			isSilent.Value = value;
			
			if(!value) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Permissions:{0}",AccessToken.CurrentAccessToken.Permissions);
			LoadPerson();
		}
		#endregion
		
		#region LoginSilent
		[SerializeField]private NodeBool isSilent;
		public void LoginSilent()
		{
			if(isSilent.Value) Login();
		}
		#endregion
		
		#region Logout
		public void Logout()
		{
			if(!isActive.Value)
			{
				Init();
				return;
			}
			
			if(!isLogin.Value) return;
			FB.LogOut();
			IsLogin(false);
			personName.Value = personName.ValueDefault;
			personPicture.Value = personPicture.ValueDefault;
		}
		#endregion
		
		#region LoadPerson
		private void LoadPerson()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":LoadPerson");
			if(!isLogin.Value) return;
			
			FB.API("/me",HttpMethod.GET,OnLoadPersonInfo);
			FB.API("me/picture?type=normal&height=128&width=128",HttpMethod.GET,OnLoadPersonPicture);
		}
		
		[SerializeField]private NodeString personName;
		private void OnLoadPersonInfo(IResult result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLoadPersonInfo:{0}",result.RawResult);
			
			if(!result.ResultDictionary.ContainsKey("name")) return;
			personName.Value = result.ResultDictionary["name"].ToString();
		}
		
		[SerializeField]private NodeTexture personPicture;
		private void OnLoadPersonPicture(IGraphResult result)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":OnLoadPersonPicture:{0}",result.RawResult);
			
			if(!result.Texture) return;
			personPicture.Value = result.Texture;
		}
		#endregion
	}
}
#else
using System;
using System.Collections.Generic;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnFacebook : SingletonM<MnFacebook>
	{
		#pragma warning disable
		[SerializeField]private string[] permission;
		
		[SerializeField]private NodeBool isInit;
		[SerializeField]private NodeBool isActive;
		
		[SerializeField]private NodeBool isLogin;
		[SerializeField]private NodeString token;
		[SerializeField]private NodeBool isSilent;
		
		[SerializeField]private NodeString personName;
		[SerializeField]private NodeTexture personPicture;
		#pragma warning restore
		
		public void Login(){}
		
		public void Logout(){}
	}
}
#endif
#endif