#if xLibv2
#if Firebase
using System.Threading.Tasks;
using Firebase;
using UnityEngine;

namespace xLib
{
	public class MnFirebase : SingletonM<MnFirebase>
	{
		protected override void Started()
		{
			Init();
		}
		
		public override void Init()
		{
			base.Init();
			FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(OnCheck);
		}
		
		public static bool isActive;
		private void OnCheck(Task<DependencyStatus> task)
		{
			DependencyStatus dependencyStatus = task.Result;
			if(CanDebug) Debug.LogFormat(this,this.name+":dependencyStatus:{0}",dependencyStatus);
			
			if (dependencyStatus == Firebase.DependencyStatus.Available)
			{
				isActive = true;
				// FirebaseApp firebaseApp = Firebase.FirebaseApp.DefaultInstance;
				Firebase.Analytics.FirebaseAnalytics.LogEvent("Analytics");
			}
			else
			{
				xDebug.LogExceptionFormat(this,this.name+":dependencyStatus:{0}",dependencyStatus);
			}
		}
	}
}
#else
using UnityEngine;

namespace xLib
{
	public class MnFirebase : SingletonM<MnFirebase>
	{
		public static bool isActive;
	}
}
#endif
#endif