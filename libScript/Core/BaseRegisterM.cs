#if xLibv3
namespace xLib
{
	public abstract class BaseRegisterM : BaseActiveM
	{
		#region Object
		[System.Serializable]
		public class BaseRegisterInfo
		{
			public int order;
			public bool onRegister;
			internal bool isRegister;
		}
		public BaseRegisterInfo baseRegister = new BaseRegisterInfo();
		
		public static int Order;
		#if CanDebug
		public static UnityEngine.Object OrderObject;
		#endif
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
				
				BaseRegisterM.Order = baseRegister.order;
				#if CanDebug
				BaseRegisterM.OrderObject = this;
				#endif
				
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