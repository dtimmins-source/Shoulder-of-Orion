using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField] bool isPlayer;
  [SerializeField] int health = 50;
  [SerializeField] int score = 50;
  [SerializeField] ParticleSystem hitEffect;
  [SerializeField] bool applyCameraShake;
  CameraShake cameraShake;
  AudioPlayer audioPlayer;
  ScoreKeeper scoreKeeper;
  LevelManager levelManager;

  void Awake()
  {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
  }

  public int GetHealth()
  {
        return health;
  }

  private void OnTriggerEnter2D(Collider2D other) 
  {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
  }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
         if(!isPlayer)
         {
            scoreKeeper.ModifyScore(score);
            Debug.Log(scoreKeeper.GetScore());
         }
         else
         {
            levelManager.LoadGameOver();
         }         
                           
        Destroy(gameObject);
            
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
