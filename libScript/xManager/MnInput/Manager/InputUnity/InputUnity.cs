#if xLibv3
using UnityEngine;
using xLib.xNode.NodeObject;

namespace xLib.xInput
{
	public class InputUnity : BaseTickNodeM
	{
		[Header("Axis")]
		[SerializeField]private NodeFloat[] axis = new NodeFloat[0];
		
		protected override void Tick(float tickTime)
		{
			for (int i = 0; i < axis.Length; i++)
			{
				axis[i].Value = Input.GetAxis(axis[i].Key);
			}
		}
	}
}
#endif