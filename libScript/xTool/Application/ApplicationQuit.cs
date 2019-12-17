#if xLibv3
using UnityEngine;

namespace xLib.ToolApplication
{
	public class ApplicationQuit : BaseActiveM
	{
		public void TryQuit()
		{
			StPopupWindow.Reset();
			StPopupWindow.HeaderLocalized("warning");
			StPopupWindow.BodyLocalized("do you want to quit?");
			StPopupWindow.AcceptLocalized("quit");
			
			StPopupWindow.Listener(true,call:Listener);
			StPopupWindow.Show();
		}
		
		private void Listener(bool value)
		{
			StPopupWindow.Listener(false,call:Listener);
			if(value) Quit();
		}
		
		public void Quit()
		{
			if(CanDebug) Debug.LogWarningFormat(this,this.name+":Quit");
			Application.Quit();
		}
	}
}
#endif