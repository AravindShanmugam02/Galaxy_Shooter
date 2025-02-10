using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Variales are declared here -->
    // 1. Public[available for everywhere i.e. for other class/objects] or Priavte[only avaliable for Player class/object] reference.
    // 2. Data type of variables [int, float, bool, string]
    // 3. every variable has a name.
    // 4. optional value assigned.
    [SerializeField]
    private float _speed = 5.0f;  //Public variable are visible in the inspector panel. Private variables won't be seen there.
    //Inspector can override the values of the variables in a script. Keeping variables private is basic thing, then we can change it to public accordingly.
    //private variables are prefixed with _ which is a best practise. When you need the variable to be private and to be visible in inspector -- We go with Attibutes.
    //Attributes to be mentioned above the varibales. here we are gonna use [SerializeField]. It will serialize the varibale and it can be modified in the inspector.

    //[SerializeField]
    //private float _speedBoost = 8.0f; //Used for speed powerup

    //A varibale to get the default 0,0,0 position.
    Vector3 startPosition = new Vector3(0, 0, 0);

    //Firerate varibale:
    [SerializeField]
    private float _fireRate = 0.3f;
    private float _dummy = 0.0f;

    [SerializeField]
    private int _lives = 3;

    //To access SpawnManager to tell about player lives:
    private SpawnManager _spawnManager;

    //To access UIManager to tell Score:
    private UIManager _UI;

    //To activate Tripleshot
    //[SerializeField] //for testing making it serializefield
    private bool _IsTripleShotActive = false;

    //[SerializeField]
    private bool _IsSpeedBoostActive;

    //[SerializeField]
    private bool _IsShieldActive = false;


    //GameOnject Variable to use Prefab
    [SerializeField]
    private GameObject _fire_Laser; //Here the GameObject varibale is _fire_Laser.

    //To store TripleShot Prefab
    [SerializeField]
    private GameObject _triple_Shot;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private GameObject _rDamage;

    [SerializeField]
    private GameObject _lDamage;

    [SerializeField]
    private AudioClip _laserSoundClip;

    [SerializeField]
    private AudioClip _explosionSoundClip;

    [SerializeField]
    private AudioClip _damageSoundClip;

    private AudioSource _audioSourcePlayer;

    private PolygonCollider2D _collider;

    private bool _isPlayerAlive;

    private Animator _playerAnimator;

    [SerializeField]
    private GameObject _explosion;

    private GameObject _thruster;

    // Start is called before the first frame update
    void Start()
    {

        //To set the player in new position = (0, 0, 0)
        transform.position = startPosition;

        //Setting Player to be alive
        _isPlayerAlive = true;

        //To access SpawnManager:
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _UI = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSourcePlayer = GetComponent<AudioSource>();
        _shield = GameObject.Find("Player_Shield");
        _rDamage = GameObject.Find("Right_Engine_Damage");
        _lDamage = GameObject.Find("Left_Engine_Damage");
        _collider = GetComponent<PolygonCollider2D>();
        _playerAnimator = GetComponent<Animator>();
        _thruster = GameObject.Find("Thruster");


        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager in player is empty");
        }
        

        if (_UI == null)
        {
            Debug.LogError("_UI in player is empty");
        }
        else
        {
            _UI.LivesSystem(_lives); //to displaye the initial lives of the player.
        }


        if (_audioSourcePlayer == null)
        {
            Debug.LogError("AudioSource in player is empty");
        }

        if (_shield == null)
        {
            Debug.LogError("_shield in player is empty");
        }

        if (_rDamage == null || _lDamage == null)
        {
            Debug.LogError("rDamage or lDamage is empty in Player");
        }
        else
        {
            _rDamage.SetActive(false);
            _lDamage.SetActive(false);
        }

        if (_collider == null)
        {
            Debug.LogError("_collider is empty in Player");
        }

        if (_playerAnimator == null)
        {
            Debug.LogError("_playerAnimator is empty in Player");
        }

        if (_thruster == null)
        {
            Debug.LogError("_thruster in player is empty!");
        }
        else
        {
            _thruster.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPlayerAlive == false)
        {
            Debug.Log("Player is out of lives!!!");
        }
        else
        {
            Movement();
        #if UNITY_ANDROID

            if(CrossPlatformInputManager.GetButtonDown("Fire") && Time.time >= _dummy)
            {
                Firelaser();
            }

        #elif UNITY_IOS

            if(CrossPlatformInputManager.GetButtonDown("Fire") && Time.time >= _dummy)
            {
                Firelaser();
            }

        #else

            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _dummy)
            {
                Firelaser();
            }

        #endif
        }
    }

    void Movement()
    {
        //declaring varibale to get user input.

#if UNITY_ANDROID

        float rightleftinput =  CrossPlatformInputManager.GetAxis("Horizontal");
        float updowninput = CrossPlatformInputManager.GetAxis("Vertical");

        Debug.Log("Axis value:" + rightleftinput);

        switch (rightleftinput)
        {
            case 0:
                _playerAnimator.SetBool("OnPlayerTurnRight", false);
                _playerAnimator.SetBool("OnPlayerTurnLeft", false);
                break;

            default:
                if(rightleftinput > 0)
                {
                    _playerAnimator.SetBool("OnPlayerTurnRight", true);
                    _playerAnimator.SetBool("OnPlayerTurnLeft", false);
                }
                else if(rightleftinput < 0)
                {
                    _playerAnimator.SetBool("OnPlayerTurnLeft", true);
                    _playerAnimator.SetBool("OnPlayerTurnRight", false);
                }
                break;
        }

#elif UNITY_IOS

        float rightleftinput =  CrossPlatformInputManager.GetAxis("Horizontal");
        float updowninput = CrossPlatformInputManager.GetAxis("Vertical");

        switch(rightleftinput)
        {
            case 0:
                _playerAnimator.SetBool("OnPlayerTurnRight", false);
                _playerAnimator.SetBool("OnPlayerTurnLeft", false);
                break;

            default:
                if(rightleftinput > 0)
                {
                    _playerAnimator.SetBool("OnPlayerTurnRight", true);
                    _playerAnimator.SetBool("OnPlayerTurnLeft", false);
                }
                else if(rightleftinput < 0)
                {
                    _playerAnimator.SetBool("OnPlayerTurnLeft", true);
                    _playerAnimator.SetBool("OnPlayerTurnRight", false);
                }
                break;
        }

#else

        float rightleftinput = Input.GetAxis("Horizontal"); //Where the string Horizontal refers to the name of the "Name" Field in the InputManager -> Axes.
        float updowninput = Input.GetAxis("Vertical"); //Where the string Vertical refers to the name of the "Name" field in the InputManager -> Axes.

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerAnimator.SetBool("OnPlayerTurnRight", true);
            _playerAnimator.SetBool("OnPlayerTurnLeft", false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _playerAnimator.SetBool("OnPlayerTurnLeft", true);
            _playerAnimator.SetBool("OnPlayerTurnRight", false);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _playerAnimator.SetBool("OnPlayerTurnRight", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _playerAnimator.SetBool("OnPlayerTurnLeft", false);
        }

#endif

        //To move the player(translate the position)
        // **transform.Translate(Vector3.right);** //Also can be coded as transform.Translate(new Vector3(1, 0, 0)); --> As both are same
        //Just coding like above will move the player very fast meaning --> One unit in Unity means 1 meter in real world. So 60fps --> 60mts/sec in real world.

        //Now we'll use the below to normalize the speed.
        // **transform.Translate(Vector3.right * rightleftinput * _speed * Time.deltaTime);** //deltaTime refers to real time -- Per Sec. Now we are moving player in (1, 0, 0) * 5 * Per Sec = 5mts/sec.
        // **transform.Translate(Vector3.up * updowninput * _speed * Time.deltaTime);**

        //More optimal way is as follows:

        //So instead of using two lines for translating position, we could come up with one line code:
        // **transform.Translate(new Vector3(rightleftinput, updowninput, 0) * _speed * Time.deltaTime);**

        //but instead of using this "new vector3(x, y, z)", we can make it more optimal by making a variable to get Vector3 input for updown and rightleft:
        Vector3 direction = new Vector3(rightleftinput, updowninput, 0);

            //if(_IsSpeedBoostActive == true)
            //{
            //    transform.Translate(direction * _speedBoost * Time.deltaTime);
            //}
            //else
            //{
            //   transform.Translate(direction * _speed * Time.deltaTime);
            //}

            //The above way(Practice) of changing the speed is correct,
            //but if we need to see the speed variable to get increased in realtime when we collect the powerup:

            transform.Translate(direction * _speed * Time.deltaTime);

            //now go see the IsSpeedBoostActive() method.


            //*************************************************************** THIS PART IS NOT REQUIRED ******************************************
            //to reset the position to start point using a key.
            // **string reset = "r";**
            // **bool resetboolinput = Input.GetKey(reset);**
            //you can use the below method too
            // **bool resetboolinput = Input.GetKey(KeyCode.R);**
            // **if (resetboolinput == true)**
            // **{**
            //  **transform.position = startPosition;**
            // **}**

            //else you can use the below for more optimal way:
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    transform.position = startPosition;
            //    Debug.Log("Reset Successfull !!!"); //Debug.log will print the message to the console.
            //}
            //**************************************************************************************************************************************

            //psuedocode is a good practise.
            //To keep boundaries for the player
            float bottombound = -5.198082f;
            float topbound = -2.157711f;
            //if(transform.position.y <= bottombound)
            //{
            //  transform.position = new Vector3(transform.position.x, bottombound, 0);
            //}
            //else if(transform.position.y >= topbound)
            //{
            //  transform.position = new Vector3(transform.position.x, topbound, 0);
            //}

            float leftbound = -9.5f;
            float rightbound = 9.5f;
            if (transform.position.x <= leftbound)
            {
                transform.position = new Vector3(leftbound, transform.position.y, 0);
            }
            else if (transform.position.x >= rightbound)
            {
                transform.position = new Vector3(rightbound, transform.position.y, 0);
            }

            //There is another way to make this boundaries code optimal:
            //Here we make use of ---> int/float Mathf.Clamp(float/int value, float/int min, float/int max) in the place of y to give the value and it's max and min limit.
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, bottombound, topbound), 0);

            //IT'S OKAY IF WE USE IF condition also.
    }

    void Firelaser()
    {
        Vector3 firelaseroffsetposition = new Vector3(0, 1.0f, 0);
        

        //OffSet positions for Tripleshot -- For each single laser
        Vector3 tripleshotoffsetposition0 = new Vector3(0, 1.0f, 0);
        _triple_Shot.transform.GetChild(0).position = tripleshotoffsetposition0;

        Vector3 tripleshotoffsetposition1 = new Vector3(0.783f, -0.349f, 0);
        _triple_Shot.transform.GetChild(1).position = tripleshotoffsetposition1;

        Vector3 tripleshotoffsetposition2 = new Vector3(-0.783f, -0.349f, 0);
        _triple_Shot.transform.GetChild(2).position = tripleshotoffsetposition2;



        if (_IsTripleShotActive == true) //&& i < poweruptime)
        {
            //Vector3 child1offsetposition = new Vector3(-0.784f, -0.372f, 0);
            //Vector3 child2offsetposition = new Vector3(0.784f, -0.372f, 0);
            //for (i = 0.0f * Time.deltaTime; i < poweruptime; i++)
            //{
            //        Instantiate(_triple_Shot.transform.GetChild(0), transform.position + firelaseroffsetposition, Quaternion.identity);
            //        Instantiate(_triple_Shot.transform.GetChild(1), transform.position + child1offsetposition, Quaternion.identity);
            //        Instantiate(_triple_Shot.transform.GetChild(2), transform.position + child2offsetposition, Quaternion.identity);
            //        _dummy = Time.time + _fireRate;
            //}
            //if(i != 0)
            //{
            //Debug.Log("TripleShot over");
            //IsTripleShotActive = false;
            //}

            //much simpler way is as below

            Instantiate(_triple_Shot, transform.position, Quaternion.identity);

            _audioSourcePlayer.clip = _laserSoundClip;
            _audioSourcePlayer.Play();

            _dummy = Time.time + _fireRate;

        }
        else
        {
            //Instantiate() is used to create Objects at runtime and to clone objects.
            //Those objects are usually the prefabs. Prefabs are used to create duplicates.
            Instantiate(_fire_Laser, transform.position + firelaseroffsetposition, Quaternion.identity); //Quaternion is a type used to represent rotation. identity gives the rotation.
                                                                                                         //The above offset vector3 variable is to off set the spawning position of the Fire_Laser.

            _audioSourcePlayer.clip = _laserSoundClip;
            _audioSourcePlayer.Play();

            //Incrementation of _dummy to give the fireRate delay
            _dummy = Time.time + _fireRate;
        }
    }

    public void Damage()
    //The reason why this is public is we need to call this class from another script to reduce the lives based on the Collision.
    {
        if(_IsShieldActive == true)
        {
            Debug.Log("Shield Active");
            return;
        }
        else
        {
            _lives--;

            if (_lives >= 0)
            {
                _audioSourcePlayer.clip = _damageSoundClip;
                _UI.LivesSystem(_lives); //to update the remaining lives display in UIManager.

                if (_lives == 2)
                {
                    _audioSourcePlayer.Play();
                }

                if (_lives == 1)
                {
                    _rDamage.SetActive(true);
                    _audioSourcePlayer.Play();
                }
                if (_lives == 0)
                {
                    _lDamage.SetActive(true);
                    _audioSourcePlayer.Play();
                }
            }

            if (_lives == -1)
            {
                _audioSourcePlayer.clip = _explosionSoundClip;
                _audioSourcePlayer.Play();
                _collider.enabled = false;
                _isPlayerAlive = false;
                _playerAnimator.SetTrigger("OnPlayerDestroy");

                _lDamage.SetActive(false);
                Instantiate(_explosion, _lDamage.transform.position, Quaternion.identity);
                _rDamage.SetActive(false);
                Instantiate(_explosion, _rDamage.transform.position, Quaternion.identity);
                _thruster.SetActive(false);


                Destroy(this.gameObject,3.0f);
                _spawnManager.PlayerDies();

                _UI.SetBestScoreRecord();
                _UI.GameOverSequence();

                Debug.Log("Player Destroyed");
            }
            Debug.Log("Remaining Life/Lives: " + _lives);
        }
    }

    public void TripleShotActive()
    {
        _IsTripleShotActive = true;
        StartCoroutine(TripleShotTimeout());
    }

    IEnumerator TripleShotTimeout()
    {
        yield return new WaitForSeconds(5.0f);
        _IsTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _IsSpeedBoostActive = true;
        //We don't need this above variable to see the real time speed change in the _speed varibale. But keeping it just incase.
        _speed = (_speed * 2) - 2;
        StartCoroutine(SpeedBoostTimeout());
    }

    IEnumerator SpeedBoostTimeout()
    {
        yield return new WaitForSeconds(6.0f);
        _IsSpeedBoostActive = false;
        _speed = (_speed / 2) + 1;
    }

    public void ShieldActive()
    {
        _IsShieldActive = true;
        _shield.SetActive(true);
        StartCoroutine(ShieldTimeout());
    }

    IEnumerator ShieldTimeout()
    {
        yield return new WaitForSeconds(10.0f);
        _IsShieldActive = false;
        _shield.SetActive(false);
    }

    //Score System
    public void AddScore(int points)
    {
        _UI.ScoreSystem(points);
    }
}