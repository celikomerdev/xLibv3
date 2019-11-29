#if xLibv3
using System.Collections.Generic;
using UnityEngine;
using xLib.EventClass;
using xLib.xTween;

namespace xLib.xTweener
{
	public class Tweener : BaseTickNodeM
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
		private List<Tween> tween;
		
		[Header("Event")]
		public EventBool onIterate;
		public EventUnity onComplete;
		
		#region Mono
		protected override void Awaked()
		{
			RefreshTweens();
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
			Iterate();
			SetAll();
		}
		#endregion
		
		
		#region RefreshTweens
		private void RefreshTweens()
		{
			tween = new List<Tween>();
			for (int i = 0; i < target.Length; i++)
			{
				tween.AddRange(target[i].GetComponents<Tween>());
			}
			tween.RemoveAll(item => item.CanWork == false);
			for (int i = 0; i < tween.Count; i++)
			{
				tween[i].Awake();
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
		
		private void SetAll()
		{
			for(int i=0; i < tween.Count; i++)
			{
				tween[i].SetBaseRatio(normalTime);
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
			if (iteration <= 0) currentIteration = 0;
			
			if (isPingPong) playDirection = -playDirection;
			else if ((iteration <= 0) || (iteration>currentIteration)) currentTime = (byte)Mathf.Repeat(currentTime,duration);
			
			if (iteration <= 0) return;
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
		public void PlayOrReset(bool value)
		{
			if(value) PlayForward();
			else ResetMotor();
		}
		
		#region SetNormalTime
		public void SetNormalTime(float value)
		{
			if(normalTime == value) return;
			
			#if UNITY_EDITOR
			if(!Application.isPlaying) RefreshTweens();
			#endif
			
			playDirection = (value > normalTime) ? 1 : -1;
			normalTime = value;
			currentTime = normalTime*duration;
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
			if(!Application.isPlaying) SetNormalTime(playDirection>0);
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
	}
}
#endif