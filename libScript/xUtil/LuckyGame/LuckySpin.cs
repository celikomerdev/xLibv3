#if xLibv3
using System.Collections;
using UnityEngine;
using xLib.EventClass;
using xLib.ToolRandom;
using xLib.xTween;

namespace xLib
{
	public class LuckySpin : BaseM
	{
		[SerializeField]private Rigidbody2D rigid = null;
		[SerializeField]private Tween tween = null;
		[SerializeField]private int loop = 2;
		[SerializeField]private int duration = 1;
		[SerializeField]private AnimationCurve curve = AnimationCurve.Linear(0,0,1,1);
		
		[SerializeField]private LuckyIndex luckyIndex = null;
		
		[Tooltip("Rotation of prizes - sync with luckyIndex")]
		[SerializeField]private int[] rotArray = new int[0];
		
		private bool inSpin = false;
		[SerializeField]private EventBool onSpin = new EventBool();
		
		public void Call()
		{
			Call(rotArray[luckyIndex.GetRandom()]);
		}
		
		public void Call(float value)
		{
			if(inSpin) return;
			inSpin = true;
			onSpin.Invoke(inSpin);
			StartCoroutine(eSpin(value));
		}
		
		private IEnumerator eSpin(float value)
		{
			float rotStart = rigid.rotation%360;
			rigid.rotation = rotStart;
			
			float rotFinal = 360*loop;
			rotFinal -= rotStart;
			rotFinal -= value;
			
			float timer = 0.0f;
			while (timer<duration)
			{
				float curveOutput = curve.Evaluate(timer/duration);
				tween.SetBaseRatio(curveOutput);
				
				float rotFrame = rotFinal*curveOutput;
				rigid.MoveRotation(rotStart+rotFrame);
				timer += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			rigid.MoveRotation(rotStart+rotFinal);
			
			inSpin = false;
			onSpin.Invoke(inSpin);
		}
	}
}
#endif