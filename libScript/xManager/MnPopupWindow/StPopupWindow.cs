#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public static class StPopupWindow
	{
		#region Result
		public static void Listener(UnityAction<bool> call,bool register)
		{
			if(MnPopupWindow.ins==null) return;
			int tempOrder = BaseRegisterM.Order;
			BaseRegisterM.Order = 0;
			MnPopupWindow.ins.result.Listener(register,call);
			BaseRegisterM.Order = tempOrder;
		}
		
		public static bool autoAccept = false;
		public static void Result(bool value)
		{
			autoAccept = false;
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Result(value);
		}
		#endregion
		
		
		#region Show
		public static void Reset()
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Reset();
		}
		
		public static void Show()
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Show();
			
			if(autoAccept) Result(true);
		}
		#endregion
		
		
		#region Header
		public static void Header(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Header(value);
		}
		
		public static void HeaderLocalized(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.HeaderLocalized(value);
		}
		#endregion
		
		
		#region Body
		public static void Body(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Body(value);
		}
		
		public static void BodyLocalized(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.BodyLocalized(value);
		}
		#endregion
		
		
		#region Accept
		public static void Accept(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Accept(value);
		}
		
		public static void AcceptLocalized(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.AcceptLocalized(value);
		}
		#endregion
		
		#region Accept
		public static void Decline(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Decline(value);
		}
		
		public static void DeclineLocalized(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.DeclineLocalized(value);
		}
		#endregion
	}
}
#endif