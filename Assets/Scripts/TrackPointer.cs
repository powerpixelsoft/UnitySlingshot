using UnityEngine;
using System.Collections;

public class TrackPointer : MonoBehaviour {
    [HideInInspector]
    public Vector3 pointerPosition;
    public GameObject cannon;
    public GameObject tip;
    public float RotationSpeed;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Start()
    {
    }
    void Update()
    {
        TrackMousePointer();
        AlignCannon();
        //Debug.Log(pointerPosition);
    }
	void TrackMousePointer()
    {
        float rayDist = 400.0f;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDist))
        {
            pointerPosition = hit.point;
            pointerPosition.z = 0;
        }
    }
    void AlignCannon()
    {
        Vector3 direction = (pointerPosition - cannon.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.down);
        cannon.transform.rotation = rotation;
    }
}
