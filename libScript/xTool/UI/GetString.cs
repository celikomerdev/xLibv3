#if xLibv3
using UnityEngine;
using xLib.EventClass;

namespace xLib.ToolConvert
{
	public class GetString : BaseWorkM
	{
		[SerializeField]private Transform target = null;
		[SerializeField]private EventString eventResult = new EventString();
		
		private string Value
		{
			set
			{
				if(CanDebug) xLogger.LogFormat(this,this.name+":GetString:Value:{0}",value);
				if(string.IsNullOrWhiteSpace(value)) return;
				eventResult.Invoke(value);
			}
		}
		
		public void FromText()
		{
			#if PackUI
			Value = target.GetComponent<UnityEngine.UI.Text>().text;
			#endif
		}
		
		public void FromTextMesh()
		{
			#if TextMeshPro
			Value = target.GetComponent<TMPro.TMP_Text>().text;
			#endif
		}
		
		public void FromLocalizeI2()
		{
			#if I2Loc
			Value = I2.Loc.Localize.MainTranslation;
			#endif
		}
	}
}
#endif