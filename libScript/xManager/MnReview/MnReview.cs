#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnReview : SingletonM<MnReview>
	{
		[SerializeField]private NodeInt playerReview = null;
		[SerializeField]private bool useNative = false;
		[SerializeField]private bool useStandard = false;
		[SerializeField]private EventUnity eventOpenCustom = new EventUnity();
		
		public void AskRewiew()
		{
			if(playerReview.Value!=0) return;
			if(PopupNative()) return;
			PopupStandard();
			PopupCustom();
		}
		
		private bool PopupNative()
		{
			if(!useNative) return false;
			bool returnValue = false;
			
			#if UNITY_IOS
			returnValue = UnityEngine.iOS.Device.RequestStoreReview();
			#endif
			
			if(returnValue) playerReview.Value = -1;
			return returnValue;
		}
		
		private void PopupStandard()
		{
			if(!useStandard) return;
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
				Application.OpenURL(MnKey.GetValue("App_Link"));
			}
			StPopupWindow.Show();
			
		}
		
		private void PopupCustom()
		{
			if(useStandard) return;
			eventOpenCustom.Invoke();
		}
	}
}
#endif