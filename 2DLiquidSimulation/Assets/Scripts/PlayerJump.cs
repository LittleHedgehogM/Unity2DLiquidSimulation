using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent myAgent;

    public void PlayerJumpTo(Vector3 targetPosition)
    {
        myAgent.enabled = false;
        this.transform.position = targetPosition;
        myAgent.enabled = true;
    }
}
