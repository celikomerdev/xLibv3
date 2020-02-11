#if xLibv3
using UnityEngine;

namespace xLib
{
	public class xMicrophone : BaseWorkM
	{
		[Header("Input")]
		[SerializeField]private bool useMax = false;
		[SerializeField]private int countSample = 128;
		
		[Header("Output")]
		private bool isRecording = false;
		private float valueMax = 0;
		private float valueAverage = 0;
		[System.NonSerialized]public float valueOutput = 0;
		
		
		#region Mono
		private void OnEnable()
		{
			Init(true);
		}
		
		#if xMicrophone
		private void FixedUpdate()
		{
			if(!isInit) return;
			
			offsetSample = Microphone.GetPosition(null)-(countSample+1);
			if(offsetSample < 0) return;
			
			//dataSample = new float[countSample];
			audioClip.GetData(dataSample,offsetSample);
			
			Calculate();
			CheckState();
		}
		#endif
		
		private void OnApplicationFocus(bool value)
		{
			//Init(value);
		}
		
		private void OnDisable()
		{
			Init(false);
		}
		#endregion
		
		
		#region Private
		private bool isInit;
		private void Init(bool value)
		{
			dataSample = new float[countSample];
			
			#if xMicrophone
			if(isInit == value) return;
			isInit = value;
			
			if(value)
			{
				audioClip = Microphone.Start(Microphone.devices[0],true,999,44100);
				SpeakerFix.Fix();
			}
			else
			{
				Microphone.End(Microphone.devices[0]);
				valueMax = 0;
				valueAverage = 0;
				valueOutput = 0;
			}
			#endif
		}
		
		
		private AudioClip audioClip;
		private int offsetSample;
		private float[] dataSample;
		private void Calculate()
		{
			valueMax = 0;
			valueAverage = 0;
			
			float valueWave = 0;
			float valueTotal = 0;
			for(int i = 0; i < countSample; i++)
			{
				valueWave = dataSample[i] * dataSample[i];
				valueTotal += valueWave;
				if(valueWave > valueMax) valueMax = valueWave;
			}
			
			valueAverage = valueTotal / countSample;
			
			valueOutput = valueAverage;
			if(useMax) valueOutput = valueMax;
		}
		
		private float timer;
		private void CheckState()
		{
			timer += Time.unscaledDeltaTime;
			isRecording = timer < 5;
			if(valueMax>0) timer = 0;
		}
		#endregion
	}
}
#endif