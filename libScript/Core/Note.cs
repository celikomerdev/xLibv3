#if xLibv3
using System;
using UnityEngine;

namespace xLib
{
	[Serializable]public class Note
	{
		[TextArea(10,15)]
		[SerializeField]private string value;
	}
}
#endif