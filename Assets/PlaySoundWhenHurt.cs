using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlaySoundWhenHurt : MonoBehaviour 
{
    [SerializeField] private AudioSource hurtSound;
    private Health health;

    // Use this for initialization
    void Start () 
    {
        health = GetComponent<Health>();
        health.damageTakenEvent.AddListener(onHurt);
    }

    private void onHurt()
    {
        if(hurtSound != null)
        {
            hurtSound.Play();
        }
    }
}
