#if xLibv3
using UnityEngine;

namespace xLib.ToolApplication
{
	public class ApplicationQuit : BaseActiveM
	{
		public void TryQuit()
		{
			StPopupWindow.Reset();
			StPopupWindow.Header(MnLocalize.GetValue("Warning"));
			StPopupWindow.Body(MnLocalize.GetValue("Do You Want To Quit?"));
			StPopupWindow.Accept(MnLocalize.GetValue("Yes"));
			StPopupWindow.Decline(MnLocalize.GetValue("No"));
			
			StPopupWindow.Listener(register:true,call:Listener);
			void Listener(bool value)
			{
				StPopupWindow.Listener(register:false,call:Listener);
				if(value) Quit();
			}
			StPopupWindow.Show();
		}
		
		public void Quit()
		{
			if(CanDebug) Debug.LogWarning($"{this.name}:Quit",this);
			Application.Quit();
		}
	}
}
#endif