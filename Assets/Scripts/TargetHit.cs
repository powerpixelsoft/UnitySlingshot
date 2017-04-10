using UnityEngine;
using System.Collections;

public class TargetHit : MonoBehaviour {

    TargetManager TM;

    void Start()
    {
        TM = GameObject.Find("targets").GetComponent<TargetManager>();
    }

	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameManager.Manager.AddScore(1);
            TM.SpawnTarget(1);
            Destroy(gameObject);
        }
    }
}
