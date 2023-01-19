using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandeler : MonoBehaviour
{
    [Tooltip("in Seconds")][SerializeField] float levelDelay = 1f;
    [Tooltip("FX Sound and Particles")][SerializeField] GameObject deathFX;
    [Tooltip("Lose Screen")] [SerializeField] GameObject text;

    private void Start()
    {
        deathFX.SetActive(false);
        var text1 = text.GetComponent<Text>();
        text1.enabled= false;
    }
    private void StartDeathSequence()
    {
        SendMessage("onPlayerDeath");
        var text1 = text.GetComponent<Text>();
        text1.enabled = true;
        deathFX.SetActive(true);
        Invoke("reloadScene",levelDelay);
    }


    private void reloadScene()
    {
        SceneManager.LoadScene(1);
    }


    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

}
