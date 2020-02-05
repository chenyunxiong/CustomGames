using UnityEngine;
using System.Collections;

public static class UIHelper
{
	static UIText text;
	
	public static UIText getText()
	{
		return new UIText("Optimus", "Optimus.png");
	}
}
