using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GeoTools : EditorWindow
{
	//création de la fenetre
	[MenuItem("Window/GeoTools")]
	public static void ShowWindow()
	{
		GetWindow<GeoTools>("GeoTools");
	}

	// variables
	public enum TypeForm { Circle, Spiral };
	TypeForm form;

	public float radius = 10;
	public bool withRotation = false; 

	//rajout des fonctionalités
	private void OnGUI()
	{
		GUILayout.Label("GeoTools : the Tools to order geometrically", EditorStyles.boldLabel);

		form = (TypeForm)EditorGUILayout.EnumPopup("Form selected", form);
		radius = EditorGUILayout.FloatField("Radius : ", radius);
		withRotation = EditorGUILayout.Toggle("With Rotation : ", withRotation);

		if (GUILayout.Button("Order"))
		{
			MakeSelectedForm(); 
		}
	}

	void MakeSelectedForm()
	{
		switch (form)
		{
			case TypeForm.Circle:
				MakeCircle(); 
				Debug.Log("Circle maked"); 
				break;
			case TypeForm.Spiral:
				MakeSpiral(); 
				Debug.Log("Spiral maked");
				break;
		}
	}

	void MakeCircle() 
	{
		Transform[] transformSelection = cleanSelectionTransform(); 

		for (int i = 0; i < transformSelection.Length; i++)
		{
			//position
			float angle = (Mathf.PI * 2) / (transformSelection.Length) * -i;
			angle += Mathf.PI / 2; 
			float angle_x = Mathf.Cos(angle);
			float angle_z = Mathf.Sin(angle);		

			Vector3 dir = new Vector3(angle_x, 0, angle_z);
			transformSelection[i].localPosition = dir * radius;

			//rotation
			if (withRotation)
			{
				Vector3 center;
				if (transformSelection[i].parent != null)
				{
					center = transformSelection[i].parent.position;
				}
				else
				{
					center = Vector3.zero;
				}
				transformSelection[i].LookAt(center);
			}
		}

		

	}

	void MakeSpiral()
	{
		Transform[] transformSelection = cleanSelectionTransform();

		for (int i = 0; i < transformSelection.Length; i++)
		{
			Vector3 center = Vector3.zero; 

		}

	}




	Transform[] cleanSelectionTransform()
	{
		Transform[] selectionTransform = Selection.transforms;
		List<Transform> selectionList= new List<Transform>();		
		List<Transform> result = new List<Transform>();

		for (int i = 0; i < selectionTransform.Length; i++)
		{
			selectionList.Add(selectionTransform[i]); 
		}

		for (int j = 0; j < selectionTransform.Length; j++)
		{
			int numberBest = 200;
			for (int i = 0; i < selectionList.Count; i++)
			{
				if (numberBest >= 200)
				{
					numberBest = i;
				}
				else if (selectionList[i].GetSiblingIndex() < selectionList[numberBest].GetSiblingIndex())
				{

					numberBest = i;
				}
			}
			result.Add(selectionList[numberBest]);
			selectionList.Remove(selectionList[numberBest]);
		}

		return result.ToArray(); 
	}
}