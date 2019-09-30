#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xValueClass
{
	[System.Serializable]
	public class ValueGroupCrypto : ValueGroup
	{
		protected string KeyEncrypt
		{
			get
			{
				return nodeSetting.Key+cryptoVersion[0].KeyEncrypt;
			}
		}
		
		protected string KeyEncryptVersion(int version)
		{
			return nodeSetting.Key+cryptoVersion[version].KeyEncrypt;
		}
		
		[Header("Crypto")]
		[SerializeField]protected CryptoVersion[] cryptoVersion = new CryptoVersion[0];
		[System.Serializable]protected class CryptoVersion
		{
			[SerializeField]private string keyEncrypt = "KeyEncrypt";
			internal string KeyEncrypt
			{
				get
				{
					return keyEncrypt+KeyExtra;
				}
			}
			
			[SerializeField]private NodeBase[] nodeEncrypt = new NodeBase[0];
			private string KeyExtra
			{
				get
				{
					string tempString = "";
					for (int i = 0; i < nodeEncrypt.Length; i++)
					{
						tempString += nodeEncrypt[i].ValueToString;
					}
					return tempString;
				}
			}
		}
	}
}
#endif