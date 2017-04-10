using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trajectory : MonoBehaviour {

    TrackPointer TP;
    //BallThrow BW;

    public GameObject point;
    //float angle = 30.0f;
    public int pointsNum = 30;
    //float Vinit = 20.0f;
    public List<GameObject> Points = new List<GameObject>();
    

    Vector3 targetPoint = new Vector3(0, 0, 0);
    float distance = 0;
    public float powMul = 2.5f;
    void Start()
    {
        TP = GameObject.Find("Main Camera").GetComponent<TrackPointer>();
        //BW = GameObject.Find("Manager").GetComponent<BallThrow>();
        CreatePoints(pointsNum);
    }
    void Update()
    {
        PositionPoints();
        //Debug.Log(string.Format("Current distance = {0}", CheckDistance2D(powMul)));
    }
    void CreatePoints(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject pointPrefab = Instantiate(point, transform.position, Quaternion.identity) as GameObject;
            pointPrefab.GetComponent<Collider>().enabled = false;
            pointPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Points.Add(pointPrefab);
        }
    }
    void PositionPoints()
    {
        for (int i = 0; i < Points.Count; i++)
        {
            Points[i].transform.position = new Vector3(transform.position.x + (i * (CheckDistance2D(powMul) / Points.Count)),
                                                       transform.position.y + CalcHeight(CheckAngle(), (i * (CheckDistance2D(powMul) / Points.Count)), CheckDistance2D(powMul) * 1.25f), 
                                                       0);
        }
    }
    float CalcHeight(float angle,
                     float distance,
                     float Vinit)
    {
        float Vvert = Vinit * (Mathf.Sin(angle * Mathf.Deg2Rad));
        float Vhor = Vinit * (Mathf.Cos(angle * Mathf.Deg2Rad));
        float x = distance;
        float t = distance / Vhor;
        float g = Physics.gravity.y;
        float u = Vvert;
        float s = (( u * x ) / Vhor ) + 0.5f * g * Mathf.Pow( t, 2 );    
        return s;
    }
    float CheckAngle()
    {
        Vector3 groundVector = Vector3.right;
        Vector3 objectVector = (TP.pointerPosition - TP.cannon.transform.position).normalized;
        float angle = Vector3.Angle(groundVector, objectVector);

        return angle;
    }
    float CheckDistance()
    {  
        float rayDist = 700.0f;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDist))
        {      
            Debug.DrawRay(ray.origin, Camera.main.transform.forward * rayDist, Color.red);
            targetPoint = hit.point;
            distance = hit.distance;
   
            return distance;
        }
        else
        {
            return distance;
        }
       
    }
    public float CheckDistance2D(float mul)
    {
        float distance2D = TP.pointerPosition.x - TP.tip.transform.position.x;
        return distance2D * mul;
    }
}
