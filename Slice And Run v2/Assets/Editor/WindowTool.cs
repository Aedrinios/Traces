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
		if (GUILayout.Button("Change Slice Mat")) ChangeMaterial();

		GUILayout.Label("Mettre le script ReactionAtSlice à tous les objets de la scène", EditorStyles.boldLabel);
		if (GUILayout.Button("Mettre le script ReactionAtSlice")) PutReaction();



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

	void PutReaction()
	{
		//List<GameObject> allSliceableObject = new List<GameObject>(); 
		SliceableObject[] allSliceable = FindObjectsOfType<SliceableObject>();

		for (int i = 0; i < allSliceable.Length; i++)
		{
			if (allSliceable[i].gameObject.GetComponent<ReactionAtSlice>() == false) 
			{
				allSliceable[i].gameObject.AddComponent<ReactionAtSlice>();
				Debug.Log(allSliceable[i].gameObject.name); 
			} 
		}
		Debug.Log("is done"); 
	}





}
