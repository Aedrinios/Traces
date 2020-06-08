using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class TranslateText : MonoBehaviour
{
    [TextArea] public string frenchText;
    public float modifSize = 0f;

    private void Start()
    {
        if (DetectLanguageSystem.inFrench)
        {
            TranslateTextPro();
        }
    }

    void TranslateTextPro()
    {
        if (GetComponent<TextMeshProUGUI>())
        {
            TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
            textMesh.text = frenchText;
            textMesh.fontSize += modifSize; 
        }
        if (GetComponent<TextMeshPro>())
        {
            TextMeshPro textMesh = GetComponent<TextMeshPro>();
            textMesh.text = frenchText;
            textMesh.fontSize += modifSize;
        }
        if (GetComponent<Text>())
        {
            Text textMesh = GetComponent<Text>();
            textMesh.text = frenchText;
            //textMesh.fontSize += modifSize;
        }
    }
}
