#if xLibv3
using UnityEngine;

namespace xLib.xTween
{
	public class TweenParticleSimulationSpeed : Tween
	{
		[SerializeField]private ParticleSystem target;
		[SerializeField]private ParticleSystem.MainModule module;
		[SerializeField]private float from;
		[SerializeField]private float to;
		
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