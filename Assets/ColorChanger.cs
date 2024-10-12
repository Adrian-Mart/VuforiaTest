using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    private int currentMaterialIndex = 0;
    private bool disableTouch = false;

    void Update()
    {
        if (!disableTouch && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ChangeMaterial();
        }
    }

    public void ChangeMaterial(bool disableTouch = false)
    {
        this.disableTouch = disableTouch;
        currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
        GetComponent<Renderer>().material = materials[currentMaterialIndex];
    }
    
}
