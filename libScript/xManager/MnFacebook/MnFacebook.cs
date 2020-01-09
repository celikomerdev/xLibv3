#if xLibv3
#if Facebook
using UnityEngine;
using Facebook.Unity;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnFacebook : SingletonM<MnFacebook>
	{
		[SerializeField]private string[] permission = new string[0];
		
		#region Init
		private bool inInit = false;
		protected override void Inited()
		{
			isInit.Value = FB.IsInitialized;
			if(isInit.Value)
			{
				Activate();
				return;
			}
			
			if(inInit) return;
			inInit = true;
			MnThread.StartThread(useThread:false,priority:1,context:this,call:delegate{FB.Init(OnInit);});
		}
		
		[SerializeField]private NodeBool isInit = null;
		private void OnInit()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:OnInit",this);
			inInit = false;
			
			isInit.Value = FB.IsInitialized;
			if(!isInit.Value) return;
			
			Activate();
		}
		#endregion
		
		#region Activate
		[SerializeField]private NodeBool isActive = null;
		private void Activate()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:Activate",this);
			if(isActive.Value) return;
			
			FB.ActivateApp();
			isActive.Value = true;
		}
		#endregion
		
		#region Login
		private bool inLogin = false;
		public void Login()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:Login",this);
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
			if(CanDebug) Debug.LogWarning($"{this.name}:LoginResult:{result}",this);
			IsLogin(FB.IsLoggedIn);
		}
		
		[SerializeField]private NodeBool isLogin = null;
		[SerializeField]private NodeString token = null;
		private void IsLogin(bool value)
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:IsLogin:{value}",this);
			inLogin = false;
			
			if(value) token.Value = AccessToken.CurrentAccessToken.TokenString;
			else token.Value = "";
			
			isLogin.Value = value;
			isSilent.Value = value;
			
			if(!value) return;
			if(CanDebug) Debug.Log($"{this.name}:Permissions:{AccessToken.CurrentAccessToken.Permissions.ToString()}",this);
			LoadPerson();
		}
		#endregion
		
		#region LoginSilent
		[SerializeField]private NodeBool isSilent = null;
		public void LoginSilent()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:LoginSilent",this);
			if(isSilent.Value) Login();
		}
		#endregion
		
		#region Logout
		public void Logout()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:Logout",this);
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
			if(CanDebug) Debug.Log($"{this.name}:LoadPerson",this);
			if(!isLogin.Value) return;
			
			
			FB.API("/me",HttpMethod.GET,OnLoadPersonInfo);
			FB.API($"me/picture?type=normal&width={personPicture.ValueDefault.width}&height={personPicture.ValueDefault.height}",HttpMethod.GET,OnLoadPersonPicture);
		}
		
		[SerializeField]private NodeString personName = null;
		private void OnLoadPersonInfo(IResult result)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnLoadPersonInfo:{result.RawResult}",this);
			
			if(!result.ResultDictionary.ContainsKey("name")) return;
			personName.Value = result.ResultDictionary["name"].ToString();
		}
		
		[SerializeField]private NodeTexture personPicture = null;
		private void OnLoadPersonPicture(IGraphResult result)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnLoadPersonPicture:{result.RawResult}",this);
			
			if(!result.Texture) return;
			personPicture.Value = result.Texture;
		}
		#endregion
	}
}
#else
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnFacebook : SingletonM<MnFacebook>
	{
		#pragma warning disable
		[SerializeField]private string[] permission = new string[0];
		
		[SerializeField]private NodeBool isInit = null;
		[SerializeField]private NodeBool isActive = null;
		
		[SerializeField]private NodeBool isLogin = null;
		[SerializeField]private NodeString token = null;
		[SerializeField]private NodeBool isSilent = null;
		
		[SerializeField]private NodeString personName = null;
		[SerializeField]private NodeTexture personPicture = null;
		#pragma warning restore
		
		public void Login(){}
		
		public void Logout(){}
	}
}
#endif
#endif