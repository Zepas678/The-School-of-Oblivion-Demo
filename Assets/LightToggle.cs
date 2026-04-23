using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light myLight;  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            myLight.enabled = !myLight.enabled;  // Cambia el estado de la luz
        }
    }
}