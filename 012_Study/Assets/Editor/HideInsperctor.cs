using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(hidetest))]
[ExecuteInEditMode]

public class HideInsperctor:Editor{
		
	public override void OnInspectorGUI()
	{					 
		hidetest test = (hidetest)target;

		test.name = EditorGUILayout.TextField("名字", test.name);

		test.texture = EditorGUILayout.ObjectField("贴图", test.texture, typeof(Texture), true) as Texture;
	}
}
