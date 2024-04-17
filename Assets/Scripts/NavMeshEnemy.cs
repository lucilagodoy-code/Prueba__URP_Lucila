using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class NavMeshEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform pointer;
    public float lookRadius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(pointer.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(pointer.position);

        }

        Debug.Log(distance);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
