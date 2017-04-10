using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileTrajectory: MonoBehaviour
{
    SlingShot SS;

    public GameObject point;
    public int pointNum = 25;
    public List<GameObject> points = new List<GameObject>();

    Vector3 updatedPos = new Vector3(0,0,0);

    void Start()
    {
        SS = GameObject.Find("slingshot").GetComponent<SlingShot>();

        CreatePoints();
    }
    void Update()
    {
        UpdatePoints();
    }
    public void CreatePoints()
    {
        for (int i = 0; i < pointNum; i++)
        {
            GameObject _point = Instantiate(point, transform.position, Quaternion.identity) as GameObject;
            _point.transform.localScale = new Vector3(0.2f / (i + 1), 0.2f / (i + 1), 0.2f / (i + 1));
            points.Add(_point);
        }
    }
    public void UpdatePoints()
    {
        if (Input.GetMouseButton(0) && SS.GetAngle() >= 0 && SS.GetAngle() <= 89.9f)
        {
            for (int i = 0; i < points.Count; i++)
            {
                updatedPos.x = SS.midPoint.transform.position.x + i * (SS.GetDistance(SS.GetVelocity()) / pointNum);
                updatedPos.y = SS.midPoint.transform.position.y + SS.GetHeight(SS.GetVelocity(), pointNum, i);
                updatedPos.z = 0;

                points[i].transform.position = updatedPos;
                points[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].gameObject.SetActive(false);
            }
        }
    }	
}
