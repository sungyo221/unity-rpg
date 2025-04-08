using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public bool walkSoundCheck = false;
    public bool enemyBattleCheck = false;
    public GameObject fireBall;
    public GameObject iceRain;
    public GameObject fireRain;

    private void Update()
    {
        WalkSound();
        fireBall = GameObject.FindGameObjectWithTag("FireBall");
        iceRain = GameObject.FindGameObjectWithTag("IceRain");
        fireRain = GameObject.FindGameObjectWithTag("FireRain");
        if( fireBall != null ) audioSources[7] = fireBall.GetComponent<AudioSource>();
        if ( iceRain != null ) audioSources[8] = iceRain.GetComponent<AudioSource>();
        if(fireRain != null ) audioSources[9] = fireRain.GetComponent<AudioSource>();
    }

    public void WalkSound()
    {
        if(GameManager.GetInstance().player.iMove)
        {
            if(!walkSoundCheck)
            {
                audioSources[0].Play();
                walkSoundCheck = true;
            }            
        }
        else
        {
            if(walkSoundCheck)
            {
                audioSources[0].Stop();
                walkSoundCheck = false;
            }
        }
    }    
}
