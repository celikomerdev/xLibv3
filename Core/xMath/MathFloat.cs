#if xLibv2
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathFloat
	{
		public static bool IsClamp(ref float value, float min = -1, float max = 1)
		{
			bool returnValue = false;
			if(value<min)
			{
				returnValue = true;
				value = min;
			}
			else if(value>max)
			{
				returnValue = true;
				value = max;
			}
			return returnValue;
		}
		
		public static float Step(float input,float step)
		{
			return Mathf.Round(input/step)*step;
		}
		
		#region Mod
		public static float Repeat(float value,float lenght)
		{
			return Mathf.Repeat(value,lenght);
			//while (value >= max) value -= max;
			//while (value < 0) value += max;
			
			//value %= max;
			//if(value < 0) value += max;
			//return value;
		}
		#endregion
		
		#region Normalize
		public static float Normalize(float minOutput,float medOutput,float maxOutput,float minInput,float medInput,float maxInput,float input)
		{
			if(input==medInput) return medOutput;
			else
			{
				if(input>medInput)
				{
					minInput=medInput;
					minOutput=medOutput;
				}
				else
				{
					maxInput=medInput;
					maxOutput=medOutput;
				}
				return Normalize(minOutput,maxOutput,minInput,maxInput,input);
			}
		}
		
		public static float Normalize(float minOutput,float maxOutput,float minInput,float maxInput,float input)
		{
			return Mathf.Lerp(minOutput,maxOutput,Normalize(minInput,maxInput,input));
		}
		
		public static float Normalize(float minInput,float maxInput,float input)
		{
			//return (input-minInput)/(maxInput-minInput);
			return Mathf.InverseLerp(minInput,maxInput,input);
		}
		
		public static float Normalize01(float minInput,float maxInput,float input)
		{
			return Mathf.Clamp01(Normalize(minInput,maxInput,input));
		}
		#endregion
	}
}
#endif