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
	public enum TypeForm { Circle, Spiral, Curve };
	TypeForm form;
	public float radius = 10f;
	public float gap = 0f;
	public bool withRotation = false;

	public AnimationCurve curveZ;
	public bool withCurveY = false;
	public AnimationCurve curveY;

	//rajout des fonctionalités
	private void OnGUI()
	{
		GUILayout.Label("GeoTools : the Tools to order geometrically", EditorStyles.boldLabel);

		form = (TypeForm)EditorGUILayout.EnumPopup("Form selected", form);
		radius = EditorGUILayout.FloatField("Radius : ", radius);
		if (form == TypeForm.Spiral) gap = EditorGUILayout.FloatField("Gap : ", gap);
		if (form != TypeForm.Curve) withRotation = EditorGUILayout.Toggle("With Rotation : ", withRotation);
		if (form == TypeForm.Curve)
		{
			curveZ = EditorGUILayout.CurveField("Curve Z form", curveZ);
			withCurveY = EditorGUILayout.Toggle("With Curve Y : ", withCurveY);
			if (withCurveY) curveY = EditorGUILayout.CurveField("Curve Y form", curveY);
		}

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
			case TypeForm.Curve:
				MakeCurve();
				Debug.Log("Curve maked");
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

		Vector3 center = Vector3.zero;

		float modifRadius = radius; 

		for (int i = 0; i < transformSelection.Length; i++)
		{
			float angle = -3.141f * 0.1f * i;
			float posX = Mathf.Cos(angle);
			float posZ = Mathf.Sin(angle);
			
			float decal = angle / -3.141f;
			Vector3 newPosition = new Vector3(posX, 0, posZ) * modifRadius;

			float posY = i * gap;
			newPosition.y = posY; 

			newPosition += center; 
			transformSelection[i].localPosition = newPosition;

			if (decal % 1 == 0 && i > 0)
			{
				modifRadius += radius;

				if (center.x == 0) { center.x = radius; }
				else { center.x = 0; }
			}

			if (withRotation)
			{
				Vector3 lookPoint = center;
				lookPoint.y = posY; 
				transformSelection[i].LookAt(lookPoint);
			}
		}

	}

	void MakeCurve()
	{
		Transform[] transformSelection = cleanSelectionTransform();

		float length = transformSelection.Length; 		

		for (int i = 0; i < transformSelection.Length; i++)
		{
			float posX = i / (length - 1);
			float posZ = curveZ.Evaluate(posX);			
			float posY = 0; 
			if (withCurveY)	posY = curveY.Evaluate(posX);

			Vector3 newPosition = new Vector3(posX, posY, posZ) * radius * (length - 1);
			transformSelection[i].localPosition = newPosition; 
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