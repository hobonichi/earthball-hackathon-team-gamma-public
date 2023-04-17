using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hobonichi;

public class MovingObject : MonoBehaviour
{
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float movingTime = 3f;

    [SerializeField]
    private bool moving = false;

    [SerializeField]
    private GameObject earth;

    private Vector3 targetPlace;
    private Vector3 sleripingPlace;
    private Vector3 prevPlace;
    private float endTime;
    private float startTime;
    private ObjectOnSphere oos;

    // Start is called before the first frame update
    void Start()
    {
        oos = GetComponent<ObjectOnSphere>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if(Time.time >= endTime)
            {
                moving = false;
            }
            sleripingPlace = Vector3.Slerp(prevPlace, targetPlace, (Time.time - startTime) / movingTime);
            // transform.position = sleripingPlace + earth.transform.position;
            oos.location = SphericalGeometry.Pos2LLH(sleripingPlace);
            oos.location.z = height;
        }
        else
        {

        }
        
    }

    public void setTargetPlace(Vector3 targetPos)
    {
        Vector3 myLLH = SphericalGeometry.Pos2LLH(transform.position);
        Vector3 tLLH = SphericalGeometry.Pos2LLH(targetPos);

        float angle = SphericalGeometry.GetDirectionOnSphere(myLLH, tLLH, height);
        //transform.rotation = Quaternion.Euler(0, angle, 0);
        oos.direction = new Vector3(0, angle, 0);
        Debug.Log(transform.rotation.eulerAngles);

        targetPlace = targetPos;
        startTime = Time.time;
        endTime = startTime + movingTime;
        prevPlace = transform.position;
        moving = true;
    }

    public void setHeight(float h)
    {
        height = h;
    }

    public bool isMoving()
    {
        return moving;
    }
}
