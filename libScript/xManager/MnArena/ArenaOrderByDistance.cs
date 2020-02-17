#if xLibv2
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace xLib.xNew
{
	public class ArenaOrderByDistance : BaseTickM
	{
		private List<View> sorting;
		[SerializeField]private NodeTransform avatarNull;
		[SerializeField]private NodeTransform avatarFirst;
		[SerializeField]private NodeTransform avatarLast;
		
		protected override void Tick(float tickTime)
		{
			Work();
		}
		
		private void Work()
		{
			//TODO stop working on leave room
			sorting = MnArena.ins.onlineAvatars.ToList();
			sorting.RemoveAll(avatar => avatar == null);
			sorting = sorting.OrderByDescending(avatar => avatar.transform.localPosition.z).ToList();
			//sorting = MnArena.ins.onlineAvatars.OrderByDescending(avatar => avatar.transform.localPosition.z).ToList();
			
			if(sorting.Count>0)
			{
				avatarFirst.Value = sorting.First().transform;
				avatarLast.Value = sorting.Last().transform;
			}
			else
			{
				avatarFirst.Value = avatarNull.Value;
				avatarLast.Value = avatarNull.Value;
			}
		}
	}
}
#endif