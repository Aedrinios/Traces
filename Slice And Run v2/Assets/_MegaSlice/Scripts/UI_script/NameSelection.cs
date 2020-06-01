using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameSelection : MonoBehaviour
{
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text = FindObjectOfType<PlayerManager>().name;
    }
}
