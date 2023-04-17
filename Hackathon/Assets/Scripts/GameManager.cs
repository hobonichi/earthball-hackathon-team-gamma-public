using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform BallPointFront;
    [SerializeField]
    private Transform Earth;
    [SerializeField]
    private putObject putObjectInstance;
    private int currentObjectIdx=0;

    [SerializeField]
    private GameObject[] putedObjects;

    private bool isPenTouchingFlag=false;
    private bool wasPenTouchedFlag=false;
    private bool wasPenReleasedFlag=false;

    private const double EARTH_RADIUS=0.15/2;
    private const double MARGIN=0.01;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 earthSize = Earth.localScale;
        Debug.Log("earth size: " + earthSize.x);
        putObjectInstance.setPutedObject(putedObjects[0]);

        // putObjectInstance = new putObject();
        
    }

    // Update is called once per frame
    void Update()
    {
        checkBallPointTouched();
        if(wasPenTouched()){
            bool asTarget=false;
            if(currentObjectIdx==2){
                asTarget=true;
            }
            putObjectInstance.putObjectItem(BallPointFront.position,BallPointFront.rotation,asTarget);
            Debug.Log("pen touched");
        }
        if(wasPenReleased()){
            Debug.Log("pen released");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            putObjectInstance.setPutedObject(getNextObject());
        }

        // if (Input.GetKeyDown(KeyCode.Return))
        // {
        //     // Enterキーが押された瞬間の処理
        // }
    }

    private GameObject getNextObject(){
        if(++currentObjectIdx >= putedObjects.Length){
            currentObjectIdx=0;
        }
        Debug.Log($"currentObjectIdx:{currentObjectIdx}");
        return putedObjects[currentObjectIdx];
    }

    private bool checkBallPointTouched(){
        Vector3 ballPointFrontPosition = BallPointFront.position;
        Vector3 earthPosition = Earth.position;

        // 両者の位置の差分ベクトルを計算
        Vector3 difference = earthPosition - ballPointFrontPosition;
        // ベクトルの長さ（距離）を計算
        float distance = difference.magnitude;

        //ベン先のサイズを取得
        Vector3 penFrontSize = BallPointFront.localScale;
        // 地球のサイズを取得
        Vector3 earthSize = Earth.localScale;

        if(distance<EARTH_RADIUS){
            if(!isPenTouchingFlag){
                wasPenTouchedFlag=true;
            }
            isPenTouchingFlag=true;
            // Debug.Log("Distance between spheres: " + distance);
        }else if(distance > (EARTH_RADIUS+MARGIN)) {
            if(isPenTouchingFlag){
                wasPenReleasedFlag=true;
            }
            isPenTouchingFlag=false;
        }

        return true;
    }

    //　現在接触しているかどうかを判定する関数。これは副作用がないのでpublicとする。
    public bool isPenTouching(){
        return isPenTouchingFlag;
    }

    // 下記関数はこれらをpublicにしたいが、現状の実装ではbugの温床になるのでprivate
    // タッチした瞬間および離した瞬間は、GameManagerからディスパッチして使用する
    private bool wasPenTouched(){
        bool ret = wasPenTouchedFlag;
        if(wasPenTouchedFlag==true){
            wasPenTouchedFlag = false;
        }
        return ret;
    }

    private bool wasPenReleased(){
        bool ret = wasPenReleasedFlag;
        if(wasPenReleasedFlag==true){
            wasPenReleasedFlag = false;
        }
        return ret;
    }
}
