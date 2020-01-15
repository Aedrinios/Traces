using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class WindowTool : EditorWindow 
{
	public Material matSliceable;

	//création de la fenetre
	[MenuItem("Window/MyTools")]
	public static void ShowWindow()
	{
		GetWindow<WindowTool>("MyTools");
	}

	//rajout des fonctionalités
	private void OnGUI()
	{
		GUILayout.Label("Matériaux à appliquer à tous les objets Sliable", EditorStyles.boldLabel);

		// manière spéciale de mettre ce qu'on veut dans la fenetre
		matSliceable = (Material)EditorGUILayout.ObjectField("material", matSliceable, typeof(Material));
		if (GUILayout.Button("Change Slice Mat"))
		{
			ChangeMaterial(); 
		}

		GUILayout.Label("\n Point faible trop fort", EditorStyles.boldLabel);
	}

	void ChangeMaterial()
	{
		if (matSliceable != null)
		{
			SliceableObject[] allSliceableObject = FindObjectsOfType<SliceableObject>();

			for (int i = 0; i < allSliceableObject.Length; i++)
			{
				allSliceableObject[i].crossMaterial = matSliceable;
			}
		}

	}




}
