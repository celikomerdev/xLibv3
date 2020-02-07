#if xLibv3
using UnityEngine.Events;

namespace xLib
{
	public static class StPopupWindow
	{
		#region Result
		public static void Listener(bool register,UnityAction<bool> call,string viewId="",int order=0)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.result.Listener(register:register,call:call,viewId:viewId,order:order);
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
		#endregion
		
		
		#region Body
		public static void Body(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Body(value);
		}
		#endregion
		
		
		#region Accept
		public static void Accept(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Accept(value);
		}
		#endregion
		
		#region Accept
		public static void Decline(string value)
		{
			if(MnPopupWindow.ins==null) return;
			MnPopupWindow.ins.Decline(value);
		}
		#endregion
	}
}
#endif