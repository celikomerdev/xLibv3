#if xLibv2
using UnityEngine;
using xLib.ToolEventClass;

namespace xLib.ToolMnQuality
{
	public class MnQualityChange : BaseActiveM
	{
		public EventInt qualityLevel;
		
		public void Set(int value)
		{
			MnQuality.ins.QualityLevel = value;
			qualityLevel.Invoke(value);
		}
		
		#region TrySet
		private int qualityTemp;
		public void TrySet(int value)
		{
			qualityTemp = value;
			
			if(qualityTemp <= MnQuality.ins.qualityAuto.Value)
			{
				Set(qualityTemp);
				return;
			}
			
			if(qualityTemp <= MnQuality.ins.qualityLevel.Value)
			{
				Set(qualityTemp);
				return;
			}
			
			StPopupWindow.Reset();
			StPopupWindow.HeaderLocalized("warning");
			StPopupWindow.BodyLocalized("you may lose performance");
			StPopupWindow.AcceptLocalized("accept");
			StPopupWindow.Listener(Listener,true);
			StPopupWindow.Show();
		}
		
		private void Listener(bool value)
		{
			StPopupWindow.Listener(Listener,false);
			if(value) Set(qualityTemp);
			else Set(MnQuality.ins.QualityLevel);
		}
		#endregion
	}
}
#endif