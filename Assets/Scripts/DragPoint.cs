using UnityEngine;
using System.Collections;

public class DragPoint : MonoBehaviour
{
    [HideInInspector]
    public Vector3 currentPosition;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 defaulPos;

    public Transform midPoint_out;
    SlingShot SS;

    void Start()
    {
        SS = GameObject.Find("slingshot").GetComponent<SlingShot>();
        defaulPos = new Vector3(-6.25f, -0.25f, 0);
        transform.position = defaulPos;
    }
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                                                                             Input.mousePosition.y, screenPoint.z));
        Cursor.visible = false;
        //Debug.Log(offset);
    }
    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;

        transform.position = currentPosition;
        
    }
    void OnMouseUp()
    {
        Cursor.visible = true;
        SS.MakeShot(SS.startPower);        //transform.position = defaulPos;
    }
}
