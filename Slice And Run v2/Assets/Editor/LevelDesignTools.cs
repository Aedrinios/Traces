using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

public class LevelDesignTools : EditorWindow 
{
	//création de la fenetre
	[MenuItem("Window/LevelDesignTools")]
	public static void ShowWindow()
	{
		GetWindow<LevelDesignTools>("LevelDesignTools");
	}

	SaveScriptableObject data;
	public GameObject[] essentialsGameObjects;
	public Material matSliceable; 

	//rajout des fonctionalités
	private void OnGUI()
	{
		GUILayout.Label("Fenetre pour aider le Level Design", EditorStyles.boldLabel);

		// list contenant les gameobjects essentiels
		SerializedObject so = new SerializedObject(this);
		EditorGUILayout.PropertyField(so.FindProperty("essentialsGameObjects"), true);
		so.ApplyModifiedProperties();

		// bouton pour placer les objets essentiels
		if (GUILayout.Button("Mettre les objects essentiels"))
		{
			GameObject newParent = new GameObject("Essentials"); 
			for (int i = 0; i < essentialsGameObjects.Length; i++)
			{				
				GameObject newObject = PrefabUtility.InstantiatePrefab(essentialsGameObjects[i]) as GameObject;
				newObject.transform.position = newParent.transform.position;
				newObject.transform.rotation = newParent.transform.rotation;
				newObject.transform.parent = newParent.transform; 
			}
			ChangeLighting(); 
		}

		GUILayout.Label(" ", EditorStyles.boldLabel);
		matSliceable = (Material)EditorGUILayout.ObjectField("material", matSliceable, typeof(Material));
		if (GUILayout.Button("Change Slice Mat"))
		{
			ChangeMaterial();
		}
	}

	void ChangeLighting()
	{
		RenderSettings.skybox = null;
		RenderSettings.sun = null;
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
			Debug.Log("Change Material Done"); 
		}
	}


	#region
	private void OnEnable()
	{
		data = Resources.Load<SaveScriptableObject>("LevelDataData");
		essentialsGameObjects = data.saveObjects; 
	}

	private void OnDisable()
	{
		data = Resources.Load<SaveScriptableObject>("LevelDataData");
		data.saveObjects = essentialsGameObjects;
	}
	#endregion

}
