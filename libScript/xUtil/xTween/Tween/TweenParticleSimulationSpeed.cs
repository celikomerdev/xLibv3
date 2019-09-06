#if xLibv3
#if ModParticleSystem
using UnityEngine;

namespace xLib.xTween
{
	public class TweenParticleSimulationSpeed : Tween
	{
		[SerializeField]private ParticleSystem target = null;
		[SerializeField]private ParticleSystem.MainModule module = new ParticleSystem.MainModule();
		[SerializeField]private float from = 0;
		[SerializeField]private float to = 1;
		
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
#endif