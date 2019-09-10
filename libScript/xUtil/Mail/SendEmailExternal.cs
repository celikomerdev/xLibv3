#if xLibv2
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xNew
{
	public class SendEmailExternal : BaseWorkM
	{
		[SerializeField]private string to;
		[SerializeField]private string subject = "Subject";
		[SerializeField]private MonoGroup nodeBody;
		
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			string body = "";
			body += "Please Enter Your Message Here\n\n\n\n";
			
			body += "____________\n";
			body += "Please Do Not Modify This\n";
			body += nodeBody.SerializedObjectName.ToString();
			
			string tempUrl = string.Format("mailto:{0}?subject={1}&body={2}",to,EscapeURL(subject),EscapeURL(body));
			
			if(CanDebug) Debug.LogFormat(this,this.name+":Url:{0}",tempUrl);
			Application.OpenURL(tempUrl);
		}
		
		private string EscapeURL(string url) 
		{
			return WWW.EscapeURL(url).Replace("+","%20");
		}
	}
}
#endif