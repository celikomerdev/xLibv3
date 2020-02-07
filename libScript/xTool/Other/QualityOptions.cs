#if xLibv3
using UnityEngine;

namespace xLib.Tool
{
	public class QualityOptions : BaseMainM
	{
		[SerializeField]private bool applyExpensive = true;
		public void SetQualityLevel(int qualityLevel)
		{
			QualitySettings.SetQualityLevel(qualityLevel,applyExpensive);
		}
		
		public void TargetFrameRate(int targetFrameRate)
		{
			Application.targetFrameRate = targetFrameRate;
		}
		
		public void SleepTimeout(int targetFrameRate)
		{
			Screen.sleepTimeout = targetFrameRate;
		}
		
		public void ShaderGlobalMaximumLOD(int globalMaximumLOD)
		{
			Shader.globalMaximumLOD = globalMaximumLOD;
		}
	}
}
#endif