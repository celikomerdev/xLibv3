#if xLibv3
using System.Collections;
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;
using xLib.xTween;

namespace xLib
{
	public class MiniGameSpin : BaseWorkM
	{
		[SerializeField]private Transform trans = null;
		[SerializeField]private NodeFloat tickTime = null;
		[SerializeField]private Tween tween = null;
		[SerializeField]private int loop = 2;
		[SerializeField]private int duration = 1;
		[SerializeField]private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		
		[SerializeField]public float[] arrayRotation = new float[0];
		
		private bool inSpin = false;
		[SerializeField]private EventBool onSpin = new EventBool();
		
		public void CallIndex(int index)
		{
			CallRotation(arrayRotation[index]);
		}
		
		public void CallRotation(float value)
		{
			if(inSpin) return;
			inSpin = true;
			onSpin.Invoke(inSpin);
			this.NewCoroutine(eSpin(value),CanDebug);
		}
		
		private IEnumerator eSpin(float value)
		{
			yield return new WaitForEndOfFrame();
			Vector3 rotStart = trans.localEulerAngles;
			rotStart.z %= 360f;
			trans.localEulerAngles = rotStart;
			
			float rotFinal = 360*loop;
			rotFinal -= rotStart.z;
			rotFinal -= value;
			
			float timer = 0.0f;
			while (timer<duration)
			{
				float curveOutput = curve.Evaluate(timer/duration);
				tween.SetBaseRatio(curveOutput);
				
				float rotFrame = rotFinal*curveOutput;
				trans.localEulerAngles = rotStart+Vector3.forward*rotFrame;
				timer += tickTime.Value;
				yield return new WaitForEndOfFrame();
			}
			trans.localEulerAngles = rotStart+Vector3.forward*rotFinal;
			
			inSpin = false;
			onSpin.Invoke(inSpin);
		}
	}
}
#endif