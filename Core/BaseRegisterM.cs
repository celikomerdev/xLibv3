#if xLibv3
using System;
using UnityEngine;

namespace xLib
{
	public abstract class BaseRegisterM : BaseActiveM
	{
		#region Object
		[Serializable]
		public class BaseRegisterInfo
		{
			public int order;
			public bool onRegister;
			internal bool isRegister;
		}
		public BaseRegisterInfo baseRegister = new BaseRegisterInfo();
		
		private static int order;
		public static int Order
		{
			get
			{
				return order;
			}
			internal set
			{
				if(order == value) return;
				order = value;
			}
		}
		#endregion
		
		
		#region Custom
		protected override void OnActive(bool value)
		{
			IsRegister = value;
		}
		
		public bool IsRegister
		{
			get
			{
				return baseRegister.isRegister;
			}
			set
			{
				if(baseRegister.isRegister == value) return;
				ApplyViewIdWithDebug();
				
				if(CanDebug && BaseRegisterM.Order!=Order) Debug.LogWarningFormat("BaseRegisterM:Order:{0}:order:{1}",BaseRegisterM.Order,baseRegister.order);
				BaseRegisterM.Order = baseRegister.order;
				
				baseRegister.isRegister = OnRegister(value);
				ApplyLastIdWithDebug();
			}
		}
		
		protected virtual bool OnRegister(bool value)
		{
			return value;
		}
		#endregion
		
		
		#region Public
		public void ForceRegister()
		{
			IsRegister = false;
			IsRegister = true;
		}
		
		public void ForceExecute()
		{
			IsRegister = !baseRegister.isRegister;
			IsRegister = !baseRegister.isRegister;
		}
		#endregion
		
		
		#if UNITY_EDITOR
		public override void CheckErrors()
		{
			base.CheckErrors();
			if(Order != baseRegister.order) xDebug.LogExceptionFormat(this,this.name+":Order:{0}:order:{1}",Order,baseRegister.order);
		}
		#endif
		
		protected override void OnDestroyed()
		{
			IsRegister = false;
		}
	}
}
#endif