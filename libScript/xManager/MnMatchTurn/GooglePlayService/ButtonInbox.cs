#if xLibv1
#if GooglePlayService
using UnityEngine;

namespace xLib.Google.PlayGames.MultiTurn
{
	public class ButtonInbox : BaseM
	{
		public void OnClick()
		{
			MnTurnMatch.AcceptFromInbox();
		}
	}
}
#endif
#endif