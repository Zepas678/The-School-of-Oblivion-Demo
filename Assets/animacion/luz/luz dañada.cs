using UnityEngine;
using System.Collections; 

public class BrokenLight : MonoBehaviour
{
    public Light targetLight; 

    [Header("Flicker Timing")]
    public float minFlickerInterval = 0.05f; // Intervalo mínimo entre encendido/apagado
    public float maxFlickerInterval = 0.2f;  // Intervalo máximo entre encendido/apagado
    public float flickerDuration = 5f;       // Cuánto tiempo dura el efecto de parpadeo (0 para continuo)
    public float offChance = 0.7f;           // Probabilidad de que la luz se apague (0.0 a 1.0)

    [Header("Intensity Variation")]
    public bool varyIntensity = true;      // Habilitar variación de intensidad
    public float minIntensity = 0.5f;      // Intensidad mínima cuando la luz está encendida
    public float maxIntensity = 1f;        // Intensidad máxima cuando la luz está encendida

    private bool isFlickering = false;
    private float originalIntensity;       // Para guardar la intensidad original de la luz

    void Start()
    {
        
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
            if (targetLight == null)
            {
                Debug.LogError("No se encontró un componente Light en este GameObject ni se asignó una luz externa. Deshabilitando el script.");
                enabled = false; 
                return;
            }
        }

        
        originalIntensity = targetLight.intensity;

        
        if (flickerDuration > 0)
        {
            StartCoroutine(StartFlickeringForDuration());
        }
        else
        {
            StartFlickeringContinously(); 
        }
    }

    void StartFlickeringContinously()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(DoFlicker());
        }
    }

    IEnumerator StartFlickeringForDuration()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(DoFlicker());
            yield return new WaitForSeconds(flickerDuration);
            StopFlickering();
        }
    }

    IEnumerator DoFlicker()
    {
        while (isFlickering)
        {
            float randomInterval = Random.Range(minFlickerInterval, maxFlickerInterval);

            if (Random.value < offChance) // Decide si la luz se apaga
            {
                targetLight.enabled = false; // Apaga la luz
            }
            else // La luz se enciende
            {
                targetLight.enabled = true; // Enciende la luz

                
                if (varyIntensity)
                {
                    targetLight.intensity = Random.Range(minIntensity, maxIntensity);
                }
                else
                {
                    targetLight.intensity = originalIntensity; // Restaura la intensidad original si no varía
                }
            }
            yield return new WaitForSeconds(randomInterval); 
        }
    }

    public void StopFlickering()
    {
        if (isFlickering)
        {
            isFlickering = false;
            StopAllCoroutines(); 
            targetLight.enabled = true; 
            targetLight.intensity = originalIntensity; 
        }
    }

    
    public void StartFlickering()
    {
        if (!isFlickering)
        {
            if (flickerDuration > 0)
            {
                StartCoroutine(StartFlickeringForDuration());
            }
            else
            {
                StartFlickeringContinously();
            }
        }
    }
}