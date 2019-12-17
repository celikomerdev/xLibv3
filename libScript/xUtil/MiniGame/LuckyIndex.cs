#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolRandom
{
	[System.Serializable]public class LuckyIndex : BaseWorkM
	{
		[SerializeField]private float[] arrayLuck = new float[0];
		public float[] ArrayLuck
		{
			get
			{
				return arrayLuck;
			}
			set
			{
				arrayLuck = value;
				LuckMax = 0;
			}
		}
		
		
		public int GetRandom()
		{
			if(LuckMax==0) LuckMax = 0;
			float random = Random.Range(0,LuckMax);
			if(CanDebug) Debug.LogFormat(this,this.name+":Random:{0}",random);
			
			int index = 0;
			float totalLuck = 0;
			
			for (int i=0; i<arrayLuck.Length; i++)
			{
				totalLuck += arrayLuck[i];
				
				if(totalLuck > random)
				{
					index = i;
					break;
				}
			}
			
			if(CanDebug) Debug.LogFormat(this,this.name+":Index:{0}",index);
			return index;
		}
		
		private float luckMax;
		public float LuckMax
		{
			get
			{
				return luckMax;
			}
			set
			{
				luckMax = value;
				for (int i = 0; i < arrayLuck.Length; i++)
				{
					luckMax += arrayLuck[i];
				}
			}
		}
		
		public EventInt eventInt;
		public void Call()
		{
			eventInt.Invoke(GetRandom());
		}
	}
}
#endif