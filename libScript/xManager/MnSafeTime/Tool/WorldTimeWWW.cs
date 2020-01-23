#if xLibv2
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

namespace xLib.ToolWorldTime
{
	public class WorldTimeWWW : WorldTimeBase
	{
		[Header("WWW")]
		[SerializeField]private string regex = @"(?<=\btime="")[^""]*";
		
		#region Refresh
		public override void Refresh()
		{
			base.Refresh();
			MnCoroutine.ins.NewCoroutine(eRefresh());
		}
		
		private IEnumerator eRefresh()
		{
			WWW www = new WWW(url);
			yield return www;
			
			if(!string.IsNullOrEmpty(www.error))
			{
				xDebug.LogExceptionFormat(this,this.name+":eRefresh:error:{0}",www.error);
			}
			else
			{
				string valueString = Regex.Match(www.text,regex).Value;
				long valueLong = 0;
				
				if(!Int64.TryParse(valueString,out valueLong))
				{
					xDebug.LogExceptionFormat(this,this.name+":eRefresh:valueString:{0}",valueString);
				}
				else
				{
					long milliseconds = valueLong/1000;
					
					DateTime dateTime = dateTimeOrigin.dateTime.AddMilliseconds(milliseconds);
					
					if(CanDebug) Debug.LogFormat(this,this.name+":{0}",dateTime.ToString());
					MnWorldTime.ins.DateTimeUtc = dateTime;
				}
			}
			
			www.Dispose();
			www = null;
			yield return new WaitForEndOfFrame();
		}
		#endregion
	}
}
#endif