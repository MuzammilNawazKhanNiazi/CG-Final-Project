using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cittari : MonoBehaviour
{
   
    ScoreBoard scoreBoard;
    [SerializeField] GameObject enemyDeath;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 33;
    [SerializeField] int hits = 10;

    // Start is called before the first frame update
    [Obsolete]
    private void Start()
    {
        addNonTriggerBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void addNonTriggerBoxCollider()
    {
      Collider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.hitScore(scorePerHit);
        hits--;
        if (hits <= 0)
        {
            KillEnemy();
        }
        
    }

    private void KillEnemy()
    {
        GameObject fxClone = Instantiate(enemyDeath, transform.position, Quaternion.identity);
        fxClone.transform.parent = parent;
        Destroy(gameObject);
    }
}
