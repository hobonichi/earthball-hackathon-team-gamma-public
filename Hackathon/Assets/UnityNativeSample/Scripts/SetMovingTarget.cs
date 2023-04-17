using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hobonichi;

public class SetMovingTarget : MonoBehaviour
{
    private MovingObject mo;

    // Start is called before the first frame update
    void Start()
    {
        mo = GetComponent<MovingObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * 0.01f, Color.red);
        if(!mo.isMoving())
        {
            Collider[] esas = Physics.OverlapSphere(transform.position, 0.01f);
            //到着した時に該当オブジェクトを破壊
            foreach (Collider c in esas)
            {
                if(c.tag == "Target")
                {
                    Destroy(c.gameObject);
                    return;
                }
            }

            //次に目的地にするオブジェクトを検索
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
            if (targets.Length > 0)
            {
                float maxDist = 10000f;
                GameObject target = null;
                Vector3 myLLH = SphericalGeometry.Pos2LLH(transform.position);
                foreach (GameObject g in targets)
                {
                    //Debug.Log(g.name);

                    Vector3 gLLH = SphericalGeometry.Pos2LLH(g.transform.position);
                    float dist = SphericalGeometry.GetDistanceOnSphere(myLLH, gLLH, 1.0f);
                    if(dist < maxDist)
                    {
                        maxDist = dist;
                        target = g;
                    }
                }
                if(target != null)
                {
                    mo.setTargetPlace(target.transform.position);
                }
            }
        }
    }
}
