#if xLibv2
using UnityEngine;
using UnityEngine.UI;

namespace xLib.ToolShowcase
{
	public class ItemShowcaseM : BaseM
	{
		public int index;
		private ItemShowcase itemShowcase;
		public Text textName;
		public RawImage[] arrayImage;
		
		private void Start()
		{
			Setup();
		}
		
		public void Setup()
		{
			if(index >= MnShowcase.ins.itemShowcase.Count) return;
			itemShowcase = MnShowcase.ins.itemShowcase[index];
			itemShowcase.onRefresh.eventUnity.AddListener(OnRefresh);
			OnRefresh();
		}
		
		private void OnRefresh()
		{
			if(textName != null) textName.text = itemShowcase.name;
			
			for (int i = 0; i < arrayImage.Length; i++)
			{
				if(i >= itemShowcase.arrayImage.Count) break;
				if(itemShowcase.arrayImage[i].texture == null) return;
				arrayImage[i].texture = itemShowcase.arrayImage[i].texture;
				arrayImage[i].gameObject.SetActive(true);
			}
		}
		
		public void OnClick()
		{
			StAnalytics.LogEvent("AppClick",itemShowcase.nameShort);
			Application.OpenURL(itemShowcase.link);
		}
	}
}
#endif