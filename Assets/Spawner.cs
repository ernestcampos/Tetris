using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    //groups
    public List<GameObject> groups;

	// Use this for initialization
	void Start () {
        spawnNext();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject spawnNext()
    {
        int index = Random.Range(0, groups.Count);

        return (GameObject) Instantiate(groups[index],
            transform.position,
            Quaternion.identity);
    }
}
