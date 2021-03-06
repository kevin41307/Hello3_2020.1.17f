using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{

    public CapsuleCollider cc;

    public LayerMask groundLayer;
    private Vector3 point1;
    private Vector3 point2;
    private Collider[] hits;
    private float offset = 0.2f;

    private void Awake()
    {
        //cc = GetComponentInParent<CapsuleCollider>();

    }

    private void FixedUpdate()
    {
        point1 = transform.position + Vector3.up * cc.radius * cc.transform.localScale.y + Vector3.down * offset;
        point2 = point1 + Vector3.up * cc.height * cc.transform.localScale.y + Vector3.down * cc.radius * cc.transform.localScale.y * 2f + Vector3.down * offset;

        hits = Physics.OverlapCapsule(point1, point2, cc.radius * cc.transform.localScale.y + offset, groundLayer.value);
        if (hits.Length > 0)
        {
            foreach (var col in hits)
            {
                //Debug.Log(col.name);
            }
            SendMessageUpwards("IsGround");
            

        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(point1, cc.radius * cc.transform.localScale.y);
        Gizmos.DrawSphere(point2, cc.radius * cc.transform.localScale.y);
    }
}
