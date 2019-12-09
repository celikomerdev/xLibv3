#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnReview : SingletonM<MnReview>
	{
		[SerializeField]private NodeInt playerReview = null;
		[SerializeField]private bool useIOS = false;
		[SerializeField]private bool useCustom = false;
		[SerializeField]private EventUnity eventOpenCustom = new EventUnity();
		
		public void AskRewiew()
		{
			if(playerReview.Value!=0) return;
			
			#if UNITY_IOS
			if(useIOS && UnityEngine.iOS.Device.RequestStoreReview())
			{
				playerReview.Value = -1;
				return;
			}
			#endif
			
			PopupCustom();
			PopupStandard();
		}
		
		private void PopupCustom()
		{
			if(!useCustom) return;
			eventOpenCustom.Invoke();
		}
		
		private void PopupStandard()
		{
			if(useCustom) return;
			StPopupWindow.Reset();
			StPopupWindow.Header(MnLocalize.GetValue(""));
			StPopupWindow.Body(MnLocalize.GetValue("Please Rate and Write What You Think to Support"));
			StPopupWindow.Accept(MnLocalize.GetValue("Submit"));
			StPopupWindow.Decline(MnLocalize.GetValue("Close"));
			
			StPopupWindow.Listener(true,Listener);
			void Listener(bool result)
			{
				StPopupWindow.Listener(false,Listener);
				if(!result) return;
				playerReview.Value = -1;
				Application.OpenURL(MnKey.GetValue("App-Link"));
			}
			StPopupWindow.Show();
		}
	}
}
#endif