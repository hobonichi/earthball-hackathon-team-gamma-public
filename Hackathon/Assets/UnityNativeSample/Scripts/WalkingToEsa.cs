using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hobonichi;

public class WalkingToEsa : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject[] GetTargets()
    {
        return GameObject.FindGameObjectsWithTag("Target");
    }


    private float getKyoriFromMe(Transform t)
    {
        Vector3 myLonLat = SphericalGeometry.Pos2LLH(transform.position);
        Vector3 tLonLat = SphericalGeometry.Pos2LLH(t.position);
        float dist = SphericalGeometry.GetDistanceOnSphere(
            new Vector2(myLonLat.x, myLonLat.y),
            new Vector2(tLonLat.x, tLonLat.y),
            1f);
        return dist;
    }

    private GameObject getNearbyTargetFromMe(GameObject[] gameObjects)
    {
        if (gameObjects.Length <= 0)
        {
            return null;
        }
        float minDist = 10000f;
        GameObject answer = null;
        foreach (GameObject g in gameObjects)
        {
            float dist = getKyoriFromMe(g.transform);
            if (dist < minDist)
            {
                minDist = dist;
                answer = gameObject;
            }
        }
        return answer;
    }
}
