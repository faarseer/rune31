using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Elements {
	None = 0,
	Fire = 1 << 0,
	Ice = 1 << 1,
	Lightning = 1 << 2,
	Earth = 1 << 3
}
