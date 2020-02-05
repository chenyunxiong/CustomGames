using UnityEngine;
using System.Collections;

public static class NumberExtensions
{
	public static string getTimeFormat(this float time)
	{
		var secs = (int)time % 60; 
		var mins = (int)time / 60;
		var hours = (int)mins / 60;
		return (hours > 0 ? hours.ToString() + ":" : "") + (mins % 60).ToString("D2") + ":" + secs.ToString("D2");
	}
}
