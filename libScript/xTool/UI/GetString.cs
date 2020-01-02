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
				eventResult.Invoke(value);
			}
		}
		
		public void FromText()
		{
			Value = target.GetComponent<UnityEngine.UI.Text>().text;
		}
		
		public void FromTextMesh()
		{
			Value = target.GetComponent<TMPro.TMP_Text>().text;
		}
		
		public void FromLocalizeI2()
		{
			Value = I2.Loc.Localize.MainTranslation;
		}
	}
}
#endif