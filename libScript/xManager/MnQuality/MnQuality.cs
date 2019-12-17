#if xLibv2
using System;
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnQuality : SingletonM<MnQuality>
	{
		#region Field
		[Header("Nodes")]
		public NodeInt qualityAuto;
		
		[SerializeField]private NodeInt qualitySetting;
		private int QualitySetting
		{
			get
			{
				return qualitySetting.Value;
			}
			set
			{
				if(value==qualityAuto.Value) qualitySetting.Value = -1;
				else qualitySetting.Value = value;
			}
		}
		
		public NodeInt qualityLevel;
		public int QualityLevel
		{
			get
			{
				qualityLevel.Value = QualitySettings.GetQualityLevel();
				return qualityLevel.Value;
			}
			set
			{
				if(value == -1) value = qualityAuto.Value;
				if(value==QualityLevel) return;
				
				QualitySettings.SetQualityLevel(value,applyExpensive);
				qualityLevel.Value = value;
				QualitySetting = value;
			}
		}
		
		[Header("System Default")]
		public bool neverSleep = true;
		public int targetFrameRate = 30;
		public bool applyExpensive = true;
		public SystemDefault systemDefault;
		public ScoreSystem scoreSystem;
		public ScorePlatform scorePlatform;
		public float[] scoreQuality;
		#endregion
		
		
		#region Mono
		protected override void Awaked()
		{
			Init();
		}
		#endregion
		
		
		#region Setup
		public override void Init()
		{
			base.Init();
			if(neverSleep) Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Application.targetFrameRate = targetFrameRate;
			
			CalculateScoreSystem();
			ValueAuto();
			
			QualityLevel = qualitySetting.Value;
			
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
			
			scoreSystem.hardware = (scoreSystem.processorCount + scoreSystem.systemMemorySize + scoreSystem.graphicsMemorySize + scoreSystem.graphicsShaderLevel) / 4f;
			scoreSystem.system = (scoreSystem.processorCount + scoreSystem.systemMemorySize + scoreSystem.graphicsMemorySize + scoreSystem.graphicsShaderLevel + scoreSystem.screen) / 5f;
			scoreSystem.final = scoreSystem.hardware;
			scoreSystem.final *= scorePlatform.score;
		}
		
		private void ValueAuto()
		{
			int i = 0;
			for (i = 0; i < scoreQuality.Length-1; i++)
			{
				if(scoreSystem.final < scoreQuality[i]) break;
			}
			qualityAuto.Value = i;
		}
		
		private void TryDebug()
		{
			if(!CanDebug) return;
			Debug.LogFormat(this,this.name+":TryDebug");
			Debug.LogFormat(this,"SystemInfo.processorCount:{0}",SystemInfo.processorCount);
			Debug.LogFormat(this,"SystemInfo.systemMemorySize:{0}",SystemInfo.systemMemorySize);
			Debug.LogFormat(this,"SystemInfo.graphicsMemorySize:{0}",SystemInfo.graphicsMemorySize);
			Debug.LogFormat(this,"SystemInfo.graphicsShaderLevel:{0}",SystemInfo.graphicsShaderLevel);
			Debug.LogFormat(this,"Screen.width:{0}",Screen.width);
			Debug.LogFormat(this,"Screen.height:{0}",Screen.height);
			Debug.LogFormat(this,"Screen.dpi:{0}",Screen.dpi);
			
			Debug.LogFormat(this,"scoreSystem.processorCount:{0}",scoreSystem.processorCount);
			Debug.LogFormat(this,"scoreSystem.systemMemorySize:{0}",scoreSystem.systemMemorySize);
			Debug.LogFormat(this,"scoreSystem.graphicsMemorySize:{0}",scoreSystem.graphicsMemorySize);
			Debug.LogFormat(this,"scoreSystem.graphicsShaderLevel:{0}",scoreSystem.graphicsShaderLevel);
			Debug.LogFormat(this,"scoreSystem.screen:{0}",scoreSystem.screen);
			
			Debug.LogFormat(this,"scoreSystem.hardware:{0}",scoreSystem.hardware);
			Debug.LogFormat(this,"scoreSystem.system:{0}",scoreSystem.system);
			Debug.LogFormat(this,"valueAuto:{0}",qualityAuto.Value);
			Debug.LogFormat(this,"graphicsDeviceType:{0}",SystemInfo.graphicsDeviceType);
		}
		#endregion
		
		
		#region Class
		[Serializable]public class SystemDefault
		{
			public float scoreMax = 1.25f;
			public float processorCount = 4;
			public float systemMemorySize = 2048;
			public float graphicsMemorySize = 1024;
			public float graphicsShaderLevel = 30;
			public Vector2 screen = new Vector2(1600,900);
		}
		
		[Serializable]public class ScoreSystem
		{
			public float processorCount;
			public float systemMemorySize;
			public float graphicsMemorySize;
			public float graphicsShaderLevel;
			public float screen;
			public float hardware;
			public float system;
			public float final;
		}
		
		[Serializable]public class ScorePlatform
		{
			#pragma warning disable
			[SerializeField]private float scoreAndroid = 0.5f;
			[SerializeField]private float scoreIOS = 1f;
			[SerializeField]private float scoreDefault = 1f;
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