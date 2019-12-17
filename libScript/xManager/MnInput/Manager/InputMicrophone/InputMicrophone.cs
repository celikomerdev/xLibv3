#if xLibv2
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputMicrophone : BaseTickM
	{
		[Header("Input")]
		[SerializeField]private xMicrophone microphone;
		[SerializeField]private bool calibrateAuto = false;
		
		[SerializeField]private float multiplier = 1;
		[SerializeField]private float lerp = 10;
		
		[SerializeField]private float threshold = 0.1f;
		private float valueZero;
		
		[Header("Output")]
		[SerializeField]private InputFinal inputFinal;
		private float valueSmooth;
		private float valueCurrent;
		
		[Header("Axis")]
		[SerializeField]private NodeFloat axis;
		
		
		#region Mono
		protected override bool Register(bool register)
		{
			if(register)
			{
				Calibrate(false);
			}
			else
			{
				valueCurrent = 0;
				SetAxis();
			}
			
			return base.Register(register);
		}
		
		protected override void Tick(float tickTime)
		{
			Work(tickTime);
			SetAxis();
		}
		#endregion
		
		
		#region Work
		private void Work(float tickTime)
		{
			valueSmooth = Mathf.Lerp(valueSmooth,microphone.valueOutput-valueZero,lerp*tickTime);
			valueCurrent = valueSmooth*multiplier;
			if(threshold>valueCurrent) valueCurrent = 0;
			valueCurrent = Mathf.Clamp01(valueCurrent);
			SetAxis();
		}
		
		private void SetAxis()
		{
			axis.Value = valueCurrent;
			inputFinal.Cache(axis);
		}
		#endregion
		
		
		#region Calibrate
		public void Calibrate(bool calibrateForced)
		{
			if(calibrateAuto || calibrateForced)
			{
				valueZero = microphone.valueOutput;
				valueSmooth = 0;
			}
		}
		#endregion
	}
}
#endif