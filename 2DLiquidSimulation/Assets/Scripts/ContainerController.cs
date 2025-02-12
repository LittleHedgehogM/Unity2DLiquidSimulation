using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerController : MonoBehaviour
{
    [SerializeField] private Transform LiquidContainer;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private Transform liquid;
    [SerializeField] private Transform receiverContainer;

    private void pouringStart()
    {
        liquid.SetParent(LiquidContainer);
    }

    public void pouringEnd()
    {
        liquid.SetParent(receiverContainer);
    }
}
