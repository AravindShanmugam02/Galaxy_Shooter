using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Event_Trigger : MonoBehaviour
{

    private AudioSource _event_Trigger_Audio;

    private GameObject _mainMenuAudioManager;
    private Transform _mainMenuAudioManagerChild1transform;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuAudioManager = GameObject.Find("Audio_Manager_MainMenu");

        if (_mainMenuAudioManager == null)
        {
            Debug.LogError("_mainMenuAudioManager in UI_Event_Trigger is empty!!");
        }
        else
        {
            _mainMenuAudioManagerChild1transform = _mainMenuAudioManager.transform.GetChild(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverSound()
    {

        _event_Trigger_Audio = _mainMenuAudioManagerChild1transform.GetComponent<AudioSource>();

        if (_event_Trigger_Audio.clip == null)
        {
           Debug.LogError("Event_Trigger_Audio in UI_Event_Trigger is empty!!");
        }
        else
        {
            _event_Trigger_Audio.Play();
        }
    }
}
