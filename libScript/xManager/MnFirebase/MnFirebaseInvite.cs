#if xLibv2
#if FirebaseInvite
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Invites;

namespace xLib
{
	public class MnFirebaseInvite : SingletonM<MnFirebaseInvite>
	{
		
		#region Mono
		private void Start()
		{
			// FirebaseInvites.InviteReceived += OnInviteReceived;
			// FirebaseInvites.InviteNotReceived += OnInviteNotReceived;
			// FirebaseInvites.ErrorReceived += OnErrorReceived;
		}
		
		
		private void OnDestroy()
		{
			// FirebaseInvites.InviteReceived -= OnInviteReceived;
			// FirebaseInvites.InviteNotReceived -= OnInviteNotReceived;
			// FirebaseInvites.ErrorReceived -= OnErrorReceived;
		}
		#endregion
		
		
		
		#region Receiver
		// private void OnInviteReceived(object sender, InviteReceivedEventArgs e)
		// {
		// 	if (e.InvitationId != "")
		// 	{
		// 		Debug.Log("Invite received: Invitation ID: " + e.InvitationId);
		// 		Firebase.Invites.FirebaseInvites.ConvertInvitationAsync(e.InvitationId).ContinueWith(HandleConversionResult);
		// 	}
		// 	if (e.DeepLink.ToString() != "")
		// 	{
		// 		Debug.Log("Invite received: Deep Link: " + e.DeepLink);
		// 	}
		// }
		
		// private void HandleConversionResult(Task convertTask)
		// {
		// 	if (convertTask.IsCanceled)
		// 	{
		// 		Debug.Log("Conversion canceled.");
		// 	}
		// 	else if (convertTask.IsFaulted)
		// 	{
		// 		Debug.Log("Conversion encountered an error:");
		// 		Debug.Log(convertTask.Exception.ToString());
		// 	}
		// 	else if (convertTask.IsCompleted)
		// 	{
		// 		Debug.Log("Conversion completed successfully!");
		// 	}
		// }
		
		// private void OnInviteNotReceived(object sender, System.EventArgs e)
		// {
		// 	Debug.Log("No Invite or Deep Link received on start up");
		// }
		
		
		// private void OnErrorReceived(object sender, InviteErrorReceivedEventArgs e)
		// {
		// 	Debug.LogError("Error occurred received the invite: " + e.ErrorMessage);
		// }
		#endregion
		
		
		
		#region Sender
		public void SendInvite()
		{
			Invite invite = new Invite();
			
			invite.TitleText = "Awesome Game!";
			invite.MessageText = "Lets Challenge!";
			invite.CallToActionText = "Download it for FREE";
			invite.DeepLinkUrl = new Uri("http://google.com/abc");
			
			
			FirebaseInvites.SendInviteAsync(invite).ContinueWith(HandleSentInvite);
		}
		
		
		private void HandleSentInvite(Task<SendInviteResult> sendTask)
		{
			if (sendTask.IsCanceled)
			{
				Debug.Log("Invitation canceled.");
			}
			else if (sendTask.IsFaulted)
			{
				Debug.Log("Invitation encountered an error:");
				Debug.Log(sendTask.Exception.ToString());
			}
			else if (sendTask.IsCompleted)
			{
				int inviteCount = 0;
				foreach (string id in sendTask.Result.InvitationIds)
				{
					Debug.Log("InvitationId: "+id);
					inviteCount++;
				}
				Debug.Log("inviteCount: "+inviteCount);
			}
		}
		#endregion
		
		
	}
}
#else
using UnityEngine;

namespace xLib
{
	public class MnFirebaseInvite : SingletonM<MnFirebaseInvite>
	{
	}
}
#endif
#endif