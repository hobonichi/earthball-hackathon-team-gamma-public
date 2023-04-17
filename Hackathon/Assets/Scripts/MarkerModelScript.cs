using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerModelScript : MonoBehaviour
{
    public GameManager gameManager;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isPenTouching()){
            material.color = Color.red;
        }else{
            material.color = Color.white;
        }
    }
}
