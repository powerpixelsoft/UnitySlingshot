using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour {

    public GameObject target;
    public GameObject[] targets;
    public GameObject[] locations;

    int random;
    void Awake()
    {
    }
	void Start () 
    {
        SpawnTarget(1);
        RemoveInstances();
	}	
	void Update () {
	
	}
    public void SpawnTarget(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            random = Random.Range(0, 5);
            GameObject _target = Instantiate(target, locations[random].transform.position, Quaternion.identity) as GameObject;
        }   
    }
    void RemoveInstances()
    {
        TargetHit[] tempTargetsArray = new TargetHit[6];

        tempTargetsArray = FindObjectsOfType(typeof(TargetHit)) as TargetHit[];
        for (int i = 1; i < tempTargetsArray.Length; i++)
        {
            Destroy(tempTargetsArray[i]);
            tempTargetsArray[i] = null;
        }
    }
}
