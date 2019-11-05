using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlaceOnCircle : MonoBehaviour
{
	public float radius = 10;
	public float angle = 0;
    public Vector3 axe = Vector3.up; 

	Transform[] platforms;
	[TextArea] public string Notes = "en cochant, place tous les enfants autour d'un cercle.";

	private void OnEnable()
	{
		platforms = GetComponentsInChildren<Transform>();
		transform.rotation = Quaternion.Euler(Vector3.zero);
		PlaceOncircle(platforms, radius);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Euler(axe * 90);
    }

	void PlaceOncircle(Transform[] platforms, float radius)
	{
		for (int i = 1; i < platforms.Length; i++)
		{			
			float angle = 6.283f / (platforms.Length - 1);
			float angle_x = Mathf.Cos(i * angle);
			float angle_y = Mathf.Sin(i * angle);
			Vector3 dir = new Vector3(angle_x, angle_y, 0);
			float tourne = 360 / (platforms.Length - 1) * i;

			float scale = radius * 1.57f; 

			platforms[i].localPosition = Vector3.zero;
			platforms[i].localPosition = dir * radius;
			platforms[i].rotation = Quaternion.Euler(new Vector3(0, 0, tourne));
			platforms[i].localScale = new Vector3(platforms[i].localScale.x, scale, platforms[i].localScale.z);  
		}
	}
}
