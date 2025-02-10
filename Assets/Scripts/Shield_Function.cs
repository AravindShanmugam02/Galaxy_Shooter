using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Function : MonoBehaviour
{

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        transform.position = _player.transform.position;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShieldDamage()
    {
        //_player = GameObject.Find("Player").GetComponent<Player>();
        if (_player != null)
        {
            _player.Damage(); //Calling Damage method using reference of class Player. i.e play --> refernce variable.
        }
    }
}
