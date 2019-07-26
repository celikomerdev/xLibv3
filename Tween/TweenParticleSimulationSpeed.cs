#if xLibv2
using UnityEngine;

namespace xLib.ToolTween
{
	public class TweenParticleSimulationSpeed : Tween
	{
		public ParticleSystem target;
		private ParticleSystem.MainModule module;
		public float from;
		public float to;
		
		public override void Awake()
		{
			module = target.main;
		}
		
		override protected void SetRatio(float value)
		{
			module.simulationSpeed = Mathf.LerpUnclamped(from,to,value);
		}
	}
}
#endif