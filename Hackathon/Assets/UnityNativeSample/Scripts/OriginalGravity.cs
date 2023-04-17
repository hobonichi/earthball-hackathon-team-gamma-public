using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalGravity : MonoBehaviour
{
    //重力方向(地球の中心)にオブジェクトの足むける

    [SerializeField]
    private GameObject earth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gravityUp = (transform.localPosition - earth.transform.localPosition).normalized;
        transform.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

        /*
        float dist = Vector3.Distance(transform.localPosition, earth.transform.localPosition);
    Debug.Log(dist);
        if (dist > height)
        {
            Debug.Log("attract");
            transform.localPosition -= transform.up* 0.098f;
        }
        else if(dist < height)
        {
            Debug.Log("detract");
            transform.localPosition += transform.up * 0.098f;
        }
        */
    }
}
