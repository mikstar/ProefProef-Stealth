using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public bool playerInRange;

    private GameObject player;
    private PlayerHealth playerHealth;
    private bool cooldown;
    private float cooldownTimer;
    private float cooldownWaitTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update(){
     
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject == player){
            playerInRange = true;
            
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

}