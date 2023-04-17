using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hobonichi;

public class MovingAirplaneScript : MonoBehaviour
{
    
    [SerializeField]
    private float movingTime = 3f;
    [SerializeField]
    private bool isMoving = false;
    private Vector3 nextPlace;
    private Vector3 sleripingPlace;
    private Vector3 prevPlace;
    private float endTime;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (endTime <= Time.time)
            {
                isMoving = false;
            }
            else
            {
                sleripingPlace = Vector3.Slerp(prevPlace, nextPlace, (Time.time - startTime) / movingTime);
                transform.LookAt(nextPlace);
                transform.position = sleripingPlace;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.tag == "Earth")
                    {

                        startTime = Time.time;
                        endTime = startTime + movingTime;
                        prevPlace = transform.position;
                        nextPlace = hit.point;
                        isMoving = true;

                    }
                }
            }
        }
    }
    
    private float getAngleToPosition(Vector3 point)
    {
        Vector3 myLLH = SphericalGeometry.Pos2LLH(transform.position);
        Vector3 targetLLH = SphericalGeometry.Pos2LLH(point);
        float angle = SphericalGeometry.GetDirectionOnSphere(
            new Vector2(myLLH.x, myLLH.y),
            new Vector2(targetLLH.x, targetLLH.y),
            1.0f);
        return angle;
    }
    
}
