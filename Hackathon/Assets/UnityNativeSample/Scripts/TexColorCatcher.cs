using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hobonichi;

public class TexColorCatcher : MonoBehaviour
{
    [SerializeField]
    [Header("Earth¥base を指定します テクスチャg0のread/writeをオンにしてください")]
    private MeshRenderer textureBase;
    [SerializeField]
    private Color oceanColor;

    private MovingObject mo;
    private Texture2D tex2D;
    [SerializeField]
    private Color textureColor;

    // Start is called before the first frame update
    void Start()
    {
        mo = GetComponent<MovingObject>();
        tex2D = textureBase.material.mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 myLLH = SphericalGeometry.Pos2LLH(transform.position);
        textureColor = getColorFromTex(myLLH.x, myLLH.y);
        

        if (compareColor(textureColor, oceanColor))
        {
            mo.setHeight(0.9f);
        }
        else
        {
            mo.setHeight(1f);
        }
        
    }

    public Color getColorFromTex(float lon, float lat)
    {
        //0 ~ tex2d.width が、 -180 ~ 180
        //0 ~ tex2d.height が、 -90 ~ 90
        float xPos = (lon + 180) / 360 * tex2D.width;
        float yPos = (lat + 90) / 180 * tex2D.height;

        Debug.Log("lon:" + lon + ":lat:" + lat);
        Debug.Log("xpos:" + xPos + ":ypos" + yPos);
        return tex2D.GetPixel(Mathf.FloorToInt(xPos), Mathf.FloorToInt(yPos));
    }

    public bool compareColor(Color32 baseC, Color32 compC)
    {
        if((baseC.r > compC.r - 10 && baseC.r < compC.r + 10) &&
           (baseC.g > compC.g - 10 && baseC.g < compC.g + 10) &&
           (baseC.b > compC.b - 10 && baseC.b < compC.b + 10))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
