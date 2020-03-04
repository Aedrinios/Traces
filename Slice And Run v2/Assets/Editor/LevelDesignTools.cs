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


	public GameObject[] essentialsGameObjects;

	SaveScriptableObject data; 
	
	//rajout des fonctionalités
	private void OnGUI()
	{
		GUILayout.Label("Fenetre pour aider le Level Design", EditorStyles.boldLabel);

		SerializedObject so = new SerializedObject(this);
		EditorGUILayout.PropertyField(so.FindProperty("essentialsGameObjects"), true);
		so.ApplyModifiedProperties();

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
		}
	}

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


}
