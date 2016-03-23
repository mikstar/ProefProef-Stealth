using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

    public float fadeSpeed = 2f;
    public float highIntens = 2f;
    public float lowIntenst = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn;

    private float targetIntensity;
    private Light light;

    void Awake()
    {
        light = GetComponent<Light>();
        light.intensity = 0f;

        targetIntensity = highIntens;
    }

    void Update() {
        if (alarmOn)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0f, fadeSpeed * Time.deltaTime);

        }
    }

    void CheckTargetIntens() {
        if (Mathf.Abs(targetIntensity - light.intensity) < changeMargin) {
            if (targetIntensity == highIntens)
            {
                targetIntensity = lowIntenst;
            }
            else {
                targetIntensity = highIntens;
            }
        }
    }
}
