#if xLibv2
#if Facebook
using Facebook.Unity;
using UnityEngine;

namespace xLib.xFacebook
{
	public class InvitableFriends : BaseActiveM
	{
		[SerializeField]private string query = "me/invitable_friends?fields=id,name,picture.height(100).width(100)&limit=10";
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			FB.API(query,HttpMethod.GET,Callback);
		}
		
		private void Callback(IGraphResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",result.ToString());
			
			// JSONArray dataFriends = null;
			// var N = JSON.Parse(result.RawResult);
			// if(CanDebug) Debug.LogFormat(this,this.name+":InvitableFriend:{0}",N.ToString());
			// dataFriends = N["data"].AsArray;
		}
	}
}
#else
namespace xLib.xFacebook
{
	public class InvitableFriends : BaseActiveM
	{
		public void Call(){}
	}
}
#endif
#endif