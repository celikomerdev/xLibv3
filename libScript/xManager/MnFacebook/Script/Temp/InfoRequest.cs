#if xLibv2
#if Facebook
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;
using xLib;

namespace xLib.xFacebook
{
	public class InfoRequest : BaseActiveM
	{
		public RawImage imageSender;
		public Text textSender;
		public Text textScore;
		private Data invitation;
		
		public void Setup(Data value)
		{
			invitation = value;
			textSender.text = invitation.from.name;
			textScore.text = invitation.data;
			LoadUrl.Load("https://graph.facebook.com/"+invitation.from.id+"/picture?type=normal&height=128&width=128",OnLoadPicture);
		}
		
		private void OnLoadPicture(WWW www)
		{
			if(!imageSender) return;
			imageSender.texture = www.texture;
		}
		
		public void DeleteRequest()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":DeleteRequest");
			FB.API(invitation.id,HttpMethod.DELETE,OnDeleteRequest);
		}
		
		private void OnDeleteRequest(IResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnDeleteRequest:{0}",result.ToString());
			ListRequest.ins.Refresh();
		}
		
		public void SendScore(int score)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":SendScore:{0}",score);
			List<string> temp = new List<string>();
			temp.Add(invitation.from.id);
			
			FB.AppRequest("It is Your Turn",OGActionType.TURN,null,temp,score.ToString(),"I Scored "+score.ToString(),OnSendScore);
		}
		
		protected void OnSendScore(IResult result)
		{
			if(string.IsNullOrEmpty(result.RawResult)) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnSendScore:{0}",result.ToString());
		}
	}
}
#endif
#endif