using UnityEngine;
using System.Collections;

public class RotateSlingshot : MonoBehaviour {
    SlingShot SS;
    Quaternion rotation;
    Vector3 initRot;
    float rotSpeed = 5;
    void Start()
    {
        SS = GameObject.Find("slingshot").GetComponent<SlingShot>(); 
        transform.rotation = Quaternion.identity;
    }
    void Update()
    {
        //RotSlingshot();
        Debug.Log(transform.rotation.eulerAngles.z);
        LockedRotation();
    }
    void LateUpdate() 
    { 
    }
    
    float rotationZ;
    float sensitivityZ = 2;
    void LockedRotation()
    {
        rotationZ += Input.GetAxis("Mouse ScrollWheel") * sensitivityZ * 10;
        rotationZ = Mathf.Clamp(rotationZ, -30.0f, 30.0f);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotationZ);
    }
}
