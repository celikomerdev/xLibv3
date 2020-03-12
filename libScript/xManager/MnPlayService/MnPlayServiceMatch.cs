#if xLibv3
#if GooglePlayService
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine;
using xLib.EventClass;

namespace xLib
{
	public class MnPlayServiceMatch : SingletonM<MnPlayServiceMatch>
	{
		[SerializeField]private EventUnity onTurnMatchGot = new EventUnity();
		private void OnTurnMatchGot(TurnBasedMatch value, bool shouldAutoLaunch)
		{
			if(CanDebug) Debug.Log($"{this.name}:OnTurnMatchGot:{shouldAutoLaunch}:{value}",this);
			onTurnMatchGot.Invoke();
			// MnMatchTurn.ins.shouldAutoLaunch = shouldAutoLaunch;
			// MnMatchTurn.ins.match = value;
		}
		internal static void MatchDelegate(TurnBasedMatch value, bool shouldAutoLaunch)
		{
			ins?.OnTurnMatchGot(value,shouldAutoLaunch);
		}
	}
}
#endif
#endif