using UnityEngine;
using UnityEngine.AI; // Asegúrate de tener esto para NavMeshAgent

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Transform[] patrolPoints; // Puntos de patrulla (los definirás en el Inspector)
    public float chaseRange = 10f; // Rango de detección del jugador

    private NavMeshAgent agent;
    private int currentPatrolIndex; // Este ya no será secuencial, sino el índice del punto aleatorio actual
    private bool isChasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        
        if (patrolPoints.Length > 0)
        {
            MoveToRandomPatrolPoint();
        }
        else
        {
            Debug.LogWarning("EnemyAI: No se han asignado puntos de patrulla. El monstruo solo perseguirá al jugador.");
        }
    }

    void Update()
    {
        // Asegúrate de que el agente esté activo y el jugador exista
        if (agent == null || !agent.enabled || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= chaseRange)
        {
            if (!isChasing) 
            {
                isChasing = true;
            }
            agent.SetDestination(player.position);
        }
        
        else if (isChasing)
        {
            isChasing = false;
            
            if (patrolPoints.Length > 0)
            {
                MoveToRandomPatrolPoint();
            }
        }
        
        else if (!isChasing && patrolPoints.Length > 0 && agent.remainingDistance < 0.5f && !agent.pathPending)
        {
           
            MoveToRandomPatrolPoint();
        }
    }

    /// <summary>
    
    /// </summary>
    void MoveToRandomPatrolPoint()
    {
        if (patrolPoints.Length == 0) return; // No hay puntos para moverse

        int previousPatrolIndex = currentPatrolIndex;
        int newPatrolIndex;

        // Si solo hay un punto, simplemente ve a ese
        if (patrolPoints.Length == 1)
        {
            newPatrolIndex = 0;
        }
        else
        {
            
            do
            {
                newPatrolIndex = Random.Range(0, patrolPoints.Length);
            } while (newPatrolIndex == previousPatrolIndex); 
        }

        currentPatrolIndex = newPatrolIndex; 
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    
    void OnDrawGizmosSelected()
    {
        if (transform == null) return; 

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Dibuja los puntos de patrulla y las conexiones en el editor
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] != null)
                {
                    Gizmos.DrawWireSphere(patrolPoints[i].position, 0.5f); 
                    if (i < patrolPoints.Length - 1 && patrolPoints[i+1] != null)
                    {
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i+1].position);
                    }
                }
            }
        }
    }
}