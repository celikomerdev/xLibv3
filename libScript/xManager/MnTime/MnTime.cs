#if xLibv3
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public class MnTime : SingletonM<MnTime>
	{
		[Header("Manager")]
		[SerializeField]public NodeFloat timeScale = null;
		[SerializeField]public NodeBool timePause = null;
		
		protected override void OnEnabled()
		{
			timeScale.Listener(register:true,call:SetTimeScale,onRegister:false);
			timePause.Listener(register:true,call:SetTimePause,onRegister:false);
		}
		
		protected override void OnDisabled()
		{
			timeScale.Listener(register:true,call:SetTimeScale,onRegister:false);
			timePause.Listener(register:false,call:SetTimePause,onRegister:false);
		}
		
		private void SetTimeScale(float value)
		{
			Time.timeScale = value;
		}
		
		private float timeScaleCache = 1f;
		private void SetTimePause(bool value)
		{
			if(value)
			{
				if(timeScale.Value!=0) timeScaleCache = timeScale.Value;
				timeScale.Value = 0;
			}
			else
			{
				timeScale.Value = timeScaleCache;
			}
		}
	}
}
#endif