using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private float _speed = 20;

    Vector3 rotateDirection = new Vector3(0, 0, 1); //or we can do like Vector3 rotateDirection = Vector3.forward;

    private SpawnManager _spawnManager;

    private UIManager _UI;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _UI = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null || _UI == null)
        {
            Debug.LogError("_spawnManager or _UI is empty in Asteroid");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateDirection * _speed * Time.deltaTime);
        _UI.StartWaveTextEnabled();
    }

    private void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.tag == "Fire_Laser")
        {
            Destroy(Object.gameObject); //This destroyes the game object detected in "Object" parameter.
            Debug.Log(Object.tag + " Hit Asteroid");

            Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            _UI.StartWaveTextDisabled();
            _spawnManager.StartWave();
        }
    }
}
