using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Fire_Laser : MonoBehaviour
{
    [SerializeField]
    private float _Fire_Laser_Speed = 13.0f;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        DestroyObj();
    }

    void Movement()
    {
        //To make the Fire_Laser go upwards.
        Vector3 Fire_Laser_Dir = Vector3.up;
        transform.Translate(Fire_Laser_Dir * _Fire_Laser_Speed * Time.deltaTime);
    }
    void DestroyObj()
    {
        //To destroy the game object.
        if(transform.position.y > 6.22f || transform.position.y < -6.17f)
        {
            Destroy(this.gameObject);
            //Destroys the gameObject. Every script will be attached to a game object and that is how this command identifies which game object to destroy.
            // **Destroy(this.gameObject);** //This is a way of explicitly defining that it needs to destroy the game object which is attached with this script.
            
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
                Debug.Log("Parent Destroyed");
            }
        }
    }
}
