using UnityEngine;
using System.Collections;

public class SlingShot : MonoBehaviour {

    public GameObject line1;
    public GameObject line2;
    public LineRenderer[] lines;
    public Transform anchor1;
    public Transform anchor2;
    public Transform[] anchors;
    public GameObject dragPoint;
    public Transform midPoint;
    public Transform midPoint_out;
    public GameObject aimer;
    public GameObject projectile;

    private float[] lineLengths;
    public float startPower = 0;

    void Start()
    {
        //start position
        aimer.transform.position = new Vector3(1, 1, 0);
        //init
        //arrays
        anchors = new Transform[2];
        anchors[0] = anchor1;
        anchors[1] = anchor2;
        lineLengths = new float[2];
        //lines
        lines = new LineRenderer[2];
        line1.GetComponent<LineRenderer>().SetPosition(0, anchor1.position);
        line1.GetComponent<LineRenderer>().SetPosition(1, dragPoint.transform.position);
        line1.GetComponent<LineRenderer>().SetWidth(0.15f, 0.05f);
        lines[0] = line1.GetComponent<LineRenderer>();
        line2.GetComponent<LineRenderer>().SetPosition(0, anchor2.position);
        line2.GetComponent<LineRenderer>().SetPosition(1, dragPoint.transform.position);
        line2.GetComponent<LineRenderer>().SetWidth(0.15f, 0.05f);
        lines[1] = line2.GetComponent<LineRenderer>();
    }
    void Update()
    {
        UpdateLines();
        Aim();
        GetHeight(GetVelocity(), 3, 1);
        GetVelocity2();
        //Debug.Log(string.Format("Angle={0}", GetAngle()));

    }
    void UpdateLines()
    {   
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].SetPosition(1, dragPoint.transform.position);
            lines[i].SetPosition(0, anchors[i].position);
            lines[i].SetWidth(0.15f / lineLengths[i], 0.05f / lineLengths[i]);
            lineLengths[i] = Vector3.Distance(dragPoint.transform.position, anchors[i].position);

            if (lineLengths[i] <= 0.65f) lineLengths[i] = 0.65f;
        }
    }
    void Aim()
    {
        Vector3 pullDirection = midPoint.position - (dragPoint.transform.position - midPoint.position).normalized;
        aimer.transform.position = pullDirection;
        Debug.DrawRay(midPoint.position, (dragPoint.transform.position-midPoint.position).normalized * 10, Color.red);
    }
    public float GetVelocity()
    {
        float velocity = Vector3.Distance(dragPoint.transform.position, midPoint.transform.position);
        return velocity * 2.5f;
    }
    public void GetVelocity2()
    {
        startPower = Vector3.Distance(dragPoint.transform.position, midPoint.transform.position);
    }
    public float GetAngle()
    {
        float angle = Vector3.Angle((midPoint.transform.position - dragPoint.transform.position).normalized, Vector3.right);
        if (dragPoint.transform.position.y > aimer.transform.position.y) angle = angle * -1;
        
        return angle;
    }
    Vector3 GetShotDirection()
    {
        Vector3 shotDir = (aimer.transform.position - midPoint.transform.position).normalized;
        return shotDir;
    }
    public float GetDistance(float Vinit)
    {
        float g = Physics.gravity.y;
        float Vvert = Vinit * (Mathf.Sin(GetAngle() * Mathf.Deg2Rad));
        float Vhor = Vinit * (Mathf.Cos(GetAngle() * Mathf.Deg2Rad));
        float Tvert = (0 - Vvert) / g;
        float Thor = 2 * Tvert;
        float distance = Vhor * Thor;
        return distance;
    }
    public float GetHeight(float Vinit, int amountPoints, int pointIndex)
    {
        float g = Physics.gravity.y;
        float Vvert = Vinit * (Mathf.Sin(GetAngle() * Mathf.Deg2Rad));
        float Vhor = Vinit * (Mathf.Cos(GetAngle() * Mathf.Deg2Rad));
        float Tvert = (0 - Vvert) / g;
        float Thor = 2 * Tvert;
        float Dtot = Vhor * Thor;
        float Dp = (Dtot / (amountPoints)) * pointIndex;
        float T2 = Dp / Vhor;
        float height = ((Vvert * Dp) / Vhor) + 0.5f * g * Mathf.Pow(T2, 2);
        return height;
    }
    public void MakeShot(float power)
    {
        GameObject _projectile = Instantiate(projectile, midPoint_out.position, Quaternion.identity) as GameObject;
        //_projectile.rigidbody.AddForce(midPoint_out.TransformDirection(0, power * 2.57f, 0), ForceMode.Impulse);
        //_projectile.rigidbody.AddForce(GetShotDirection() * power * 2.65f, ForceMode.Impulse);
        _projectile.GetComponent<Rigidbody>().AddForce(GetShotDirection() * power * 2.5f, ForceMode.Impulse);
        //_projectile.rigidbody2D.AddRelativeForce(midPoint_out.TransformDirection(0, 2 * power, 0), ForceMode2D.Impulse);
        Destroy(_projectile, 4.0f);
    }
    public struct ShotData
    {
        float distance;
        float angle;
        float velocity;
    };

    
}
