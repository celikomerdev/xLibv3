#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib
{
	public class MnApplication : SingletonM<MnApplication>
	{
		[Header("Application")]
		[SerializeField]private NodeString Application_buildGUID = null;
		[SerializeField]private NodeString Application_genuine = null;
		[SerializeField]private NodeString Application_identifier = null;
		[SerializeField]private NodeString Application_installerName = null;
		[SerializeField]private NodeString Application_installMode = null;
		[SerializeField]private NodeString Application_platform = null;
		[SerializeField]private NodeString Application_productName = null;
		[SerializeField]private NodeString Application_sandboxType = null;
		[SerializeField]private NodeInt Application_version = null;
		
		[Header("SystemInfo")]
		[SerializeField]private NodeString SystemInfo_deviceUniqueIdentifier = null;
		
		protected override void Awaked()
		{
			Application.lowMemory += LowMemoryCallback;
			
			Application_buildGUID.Value = Application.buildGUID;
			Application_genuine.Value = Application.genuine.ToString();
			Application_identifier.Value = Application.identifier;
			Application_installerName.Value = Application.installerName;
			Application_installMode.Value = Application.installMode.ToString();
			Application_platform.Value = Application.platform.ToString();
			Application_productName.Value = Application.productName;
			Application_sandboxType.Value = Application.sandboxType.ToString();
			Application_version.Value = int.Parse(Application.version);
			
			SystemInfo_deviceUniqueIdentifier.Value = SystemInfo.deviceUniqueIdentifier;
		}
		
		protected override void OnDestroyed()
		{
			Application.lowMemory -= LowMemoryCallback;
		}
		
		private void LowMemoryCallback()
		{
			Debug.LogWarning($"{this.name}:LowMemoryCallback",this);
		}
	}
}
#endif