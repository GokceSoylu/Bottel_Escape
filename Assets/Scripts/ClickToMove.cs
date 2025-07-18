using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent nvAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nvAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        if(Input.GetMouseButtonDown(0))
        {
            //create a ray from the camere to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //check if the ray hits the ground(NavMesh)
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                //move the  agent to the clicked position
                nvAgent.SetDestination(hit.point);
            }
        }
    }
}
