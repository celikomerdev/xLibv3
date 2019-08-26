#if xLibv3
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using xLib.EventClass;

namespace xLib
{
	public class TransformTutorial : BaseRegisterM
	{
		public string[] messageString;
		
		
		protected override bool OnRegister(bool value)
		{
			MnTutorial.ins.onTutorialClick.Listener(value,Void=>OnClick());
			
			if(value)
			{
				MnTutorial.ins.tutorialTransformPoint.Value = transform;
				
				queue = new Queue<UnityAction>();
				for (int i = 0; i < messageString.Length; i++)
				{
					string message = messageString[i];
					queue.Enqueue(delegate
					{
						MnTutorial.ins.tutorialTransformPoint.Value = transform;
						ShowMessage(message);
					});
				}
				queue.Enqueue(OnComplete);
			}
			
			return value;
		}
		
		private static Queue<UnityAction> queue = new Queue<UnityAction>();
		private void OnClick()
		{
			queue.Dequeue().Invoke();
		}
		
		private void ShowMessage(string value)
		{
			Debug.LogFormat("ShowMessage:{0}",value);
			MnTutorial.ins.nodeMessageString.Value = value;
		}
		
		public EventUnity eventComplete;
		private void OnComplete()
		{
			gameObject.SetActive(false);
			eventComplete.Invoke();
		}
	}
}
#endif