#if xLibv3
using System;
using UnityEngine;
using xLib.xValueClass;

namespace xLib
{
	public class MnQuality : SingletonM<MnQuality>
	{
		#region Field
		[Header("Nodes")]
		[SerializeField]private NodeInt qualityAuto = null;
		[SerializeField]private NodeInt qualitySetting = null;
		[SerializeField]private NodeInt qualityLevel = null;
		public int QualityLevel
		{
			set
			{
				if(value == -1) value = qualityAuto.Value;
				
				if(value == qualityAuto.Value) qualitySetting.Value = -1;
				else qualitySetting.Value = value;
				
				qualityLevel.Value = value;
			}
		}
		
		[Header("System Default")]
		private readonly ScoreSystem scoreSystem = new ScoreSystem();
		[SerializeField]private SystemDefault systemDefault = new SystemDefault();
		[SerializeField]private ScorePlatform scorePlatform = new ScorePlatform();
		[SerializeField]private float[] scoreQuality = new float[0];
		#endregion
		
		
		#region Mono
		protected override void Awaked()
		{
			Init();
		}
		#endregion
		
		
		#region Setup
		protected override void Inited()
		{
			CalculateScoreSystem();
			ValueAuto();
			TryDebug();
		}
		
		private void CalculateScoreSystem()
		{
			scoreSystem.processorCount = SystemInfo.processorCount / systemDefault.processorCount;
			scoreSystem.systemMemorySize = SystemInfo.systemMemorySize / systemDefault.systemMemorySize;
			scoreSystem.graphicsMemorySize = SystemInfo.graphicsMemorySize / systemDefault.graphicsMemorySize;
			scoreSystem.graphicsShaderLevel = SystemInfo.graphicsShaderLevel / systemDefault.graphicsShaderLevel;
			scoreSystem.screen = (systemDefault.screen.x*systemDefault.screen.y) / (Screen.width*Screen.height);
			
			scoreSystem.processorCount = Mathf.Clamp(scoreSystem.processorCount, 0, systemDefault.scoreMax);
			scoreSystem.systemMemorySize = Mathf.Clamp(scoreSystem.systemMemorySize, 0, systemDefault.scoreMax);
			scoreSystem.graphicsMemorySize = Mathf.Clamp(scoreSystem.graphicsMemorySize, 0, systemDefault.scoreMax);
			scoreSystem.graphicsShaderLevel = Mathf.Clamp(scoreSystem.graphicsShaderLevel, 0, systemDefault.scoreMax);
			scoreSystem.screen = Mathf.Clamp(scoreSystem.screen, 0, systemDefault.scoreMax);
			
			scoreSystem.score = scorePlatform.score;
			scoreSystem.score += scoreSystem.processorCount;
			scoreSystem.score += scoreSystem.systemMemorySize;
			scoreSystem.score += scoreSystem.graphicsMemorySize;
			scoreSystem.score += scoreSystem.graphicsShaderLevel;
			// scoreSystem.score += scoreSystem.screen;
			scoreSystem.score /= 5;
		}
		
		private void ValueAuto()
		{
			int output = 0;
			for(int i = 0; i<scoreQuality.Length; i++)
			{
				if(scoreSystem.score<scoreQuality[i]) break;
				output = i;
			}
			qualityAuto.Value = output;
		}
		
		private void TryDebug()
		{
			if(!CanDebug) return;
			Debug.LogFormat($"{this.name}:SystemInfo.processorCount:{SystemInfo.processorCount}",this);
			Debug.LogFormat($"{this.name}:SystemInfo.systemMemorySize:{SystemInfo.systemMemorySize}",this);
			Debug.LogFormat($"{this.name}:SystemInfo.graphicsMemorySize:{SystemInfo.graphicsMemorySize}",this);
			Debug.LogFormat($"{this.name}:SystemInfo.graphicsShaderLevel:{SystemInfo.graphicsShaderLevel}",this);
			Debug.LogFormat($"{this.name}:Screen.width:{Screen.width}",this);
			Debug.LogFormat($"{this.name}:Screen.height:{Screen.height}",this);
			Debug.LogFormat($"{this.name}:Screen.dpi:{Screen.dpi}",this);
			
			Debug.LogFormat($"{this.name}:scoreSystem.processorCount:{scoreSystem.processorCount}",this);
			Debug.LogFormat($"{this.name}:scoreSystem.systemMemorySize:{scoreSystem.systemMemorySize}",this);
			Debug.LogFormat($"{this.name}:scoreSystem.graphicsMemorySize:{scoreSystem.graphicsMemorySize}",this);
			Debug.LogFormat($"{this.name}:scoreSystem.graphicsShaderLevel:{scoreSystem.graphicsShaderLevel}",this);
			Debug.LogFormat($"{this.name}:scoreSystem.screen:{scoreSystem.screen}");
			
			Debug.LogFormat($"{this.name}:scoreSystem.score:{scoreSystem.score}",this);
			Debug.LogFormat($"{this.name}:valueAuto:{qualityAuto.Value}",this);
			Debug.LogFormat($"{this.name}:graphicsDeviceType:{SystemInfo.graphicsDeviceType}",this);
		}
		#endregion
		
		
		#region Class
		[Serializable]internal class SystemDefault
		{
			internal float scoreMax = 1.25f;
			[SerializeField]internal float processorCount = 4;
			[SerializeField]internal float systemMemorySize = 2048;
			[SerializeField]internal float graphicsMemorySize = 1024;
			[SerializeField]internal float graphicsShaderLevel = 30;
			[SerializeField]internal Vector2 screen = new Vector2(1600,900);
		}
		
		[Serializable]internal class ScoreSystem
		{
			[SerializeField]internal float processorCount;
			[SerializeField]internal float systemMemorySize;
			[SerializeField]internal float graphicsMemorySize;
			[SerializeField]internal float graphicsShaderLevel;
			[SerializeField]internal float screen;
			[SerializeField]internal float score;
		}
		
		[Serializable]internal class ScorePlatform
		{
			#pragma warning disable
			[SerializeField]private float scoreAndroid = 1.0f;
			[SerializeField]private float scoreIOS = 1.5f;
			[SerializeField]private float scoreDefault = 1.0f;
			#pragma warning restore
			
			public float score
			{
				get
				{
					#if UNITY_ANDROID
					return scoreAndroid;
					#elif UNITY_IOS
					return scoreIOS;
					#else
					return scoreDefault;
					#endif
				}
			}
		}
		#endregion
	}
}
#endif