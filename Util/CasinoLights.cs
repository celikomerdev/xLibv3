#if xLibv3
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace xLib.xNew
{
	public class CasinoLights : BaseWorkM
	{
		public int [] headIndex;
		public Sprite[] sprite = new Sprite[2];
		public Image[] array;
		private int count;
		
		public void Animate(bool value)
		{
			StopAllCoroutines();
			if(CanDebug) Debug.LogFormat(this,this.name+":Animate:{0}",value);
			
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sprite = sprite[0];
			}
			
			if (value)
			{
				for (int i = 0; i < headIndex.Length; i++)
				{
					StartCoroutine(Play(headIndex[i]));
				}
			}
			else
			{
				StartCoroutine(Idle(sprite[0],sprite[1]));
			}
		}
		
		private IEnumerator Idle(Sprite sp1,Sprite sp2)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Idle");
			yield return new WaitForSeconds(0.2f);
			count++;
			
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sprite = (i%2 == 0)? sp1:sp2;
			}
			
			if (count < Random.Range(10,30))
			{
				StartCoroutine(Idle(sp2,sp1));
			}
			else
			{
				StartCoroutine(Symetric(1));
			}
		}
		
		
		private IEnumerator Symetric(int index)
		{
			index = index%array.Length;
			if(CanDebug) Debug.LogFormat(this,this.name+":Symetric:{0}",index);
			
			if(index==0)
			{
				count = 0;
				StartCoroutine(Idle(sprite[0],sprite[1]));
			}
			else
			{
				array[index].sprite = sprite[1];
				yield return new WaitForSeconds(0.1f);
				array[index].sprite = sprite[0];
				StartCoroutine(Symetric(index+1));
			}
		}
		
		
		private IEnumerator Play(int index)
		{
			index = index%array.Length;
			if(CanDebug) Debug.LogFormat(this,this.name+":Play:{0}",index);
			
			array[index].sprite = sprite[1];
			yield return new WaitForSeconds(0.1f);
			array[index].sprite = sprite[0];
			StartCoroutine(Play(index+1));
		}
	}
}
#endif