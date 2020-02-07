#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;

namespace xLib.ToolMnQuality
{
	public class MnQualityChange : BaseActiveM
	{
		[SerializeField]private NodeInt qualityLevel = null;
		[SerializeField]private NodeInt qualityAuto = null;
		[SerializeField]private EventInt eventQualityLevel = new EventInt();
		
		#region TrySet
		public void TrySet(int value)
		{
			if(value <= qualityLevel.Value)
			{
				eventQualityLevel.Value = value;
				return;
			}
			
			if(value <= qualityAuto.Value)
			{
				eventQualityLevel.Value = value;
				return;
			}
			
			StPopupWindow.Reset();
			StPopupWindow.Header(MnLocalize.GetValue("Warning"));
			StPopupWindow.Body(MnLocalize.GetValue("You May Lose Performance"));
			StPopupWindow.Accept(MnLocalize.GetValue("Accept"));
			StPopupWindow.Decline(MnLocalize.GetValue("Decline"));
			StPopupWindow.Listener(register:true,call:Listener);
			void Listener(bool result)
			{
				StPopupWindow.Listener(register:false,Listener);
				
				if(result) eventQualityLevel.Value = value;
				else eventQualityLevel.Value = qualityLevel.Value;
			}
			StPopupWindow.Show();
		}
		#endregion
	}
}
#endif