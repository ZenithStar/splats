using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class collidercheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            
        }
    }
    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
       
         if(other.gameObject.tag == "mailbox")
         {
                print("penis");
         }
        
    }
}
