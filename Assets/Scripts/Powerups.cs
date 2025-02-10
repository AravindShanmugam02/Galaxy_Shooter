using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField]
    private float _Powerups_speed = 3.0f;
    [SerializeField]
    private int _PowerUp_ID;
    [SerializeField]
    private AudioClip _powerup_Clip;

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

    //Movement
    void Movement()
    {
        Vector3 powerups_dir = Vector3.down;
        transform.Translate(powerups_dir * _Powerups_speed * Time.deltaTime);
    }

    //DestroyObject
    void DestroyObj()
    {
        if(transform.position.y < -6.17f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag == "Player")
        {
            Player play = Object.transform.GetComponent<Player>();
            if(play != null)
            {

                AudioSource.PlayClipAtPoint(_powerup_Clip, transform.position);

               // if(_PowerUp_ID == 0)
               // {
               //     Debug.Log("TripleShot Collected");
               //     play.TripleShotActive();
               // }
               // else if(_PowerUp_ID == 1)
               // {
               //     Debug.Log("Speed Collected");
               // }
               // else if(_PowerUp_ID == 2)
               // {
               //     Debug.Log("Shield Collected");
               // }

               //instead of using this bunch of IfElse statement, we can use Switch staement.

                switch(_PowerUp_ID)
                {
                    case 0:
                        Debug.Log("TripleShot Collected");
                        play.TripleShotActive();
                        break;

                    case 1:
                        Debug.Log("SpeedBoost Collected");
                        play.SpeedBoostActive();
                        break;

                    case 2:
                        Debug.Log("Shield Collected");
                        play.ShieldActive();
                        break;
                }

                Destroy(this.gameObject);
            }
        }
    }
}
