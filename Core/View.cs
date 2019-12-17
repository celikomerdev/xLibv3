#if xLibv3
namespace xLib
{
	public abstract class View : BaseM
	{
		protected virtual void Awaked(){}
		internal virtual void Awake()
		{
			if(Id == "NotSet") gameObject.SetActive(false);
			Awaked();
		}
		
		#region Info
		// protected bool isMy = true;
		public bool IsMy
		{
			get
			{
				return isMy;
			}
		}
		
		public abstract string Id
		{
			get;
			set;
		}
		
		internal virtual void FindId(){}
		#endregion
	}
}
#endif