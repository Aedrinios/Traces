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
	public float radiusCircle = 1; 
	
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

		GUILayout.Label("\n Les paramètres pour placer les objets en rond", EditorStyles.boldLabel);

		radiusCircle = EditorGUILayout.FloatField("Le rayon du cercle est : ", radiusCircle); 
		if (GUILayout.Button("Placer les objets sur un cercle"))
		{
			PlaceOnCircle(radiusCircle); 
		}

	}

	void ChangeLighting()
	{
		RenderSettings.skybox = null;
		RenderSettings.sun = null;
		//RenderSettings.ambientSkyColor = new Color(54, 58, 66); 
	}

	void PlaceOnCircle(float radius)
	{
		GameObject[] selectionObject = Selection.gameObjects;
		if (selectionObject.Length > 1)
		{
			for (int i = 0; i < selectionObject.Length; i++)
			{
				float angle = 6.283f / (selectionObject.Length);
				float angle_x = Mathf.Cos(i * angle);
				float angle_y = Mathf.Sin(i * angle);

				Vector3 dir = new Vector3(angle_x, angle_y, 0);
				selectionObject[i].transform.localPosition = Vector3.zero;
				selectionObject[i].transform.localPosition = dir * radius;

				float tourne = 360 / (selectionObject.Length) * i;
				selectionObject[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, tourne + 90));
			}
		}
		else
		{
			Debug.Log("Pas assez d'objets sélectionnés"); 
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
