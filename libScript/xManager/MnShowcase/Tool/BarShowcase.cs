#if xLibv2
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolShowcase
{
	public class BarShowcase : BaseM
	{
		[SerializeField]private Transform container;
		[SerializeField]private ItemShowcaseM prefab;
		
		public void Setup()
		{
			Clean();
			Fill();
		}
		
		public void Clean()
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				GameObject.Destroy(buttons[i].gameObject);
			}
		}
		
		private List<ItemShowcaseM> buttons = new List<ItemShowcaseM>();
		public void Fill()
		{
			buttons = new List<ItemShowcaseM>();
			for (int i = 0; i < MnShowcase.ins.itemShowcase.Count; i++)
			{
				ItemShowcaseM itemShowcase = Instantiate(prefab.gameObject).GetComponent<ItemShowcaseM>();
				buttons.Add(itemShowcase);
				itemShowcase.transform.SetParent(container);
				itemShowcase.transform.ResetTransform();
				itemShowcase.index = i;
				itemShowcase.Setup();
			}
		}
	}
}
#endif