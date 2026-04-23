using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class TestNavMeshAgent : MonoBehaviour
{
    private NavMeshAgent agent;

    IEnumerator Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.enabled = false;
            yield return new WaitForSeconds(0.1f);
            agent.enabled = true;
            Debug.Log("NavMeshAgent activado. isOnNavMesh: " + agent.isOnNavMesh);
        }
        else
        {
            Debug.LogError("No se encontró NavMeshAgent.");
        }
    }

    void Update()
    {
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            Debug.Log("Agent position: " + transform.position);
        }
    }
}