using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _asteroid;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _PowerupContainer;
    [SerializeField]
    private GameObject _TripleShotPowerup;
    [SerializeField]
    private GameObject _SpeedBoostPowerup;
    [SerializeField]
    private GameObject _ShieldPowerup;
    //[SerializeField]
    //private GameObject[] Powerups; //This is how we create Gameobject array which stores multiple game objects.

    private bool isPlayerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Enemy()); //TO Start the Coroutine.
        //This is not placed in Update method because Update method os called once per every frame.
        //Hence it is plced in Start method to start it once and it will keep on running as it's a coroutine.

        //StartCoroutine(Powerup());

        SpawnAsteroid();
    }
    //We create a new method like below in order to control the Wave start. Thus we remove the Coroutine from void Start().
    public void StartWave()
    {
        StartCoroutine(Enemy());
        StartCoroutine(Powerup());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnAsteroid()
    {
        Vector3 asteroidSpawnPosition = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(5.00f, 3.00f), 0);
        Instantiate(_asteroid, asteroidSpawnPosition, Quaternion.identity);
    }

    IEnumerator Enemy() //IEnumerator is a method type. IEnumerator is used to declare a Coroutine method.
    {
        yield return new WaitForSeconds(3.0f);

        while(isPlayerAlive == true) //Infinite loop to make coroutine function endlessly. Infinite loops are usually used for coroutines.
            //Infinite loops can even crash system.
        {
            Vector3 enemySpawnPosition = new Vector3(Random.Range(-9.0f, 9.0f), 7.36f, 0);

            GameObject _newEnemy = Instantiate(_enemy, enemySpawnPosition, Quaternion.identity); //To Spawn enemy objects.
            //GameObject _newEnemy is to store the instantiated GameObject _enemy.

            //Now using the _newEnemy GameObject which has the instantiated GameObject _enemy.
            _newEnemy.transform.parent = _enemyContainer.transform;
            //We are passing _enemyContainer.transform as value to the -newEnemy.transform.parent.
            //Because the _enemy GameObject which is stored in GameObject _newEnemy after instantiation will become child for GameObject _enemyContainer.

            yield return new WaitForSeconds(3.0f); //yield simply used to control the Coroutine.
            //Here it says to wait for 5 seconds before executing again.
        }
    }

    IEnumerator Powerup()
    {
        yield return new WaitForSeconds(6.0f);
        while (isPlayerAlive == true)
        {

            int x = Random.Range(0, 3);

            //Using switch is very good way to switch between random powerups.
            //But if we have like say 100 powerups we can't go n define separate cases unless n until the powerup is unique with it's behaviour.
            switch(x)
            {
                case 0:
                    Vector3 TripleShotPowerupSpawnPosition = new Vector3(Random.Range(-9.0f, 10.0f), 7.36f, 0);

                    GameObject _NewTripleShot = Instantiate(_TripleShotPowerup, TripleShotPowerupSpawnPosition, Quaternion.identity);

                    _NewTripleShot.transform.parent = _PowerupContainer.transform;

                    yield return new WaitForSeconds(Random.Range(18.0f, 26.0f));
                    break;

                case 1:
                    Vector3 SpeedBoostPowerupSpawnPosition = new Vector3(Random.Range(-9.0f, 10.0f), 7.36f, 0);

                    GameObject _NewSpeedBoost = Instantiate(_SpeedBoostPowerup, SpeedBoostPowerupSpawnPosition, Quaternion.identity);

                    _NewSpeedBoost.transform.parent = _PowerupContainer.transform;

                    yield return new WaitForSeconds(Random.Range(18.0f, 26.0f));
                    break;

                case 2:
                    Vector3 ShieldPowerupSpawnPosition = new Vector3(Random.Range(-9.0f, 10.0f), 7.36f, 0);

                    GameObject _NewShield = Instantiate(_ShieldPowerup, ShieldPowerupSpawnPosition, Quaternion.identity);

                    _NewShield.transform.parent = _PowerupContainer.transform;

                    yield return new WaitForSeconds(Random.Range(18.0f, 26.0f));
                    break;

                default:
                    Debug.Log("No Powerup");
                    break;

            }

//Hence another way to do is by using GameObject array and keeping multiple powerups under that. One draw back in that is if a array
//element is called but no object is assigned to it, Unity will throw an error. But with Switch in that situation default case will
//be called.

//Just for our knowledge, using array:

            //Vector3 SpeedBoostPowerupSpawnPosition = new Vector3(Random.Range(-9.0f, 10.0f), 7.36f, 0);
            //int y = Random.Range(0, 3); // Random.Range() starts with min value 0 and reaches below the maxvalue 3. that is 0 to 2.
            //Instantiate(Powerups[y], SpeedBoostPowerupSpawnPosition, Quaternion.identity);


        }
    }

    public void PlayerDies()
    {
        isPlayerAlive = false;
    }
}
