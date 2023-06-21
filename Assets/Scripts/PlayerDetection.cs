using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    followPlayer script;
    // Start is called before the first frame update
    void Start()
    {
       script = GetComponent<followPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            script.enabled= true;
        }
    }
}
