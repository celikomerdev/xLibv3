#if xLibv3
using UnityEngine;

namespace xLib
{
	public abstract class BaseWorkM : BaseM, IDebug
	{
		[SerializeField]private BaseWorkInfo baseWork = new BaseWorkInfo();
		
		protected virtual void SetDebug(){}
		public bool CanDebug
		{
			get
			{
				return baseWork.CanDebug;
			}
			set
			{
				if(baseWork.canDebug == value) return;
				Debug.LogFormat(this,this.name+":CanDebug:{0}",value);
				baseWork.CanDebug = value;
				SetDebug();
				
				#if UNITY_EDITOR
				UnityEditor.EditorUtility.SetDirty(this);
				#endif
			}
		}
		
		public bool CanWork
		{
			get
			{
				return baseWork.CanWork;
			}
			set
			{
				baseWork.CanWork = value;
			}
		}
		
		protected virtual void OnValidatedForced(){}
		protected virtual void OnValidated(){}
		private void OnValidate()
		{
			#if UNITY_EDITOR
			runInEditMode = baseWork.RunInEditMode;
			#endif
			
			OnValidatedForced();
			SetDebug();
			
			if(!CanWork) return;
			if(CanDebug) Debug.LogFormat(this,this.name+":OnValidate");
			OnValidated();
		}
	}
}
#endif