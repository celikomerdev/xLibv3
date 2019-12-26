#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputAcceleration : BaseTickNodeM
	{
		[Header("Input")]
		[SerializeField]private NodeFloat multiplier;
		[SerializeField]private NodeFloat lerp;
		
		[SerializeField]private bool calibrateAuto = false;
		private Vector3 valueZero;
		
		[Header("Output")]
		[SerializeField]private InputFinal inputFinal;
		private Vector3 valueCurrent;
		private Vector3 valueSmooth;
		
		[Header("Axis")]
		[SerializeField]private NodeFloat axisX;
		[SerializeField]private NodeFloat axisY;
		[SerializeField]private NodeFloat axisZ;
		
		
		#region Mono
		protected override bool OnRegister(bool register)
		{
			if(register)
			{
				Calibrate(false);
			}
			else
			{
				valueCurrent = Vector3.zero;
				valueSmooth = Vector3.zero;
				SetAxis();
			}
			
			return base.OnRegister(register);
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
			valueCurrent = Input.acceleration-valueZero;
			valueCurrent *= multiplier.Value;
			valueCurrent = Mathx.MathVector3.Clamp(valueCurrent);
			
			if(lerp.Value<100) valueSmooth = Vector3.Lerp(valueSmooth,valueCurrent,lerp.Value*tickTime);
			else valueSmooth = valueCurrent;
		}
		
		private void SetAxis()
		{
			axisX.Value = valueSmooth.x;
			axisY.Value = valueSmooth.y;
			axisZ.Value = valueSmooth.z;
			
			inputFinal.Cache(axisX);
			inputFinal.Cache(axisY);
			inputFinal.Cache(axisZ);
		}
		#endregion
		
		
		#region Calibrate
		public void Calibrate(bool calibrateForced)
		{
			if(calibrateAuto || calibrateForced)
			{
				valueZero = Input.acceleration;
				valueSmooth = Vector3.zero;
			}
		}
		#endregion
	}
}
#endif