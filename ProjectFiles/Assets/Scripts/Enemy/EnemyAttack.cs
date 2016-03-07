using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{

    private GameObject player;
    private PlayerHealth playerHealth;
    private bool cooldown;
    private float cooldownTimer;
    private float cooldownWaitTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        cooldownWaitTime = 5f;
    }

    void Update(){
        if (cooldown){
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer > cooldownWaitTime)
            {
                cooldown = false;
            }
        }
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject == player){
            if (cooldown){

            }
            else {
                
            }
        }
    }
}