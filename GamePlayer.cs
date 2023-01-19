using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GamePlayer : MonoBehaviour

{
    [Header("General")]
    //shpwing detail that speed is in ms^-1 and serializeField is because of passing the value to our desired place
    [Tooltip("In ms^-1")][SerializeField] public float xSpeed=15f;
    [Tooltip("In ms^-1")][SerializeField] public float ySpeed = 7f;

    [Header("Position Control Values")]
    //horizontal positive and negative range of our camera along the path in front of camera

    [Tooltip("In m")][SerializeField] public float xRange =  10f;
    [Tooltip("In m")][SerializeField] public float yRangeDown = 8f;
    [Tooltip("In m")][SerializeField] public float yRangeUp = 8f;

    //how much rotation on x axis with respect to move distance 1m in y axis

    [Header("Throw Based Values")]
    [SerializeField] public float pitchFactor=-5.14f ;
    [SerializeField] public float yawFactor = 5f;
    [SerializeField] public float rollFactor = -20f;

    [SerializeField] GameObject []guns;

    float xThrow,yThrow;
    float pitchThrowControlFactor=20f;
    // Start is called before the first frame update

    bool isPlayerDeade = false;

    

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerDeade)
        {
            //moving up down right and left
            Translation();
            //rotating around the scene for straight shot to enemy
            Rotation();
            firing();
        }

    }

    private void firing()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            enableFire();
        }
        else
        {
            disableFire();
        }
    }

    private void enableFire()
    {
       foreach(GameObject gun in guns)
        {
          var emissionModule=  gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = true;
        }
    }

    private void disableFire()
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false;
        }
    }

    private void onPlayerDeath()
    {
       isPlayerDeade= true;
    }


    private void Rotation()
    {
        // adding pitch factor value with original value
        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToxThrow = yThrow * pitchThrowControlFactor;

        float pitch = -109.447f +(pitchDueToPosition+ pitchDueToxThrow);
        float yaw = -5.518982f+(transform.localPosition.x*yawFactor);
        float roll= 7.222f+ rollFactor*xThrow;
        transform.localRotation= Quaternion.Euler(pitch,roll,yaw);
        
    }

    private void Translation()
    {
         xThrow= CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffsetThisFrame = xThrow * xSpeed * Time.deltaTime;
        // when key stroke are pressed offset value is retained and this value is added by using Vector3 and updating original position by previous position plus new offset retaining other axis values as they are Mathf.Clamp() simply clamping the value from whic our player(Jet) goes outside the camera path to stay put in our play area
        transform.localPosition = new Vector3(Mathf.Clamp(xOffsetThisFrame + transform.localPosition.x, -xRange, xRange), transform.localPosition.y, transform.localPosition.z);

         yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        // when key stroke are pressed offset value is retained and this value is added by using Vector3 and updating original position by previous position plus new offset retaining other axis values as they are Mathf.Clamp() simply clamping the value from whic our player(Jet) goes outside the camera path to stay put in our play area

        float yOffsetThisFrame = yThrow * ySpeed * Time.deltaTime;
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(yOffsetThisFrame + transform.localPosition.y, -yRangeDown, yRangeUp), transform.localPosition.z);
    }
}
