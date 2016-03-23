using UnityEngine;
using System.Collections;

public class EnemyAnimAttack : MonoBehaviour {

    private Char_Movement player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Char_Movement>();
    }

    public void AttackDone() {
        player.killPlayer();
    }

}
