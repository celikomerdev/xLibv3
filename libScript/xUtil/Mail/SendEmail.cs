#if xLibv2
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace xLib.xNew
{
	public class SendEmail : BaseWorkM
	{
		[SerializeField]private string pass;
		[SerializeField]private string from;
		[SerializeField]private string to;
		
		[SerializeField]private string subject = "Subject";
		[SerializeField]private MonoGroup nodeBody;
		
		private bool inCall;
		private SmtpClient smtpClient;
		public void Call()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Call");
			
			MailAddress mailAdressFrom = new MailAddress(from);
			MailAddress mailAdressTo = new MailAddress(to);
			MailMessage mailMessage = new MailMessage(mailAdressFrom,mailAdressTo);
			
			mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
			mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
			
			mailMessage.Subject = subject;
			mailMessage.Body = nodeBody.SerializedObjectName.ToString();
			
			smtpClient = new SmtpClient("smtp.gmail.com");
			smtpClient.SendCompleted += Callback;
			
			smtpClient.Port = 587;
			smtpClient.Credentials = new System.Net.NetworkCredential(from,pass) as ICredentialsByHost;
			//smtpClient.UseDefaultCredentials = true;
			smtpClient.EnableSsl = true;
			
			ServicePointManager.ServerCertificateValidationCallback =
			delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				if(CanDebug) Debug.LogFormat("ServerCertificateValidationCallback");
				return true;
			};
			
			inCall = true;
			smtpClient.SendAsync(mailMessage,"");
			
			StPopupBar.MessageLocalized("please wait");
		}
		
		public void CallCancel()
		{
			if(!inCall) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":CallCancel");
			
			smtpClient.SendAsyncCancel();
			ClearCache();
		}
		
		private void Callback(object sender,AsyncCompletedEventArgs eventArgs)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Callback:{0}",eventArgs.UserState.ToString());
			
			if(eventArgs.Error != null)
			{
				xDebug.LogExceptionFormat("Callback:Error:{0}",eventArgs.Error);
				StPopupBar.MessageLocalized("send failed");
				return;
			}
			if(eventArgs.Cancelled)
			{
				StPopupBar.MessageLocalized("send cancelled");
				return;
			}
			StPopupBar.MessageLocalized("send successfull");
			
			ClearCache();
		}
		
		private void ClearCache()
		{
			if(!inCall) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":ClearCache");
			
			smtpClient = null;
			inCall = false;
		}
	}
}
#endif