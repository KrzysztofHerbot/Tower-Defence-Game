using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 6;
    [SerializeField] int difficultyRamp = 1;
    [SerializeField] int scoreReward = 10;
    Bank bank;
    public int currentHealth;
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathfinder;

    private void Awake()
    {
        bank = FindObjectOfType<Bank>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }
    void OnEnable()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    void ProcessHit()
    {
        currentHealth--;
        if (currentHealth < 1)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<Transform>().position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
            enemy.RewardGold();
            maxHealth = maxHealth + difficultyRamp;
            bank.AddScore(scoreReward);
        }
    }
}
