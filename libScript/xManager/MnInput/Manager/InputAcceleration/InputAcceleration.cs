#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.xInput
{
	public class InputAcceleration : BaseTickNodeM
	{
		[Header("Input")]
		[SerializeField]private bool calibrateAuto = false;
		private Vector3 valueZero = Vector3.zero;
		
		[Header("Output")]
		private Vector3 valueCurrent = Vector3.zero;
		
		[Header("Axis")]
		[SerializeField]private EventFloat axisX = new EventFloat();
		[SerializeField]private EventFloat axisY = new EventFloat();
		[SerializeField]private EventFloat axisZ = new EventFloat();
		
		
		#region Mono
		protected override bool TryRegister(bool register)
		{
			Calibrate(false);
			valueCurrent = Vector3.zero;
			SetAxis();
			return base.TryRegister(register);
		}
		
		protected override void Tick(float tickTime)
		{
			valueCurrent = Input.acceleration-valueZero;
			SetAxis();
		}
		#endregion
		
		
		#region Custom
		private void SetAxis()
		{
			axisX.Value = valueCurrent.x;
			axisY.Value = valueCurrent.y;
			axisZ.Value = valueCurrent.z;
		}
		
		public void Calibrate(bool calibrateForced)
		{
			if(calibrateAuto || calibrateForced)
			{
				valueZero = Input.acceleration;
			}
		}
		#endregion
	}
}
#endif