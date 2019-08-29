#if xLibv3
using UnityEngine;
using UnityEngine.UI;
using xLib.xNode.NodeObject;
using xLib.xTweener;

namespace xLib
{
	public class MnPopupWindow : SingletonM<MnPopupWindow>
	{
		[SerializeField]private Tweener window;
		
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
		[SerializeField]private Text header;
		public void Header(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Header:{0}",value);
			header.text = value;
		}
		
		public void HeaderLocalized(string value)
		{
			Header(MnLocalize.GetValue(value));
		}
		#endregion
		
		#region Body
		[SerializeField]private Text body;
		public void Body(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Body:{0}",value);
			body.text = value;
		}
		
		public void BodyLocalized(string value)
		{
			Body(MnLocalize.GetValue(value));
		}
		#endregion
		
		#region Accept
		[SerializeField]private Text accept;
		[SerializeField]private GameObject objAccept;
		public void Accept(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Accept:{0}",value);
			objAccept.SetActive(!string.IsNullOrEmpty(value));
			accept.text = value;
		}
		
		public void AcceptLocalized(string value)
		{
			Accept(MnLocalize.GetValue(value));
		}
		#endregion
		
		#region Decline
		[SerializeField]private Text decline;
		[SerializeField]private GameObject objDecline;
		public void Decline(string value)
		{
			if(CanDebug) Debug.LogFormat(this,this.name+":Decline:{0}",value);
			objDecline.SetActive(!string.IsNullOrEmpty(value));
			decline.text = value;
		}
		
		public void DeclineLocalized(string value)
		{
			Decline(MnLocalize.GetValue(value));
		}
		#endregion
	}
}
#endif