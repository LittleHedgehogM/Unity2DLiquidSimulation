using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    private ContainerController myFollowingContainer;

    public void FollowContainer(ContainerController aContainer) 
    {
        myFollowingContainer = aContainer;
        this.transform.parent = aContainer.gameObject.transform;
    }
}
