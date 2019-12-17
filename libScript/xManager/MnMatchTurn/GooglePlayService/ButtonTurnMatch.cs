#if xLibv1
#if GooglePlayService
using UnityEngine;

namespace xLib.Google.PlayGames.MultiTurn
{
	public class ButtonTurnMatch : BaseM
	{
		public uint variant = 1;
		public uint minOpponent = 1;
		public uint maxOpponent = 1;
		
		public void OnClick()
		{
			MnTurnMatch.ins.variant = variant;
			MnTurnMatch.CreateNewMatch();
		}
	}
}
#endif
#endif