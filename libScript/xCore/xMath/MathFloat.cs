#if xLibv3
using UnityEngine;

namespace xLib.Mathx
{
	public static class MathFloat
	{
		public static float LerpSin(float a,float b,float t)
		{
			return Mathf.Lerp(a,b,Mathf.Sin(t));
		}
		
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
		
		#region Normalize
		public static float Remap(float minOutput,float medOutput,float maxOutput,float minInput,float medInput,float maxInput,float input)
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
				return Remap(minOutput,maxOutput,minInput,maxInput,input);
			}
		}
		
		public static float Remap(float minOutput,float maxOutput,float minInput,float maxInput,float input)
		{
			return Mathf.Lerp(minOutput,maxOutput,Remap(minInput,maxInput,input));
		}
		
		public static float Remap(float minInput,float maxInput,float input)
		{
			return (input-minInput)/(maxInput-minInput);
		}
		#endregion
	}
}
#endif