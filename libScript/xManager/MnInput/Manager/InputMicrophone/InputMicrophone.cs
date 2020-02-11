#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputMicrophone : BaseTickNodeM
	{
		[Header("Input")]
		[SerializeField]private xMicrophone microphone = null;
		[SerializeField]private bool calibrateAuto = false;
		
		[SerializeField]private float multiplier = 1;
		[SerializeField]private float lerp = 10;
		
		[SerializeField]private float threshold = 0.1f;
		private float valueZero;
		
		[Header("Output")]
		private float valueSmooth = 0;
		private float valueCurrent = 0;
		
		[Header("Axis")]
		[SerializeField]private NodeFloat axis = null;
		
		
		#region Mono
		protected override bool TryRegister(bool register)
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
			
			return base.TryRegister(register);
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