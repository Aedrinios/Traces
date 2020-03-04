using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "AddData", order = 100)]
public class SaveScriptableObject : ScriptableObject
{
	public GameObject[] saveObjects; 
}
