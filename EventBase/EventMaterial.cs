﻿#if xLibv3
using System;
using UnityEngine;
using UnityEngine.Events;

namespace xLib.EventBase
{
	[Serializable]
	public class EventMaterial : UnityEvent<Material>{}
}
#endif