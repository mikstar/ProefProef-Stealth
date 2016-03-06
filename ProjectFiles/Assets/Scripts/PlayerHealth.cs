using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float health = 100f;
    public float resetAfterDeathTime = 5f;
    public AudioClip deathSound;

    public bool death = false;

    void Update() {
        Debug.Log(health);
    }
}
