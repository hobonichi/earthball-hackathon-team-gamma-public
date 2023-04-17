using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
// using System.Numerics;
using UnityEngine;

public class putObject : MonoBehaviour
{
    [SerializeField]
    private GameObject putedObject;
    // [SerializeField]
    // private float objectScale = 0.04f; //置いたものの拡大倍率、1にするとひまわりが巨大になる

    [SerializeField]
    private Transform Earth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void putObjectItem(Vector3 pointedPossition,Quaternion pointedRotation,bool asTarget=false){
        // Ray ray = new Ray(pointedPossition,Earth.position);
        // RaycastHit hit;
        // if(Physics.Raycast(ray, out hit))
        // {
        //     Debug.Log(hit.collider.gameObject.name);
        //     if (hit.collider.tag == "Earth")
        //     {
        //         Vector3 pos = hit.point;
        //         Debug.Log(pos);
        //         GameObject p = Instantiate(
        //             putedObject,
        //             pos,
        //             Quaternion.identity,
        //             transform);
        //         p.transform.rotation = Quaternion.FromToRotation(p.transform.up, hit.normal) * p.transform.rotation;
        //         p.transform.localScale = Vector3.one * objectScale;
        //     }
        // }
        GameObject p = Instantiate(
                    putedObject, 
                    pointedPossition,
                    pointedRotation,
                    transform
                    );
                p.transform.parent = Earth.transform;
                if(asTarget){
                    p.tag="Target";
                }
                // p.transform.localScale = Vector3.one * objectScale;
    }

    //生えてくるものを変える
    public void setPutedObject(GameObject gameObject)
    {
        putedObject = gameObject;
    }
}
