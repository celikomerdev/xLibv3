#if xLibv3
using System.Collections;
using UnityEngine;
using xLib.EventClass;
using xLib.xTween;

namespace xLib
{
	public class LuckySpin : BaseMainM
	{
		[SerializeField]private Transform trans = null;
		[SerializeField]private Tween tween = null;
		[SerializeField]private int loop = 2;
		[SerializeField]private int duration = 1;
		[SerializeField]private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		
		[SerializeField]private int[] arrayRotation = new int[0];
		
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
			StartCoroutine(eSpin(value));
		}
		
		private IEnumerator eSpin(float value)
		{
			Vector3 rotStart = trans.eulerAngles;
			rotStart.z %= 360f;
			trans.eulerAngles = rotStart;
			
			float rotFinal = 360*loop;
			rotFinal -= rotStart.z;
			rotFinal -= value;
			
			float timer = 0.0f;
			while (timer<duration)
			{
				float curveOutput = curve.Evaluate(timer/duration);
				tween.SetBaseRatio(curveOutput);
				
				float rotFrame = rotFinal*curveOutput;
				trans.eulerAngles = rotStart+Vector3.forward*rotFrame;
				timer += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			trans.eulerAngles = rotStart+Vector3.forward*rotFinal;
			
			inSpin = false;
			onSpin.Invoke(inSpin);
		}
	}
}
#endif