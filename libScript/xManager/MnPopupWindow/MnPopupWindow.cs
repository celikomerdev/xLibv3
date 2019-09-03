#if xLibv3
using UnityEngine;
using xLib.EventClass;
using xLib.xNode.NodeObject;
using xLib.xTweener;

namespace xLib
{
	public class MnPopupWindow : SingletonM<MnPopupWindow>
	{
		[SerializeField]private Tweener window = null;
		
		#region Result
		public NodeBool result;
		public void Result(bool value)
		{
			window.enabled = false;
			result.Value = value;
		}
		#endregion
		
		
		#region Show
		public void Reset()
		{
			Header("");
			Body("");
			Accept("");
		}
		
		public void Show()
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Show");
			window.enabled = true;
		}
		#endregion
		
		
		#region Header
		[SerializeField]private EventString stringHeader = new EventString();
		public void Header(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Header:{0}",value);
			stringHeader.Invoke(value);
		}
		
		public void HeaderLocalized(string value)
		{
			Header(MnLocalize.GetValue(value));
		}
		#endregion
		
		#region Body
		[SerializeField]private EventString stringBody = new EventString();
		public void Body(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Body:{0}",value);
			stringBody.Invoke(value);
		}
		
		public void BodyLocalized(string value)
		{
			Body(MnLocalize.GetValue(value));
		}
		#endregion
		
		#region Accept
		[Header("Accept")]
		[SerializeField]private EventString stringAccept = new EventString();
		[SerializeField]private EventBool activeAccept = new EventBool();
		public void Accept(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Accept:{0}",value);
			stringAccept.Invoke(value);
			activeAccept.Invoke(!string.IsNullOrEmpty(value));
		}
		
		public void AcceptLocalized(string value)
		{
			Accept(MnLocalize.GetValue(value));
		}
		#endregion
		
		#region Decline
		[Header("Decline")]
		[SerializeField]private EventString stringDecline = new EventString();
		[SerializeField]private EventBool activeDecline = new EventBool();
		public void Decline(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Decline:{0}",value);
			stringDecline.Invoke(value);
			activeDecline.Invoke(!string.IsNullOrEmpty(value));
		}
		
		public void DeclineLocalized(string value)
		{
			Decline(MnLocalize.GetValue(value));
		}
		#endregion
	}
}
#endif