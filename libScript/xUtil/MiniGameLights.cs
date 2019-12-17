#if xLibv3
using System.Collections;
using UnityEngine;

namespace xLib.xNew
{
	public class MiniGameLights : BaseWorkM
	{
		public float durationTemp = 5f;
		public float tick = 0.1f;
		public bool defaultState = false;
		public byte idleMod = 2;
		public int [] headIndex;
		public GameObject[] array;
		
		#region Main
		private void OnEnable()
		{
			Animate(false);
		}
		
		private void ResetState()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":ResetState");
			StopAllCoroutines();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(defaultState);
			}
		}
		
		public void Animate(bool value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Animate:{0}",value);
			if(value) Symetric(durationTemp);
			else Idle(Random.Range(1f,3f));
		}
		#endregion
		
		
		#region Idle
		public void Idle(float time)
		{
			ResetState();
			if(CanDebug) Debug.LogFormat(this,this.name+":Idle:{0}",time);
			StartCoroutine(FlowIdle());
			this.WaitForSeconds(call:()=>Symetric(tick*array.Length*2),delay:time);
		}
		
		private IEnumerator FlowIdle()
		{
			bool state = defaultState;
			while(true)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive((i%idleMod==0)? state:!state);
				}
				state = !state;
				yield return new WaitForSecondsRealtime(tick*2f);
			}
		}
		#endregion
		
		
		#region Symetric
		public void Symetric(float time)
		{
			ResetState();
			if(CanDebug) Debug.LogFormat(this,this.name+":Symetric:{0}",time);
			StartCoroutine(FlowSymetric());
			this.WaitForSeconds(call:()=>Idle(Random.Range(1f,3f)),delay:time);
		}
		
		private IEnumerator FlowSymetric()
		{
			int offset = 0;
			while(true)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(defaultState);
				}
				for (int i = 0; i < headIndex.Length; i++)
				{
					array[(headIndex[i]+offset)%array.Length].SetActive(!defaultState);
				}
				offset++;
				yield return new WaitForSecondsRealtime(tick);
			}
		}
		#endregion
	}
}
#endif