#if xLibv2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xLib.ToolShowcase;
using xLib.ToolWWW;

namespace xLib
{
	public class MnShowcase : SingletonM<MnShowcase>
	{
		public string key = "MnShowcase";
		private string value;
		
		#region Init
		public override void Init()
		{
			base.Init();
			value = MnKey.GetValue(key);
			MnCoroutine.ins.NewCoroutine(eLoad());
		}
		#endregion
		
		#region Load
		private IEnumerator eLoad()
		{
			yield return new WaitForSeconds(3);
			if(string.IsNullOrEmpty(value)) yield break;
			
			WWW www = new WWW(value);
			yield return www;
			
			if (string.IsNullOrEmpty(www.error))
			{
				OnLoad(www);
			}
			else
			{
				xDebug.LogExceptionFormat(this,this.name+":eLoad:error:{0}:{1}",www.error,value);
			}
			www.Dispose();
			www = null;
			yield return new WaitForEndOfFrame();
		}
		#endregion
		
		#region Process
		public NodeBool showcaseLoaded;
		public List<ItemShowcase> itemShowcase = new List<ItemShowcase>();
		
		private void OnLoad(WWW www)
		{
			xSimpleJSON.JSONNode mnShowcase = xSimpleJSON.JSON.Parse(www.text);
			xSimpleJSON.JSONNode infoShowcase = mnShowcase["infoShowcase"];
			
			string thisNameShort = MnKey.GetValue("App_NameShort");
			
			for (int indexShowcase = 0; indexShowcase < infoShowcase.Count; indexShowcase++)
			{
				ItemShowcase newItemShowcase = new ItemShowcase();
				newItemShowcase.nameShort = infoShowcase[indexShowcase]["nameShort"];
				if(newItemShowcase.nameShort == thisNameShort) continue;
				
				newItemShowcase.name = infoShowcase[indexShowcase]["name"];
				newItemShowcase.link = infoShowcase[indexShowcase]["link"];
				
				xSimpleJSON.JSONNode arrayImage = infoShowcase[indexShowcase]["arrayImage"];
				for (int indexImage = 0; indexImage < arrayImage.Count; indexImage++)
				{
					LoadTexture loadTexture = new LoadTexture();
					newItemShowcase.arrayImage.Add(loadTexture);
					
					loadTexture.value = arrayImage[indexImage];
					loadTexture.eventTexture.eventTexture.AddListener(newItemShowcase.OnRefresh);
					MnCoroutine.ins.NewCoroutine(loadTexture.eLoad());
				}
				
				itemShowcase.Add(newItemShowcase);
			}
			
			showcaseLoaded.Value = true;
		}
		#endregion
	}
}
#endif