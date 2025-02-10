using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 6.0f;

    Vector3 direction = Vector3.down;

    private int _points;

    private Player _player;
    private Shield_Function _shield;

    private Animator _anim;
    //private SpriteRenderer _spr_rend;

    private PolygonCollider2D _collider;

    private AudioSource _enemyAud;

    [SerializeField]
    private AudioClip _enemyDestroy;

    // Start is called before the first frame update
    void Start()
    {
        //comunicate with player to increase score. We are keeping this in start to Find the object once and use it's reference many times.
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("_player is Empty");
        }
        
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("_anim in enemy is empty");
        }

        //_spr_rend = GetComponent<SpriteRenderer>();
        //if(_spr_rend)
        //{
        //    Debug.LogError("_spr_rend -> enemy is empty");
        //}

        _collider = GetComponent<PolygonCollider2D>();
        if(_collider == null)
        {
            Debug.LogError("_collider in enemy is empty");
        }

        _enemyAud = GetComponent<AudioSource>();
        if (_enemyAud == null)
        {
            Debug.LogError("_enemyAud is empty in enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
        _points = ((int)Random.Range(5f, 10f)); //explicit conversion of floast to int.
    }

    void Behaviour()
    {
        //To make the Enemy Object go down
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y <= -7.31f)
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 7.36f, 0);
            //Also we can make it more optimal by assigning a float variable to get random range values like --> float randomX = Random.Range(-9.0f,9.0f);
            //Then we can use that randomX in the transform.position code --> transform.position = new Vector3(rendomX, 6.47f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D Object) //A game object which collides with the enemy is detected n stored to this "Object" parameter.
    //This method triggers when the collision happens.
    //To detect the collision we need Collider enabled for the game object and we need Rigid body component added to act upon the collision.
    {
        if (Object.tag == "Fire_Laser")
        {
            Destroy(Object.gameObject); //This destroyes the game object detected in "Object" parameter.
            Debug.Log(Object.tag + " Hit");

            _player.AddScore(_points);

            _collider.enabled = false;

            _enemyAud.clip = _enemyDestroy;
            _enemyAud.Play();
            _anim.SetTrigger("OnEnemyDestroy");
            Destroy(this.gameObject, 1.5f);

        }
        else if (Object.tag == "Player")
        {
            // **Object.transform.GetComponent<Player>().Damage();** //Script Communication.
            //This is the GetComponent<>() method used to access the components of other Objects.
            //Here the Player Object gets stored in this Object parameter and we do "Object.transform" to access the root of the Player Object.
            //From there we access the component Player, meaning we access the class Player to access damage() method of Player script.

            Debug.Log(Object.tag + " Hit");

            //The below method is to store the Player component in a reference variable to do a null check for avoiding errors.
            if (_player != null)
            {
                _player.Damage(); //Calling Damage method using reference of class Player. i.e play --> refernce variable.
            }
            else
            {
                Debug.LogError("player is empty");
            }

            _collider.enabled = false;
            _enemyAud.clip = _enemyDestroy;
            _enemyAud.Play();

            _anim.SetTrigger("OnEnemyDestroy");
            Destroy(this.gameObject, 1.5f);
        }
        else if(Object.tag == "Player_Shield")
        {
            //comunicate with powerups function.
            _shield = GameObject.Find("Player_Shield").GetComponent<Shield_Function>();
            //We had kept this here and not in void start() because this object will be inactive at the start of the game and only when 
            //player collected shield power up this will be active.

            _player.AddScore(_points);
            Debug.Log(Object.tag + " Hit");

            if(_shield != null)
            {
                _shield.ShieldDamage();
            }
            else
            {
                Debug.LogError("shield is empty");
            }

            _enemyAud.clip = _enemyDestroy;
            _enemyAud.Play();
            _anim.SetTrigger("OnEnemyDestroy");
            Destroy(this.gameObject, 1.5f);
        }

    }
}
