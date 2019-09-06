#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.EventClass;
using xLib.xTween;

namespace xLib.xTweener
{
	public abstract class Tweener : BaseTickNodeM
	{
		[Header("Tweening")]
		public bool playAuto = true;
		public bool isPingPong;
		public byte iteration = 1;
		public float playDirection = 1;
		
		[Header("Timing")]
		public float duration = 1f;
		
		[Header("Target")]
		public GameObject[] target;
		
		[Header("Event")]
		public EventBool onIterate;
		public EventUnity onComplete;
		
		protected virtual float RatioForward(float value)
		{
			return value;
		}
		
		protected virtual float RatioBackward(float value)
		{
			return value;
		}
		
		#region Mono
		protected override void Awaked()
		{
			RefreshTarget();
		}
		
		protected override void OnActive(bool value)
		{
			if(!value && iteration == 0)
			{
				ResetMotor();
				return;
			}
			
			if(playAuto) Play(value);
		}
		
		protected override void Tick(float tickTime)
		{
			NormalTime(tickTime);
			CurveValue();
			Iterate();
			
			SetAll();
		}
		#endregion
		
		
		#region RefreshTarget
		private void RefreshTarget()
		{
			tweens = new List<Tween>();
			for (int i = 0; i < target.Length; i++)
			{
				tweens.AddRange(target[i].GetComponents<Tween>());
			}
			tweens.RemoveAll(item => item.CanWork == false);
			for (int i = 0; i < tweens.Count; i++)
			{
				tweens[i].Awake();
			}
		}
		#endregion
		
		
		#region Behave
		private float currentTime;
		private float normalTime;
		private void NormalTime(float tickTime)
		{
			currentTime += playDirection * tickTime;
			
			currentTime = Mathf.Clamp(currentTime, 0, duration);
			normalTime = Mathf.Clamp01(currentTime / duration);
		}
		
		private float curveValue;
		private void CurveValue()
		{
			if (playDirection>0) curveValue = RatioForward(normalTime);
			else curveValue = RatioBackward(normalTime);
		}
		
		private List<Tween> tweens;
		private void SetAll()
		{
			for(int i=0; i < tweens.Count; i++)
			{
				tweens[i].SetBaseRatio(curveValue);
			}
		}
		
		private void Iterate()
		{
			if (normalTime == 0 || normalTime == 1) OnIterate();
		}
		
		private byte currentIteration;
		private void OnIterate()
		{
			onIterate.Invoke(playDirection>0);
			
			currentIteration++;
			if(iteration <= 0) currentIteration = 0;
			
			if (isPingPong) playDirection = -playDirection;
			else if (currentIteration < iteration) currentTime = (byte)Mathf.Repeat(currentTime,duration);
			
			if (currentIteration >= iteration) OnComplete();
		}
		
		private void OnComplete()
		{
			currentIteration = (byte)Mathf.Repeat(currentIteration,iteration);
			currentTime = Mathf.Clamp(currentTime, 0, duration);
			onComplete.Invoke();
			IsRegister = false;
		}
		#endregion
		
		
		#region PublicMethods
		public void ResetMotor()
		{
			IsRegister = false;
			currentIteration = 0;
			SetNormalTime(0);
		}
		
		#region SetRatio
		public void SetRatio(float value)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying) RefreshTarget();
			#endif
			
			if(value == curveValue) return;
			curveValue = value;
			SetAll();
		}
		
		public void SetRatio(bool isForward)
		{
			SetRatio(isForward ? 1 : 0);
		}
		#endregion
		
		#region SetNormalTime
		public void SetNormalTime(float value)
		{
			#if UNITY_EDITOR
			if(!Application.isPlaying) RefreshTarget();
			#endif
			
			playDirection = (value > normalTime) ? 1 : -1;
			normalTime = value;
			currentTime = normalTime*duration;
			CurveValue();
			SetAll();
		}
		
		public void SetNormalTime(bool isForward)
		{
			SetNormalTime(isForward ? 1 : 0);
		}
		#endregion
		
		#region Play
		public void Play(float direction)
		{
			if (direction != 0) playDirection = direction;
			else playDirection = -playDirection;
			currentIteration = (byte)Mathf.Repeat(currentIteration,iteration);
			if(!Application.isPlaying) SetNormalTime(playDirection);
			IsRegister = true;
		}
		
		public void Play(bool isForward)
		{
			Play(isForward ? 1 : -1);
		}
		
		public void PlayForward()
		{
			Play(1);
		}
		
		public void PlayReverse()
		{
			Play(-1);
		}
		
		public void PlayToggle()
		{
			Play(0);
		}
		#endregion
		
		#region PlayForced
		public void PlayForced(float direction)
		{
			if (direction > 0) currentTime = 0;
			if (direction < 0) currentTime = duration;
			Play(direction);
		}
		
		public void PlayForced(bool isForward)
		{
			PlayForced(isForward ? 1 : -1);
		}
		
		public void PlayForcedForward()
		{
			PlayForced(1);
		}
		
		public void PlayForcedReverse()
		{
			PlayForced(-1);
		}
		#endregion
		#endregion
		
		public void PlayOrReset(bool value)
		{
			if(value) PlayForward();
			else ResetMotor();
		}
	}
}
#endif