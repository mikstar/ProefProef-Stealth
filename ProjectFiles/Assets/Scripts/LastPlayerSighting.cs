using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour {

    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);

    public float musicFadeSpeed = 1f;

    private AlarmLight alarm;
    private Light mainLight;
    private AudioSource[] sirens;
    private AudioSource audio;
    private AudioSource alarmAudio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        alarmAudio = transform.Find("secondaryMusic").GetComponent<AudioSource>();

        alarm = GameObject.FindGameObjectWithTag("Alarm").GetComponent<AlarmLight>();

        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag("Siren");
        sirens = new AudioSource[sirenGameObjects.Length];

        for (int i = 0; i < sirens.Length; i++) {
            sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        SwitchAlarms();
        MusicFading();
    }

    void SwitchAlarms()
    {
        Debug.Log(position);
        alarm.alarmOn = (position != resetPosition);

        for (int i = 0; i < sirens.Length; i++)
        {
           
            if (position != resetPosition && !sirens[i].isPlaying)
                sirens[i].Play();
        
            else if (position == resetPosition)
                sirens[i].Stop();
        }
    }

 
    void MusicFading ()
    { 
            if (position != resetPosition)
            {
                audio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);
                alarmAudio.volume = Mathf.Lerp(alarmAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            }
            else
            {
                audio.volume = Mathf.Lerp(audio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
                alarmAudio.volume = Mathf.Lerp(alarmAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
    
}
