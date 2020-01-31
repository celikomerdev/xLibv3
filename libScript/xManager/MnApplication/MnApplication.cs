#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#endif

namespace xLib
{
	public class MnApplication : SingletonM<MnApplication>,
	#if UNITY_EDITOR
	IPreprocessBuildWithReport
	#endif
	{
		[Header("SystemInfo")]
		[SerializeField]private NodeString SystemInfo_deviceUniqueIdentifier = null;
		
		[Header("Application")]
		[SerializeField]private NodeString Application_buildGUID = null;
		[SerializeField]private NodeString Application_genuine = null;
		[SerializeField]private NodeString Application_identifier = null;
		[SerializeField]private NodeString Application_installerName = null;
		[SerializeField]private NodeString Application_installMode = null;
		[SerializeField]private NodeString Application_platform = null;
		[SerializeField]private NodeString Application_productName = null;
		[SerializeField]private NodeString Application_sandboxType = null;
		[SerializeField]private NodeString Application_version_name = null;
		
		[SerializeField]private int app_version = 0;
		[SerializeField]private NodeInt Application_version = null;
		
		protected override void Awaked()
		{
			SystemInfo_deviceUniqueIdentifier.Value = SystemInfo.deviceUniqueIdentifier;
			
			Application_buildGUID.Value = Application.buildGUID;
			Application_genuine.Value = Application.genuine.ToString();
			Application_identifier.Value = Application.identifier;
			Application_installerName.Value = Application.installerName;
			Application_installMode.Value = Application.installMode.ToString();
			Application_platform.Value = Application.platform.ToString();
			Application_productName.Value = Application.productName;
			Application_sandboxType.Value = Application.sandboxType.ToString();
			Application_version_name.Value = Application.version;
			
			#if UNITY_EDITOR
			app_version = PlayerSettings.Android.bundleVersionCode;
			#endif
			
			int app_version_parse = 0;
			if(int.TryParse(Application.version, out app_version_parse))
			{
				app_version = app_version_parse;
			}
			
			Application_version.Value = app_version;
		}
		
		
		#if UNITY_EDITOR
		int IOrderedCallback.callbackOrder
		{
			get
			{
				return 0;
			}
		}
		
		void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport report)
		{
			app_version = PlayerSettings.Android.bundleVersionCode;
			Debug.Log($"{this.name}:OnPreprocessBuild:app_version:{app_version}",this);
		}
		#endif
	}
}
#endif

// string[] version = Application.version.Split('.');
// var versionNumbered = Convert.ToInt32(version[0])*1000000 +Convert.ToInt32(version[1]) * 1000 + Convert.ToInt32(version[2]);