using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    public event Action<EnemyHealth> Spawned;
    public event Action EnemyDead;

    [SerializeField] private EnemyFightStateMachine _enemyPrefab;
    [SerializeField] private List<EnemyData> _enemyTypes;
    [SerializeField] private Transform _spawnPoint;

    private EnemyFightStateMachine _enemy;

    public EnemyFightStateMachine Spawn()
    {
        EnemyData enemyData = GetRandomEnemyDataByChance();

        _enemy = Instantiate(_enemyPrefab, transform);

        _enemy.InitializeEnemyData(enemyData);

        EnemyHealth enemyHealth = _enemy.GetComponent<EnemyHealth>();

        enemyHealth.SetEnemyData(enemyData);

        enemyHealth.Dead += OnEnemyDeath;

        Spawned?.Invoke(enemyHealth);

        return _enemy;
    }

    public void Despawn()
    {
        Destroy(_enemy.gameObject);
        _enemy = null;
    }

    private EnemyData GetRandomEnemyDataByChance()
    {
        if (_enemyTypes == null || _enemyTypes.Count() == 0)
            throw new NullReferenceException("Enemy pool is empty");

        List<float> _realEnemySpawnChances = new();
        float chancesSum = _enemyTypes.Sum(chance => chance.SpawnChance);

        foreach (EnemyData enemy in _enemyTypes)
        {
            _realEnemySpawnChances.Add(enemy.SpawnChance / chancesSum + (_realEnemySpawnChances.Count() != 0 ? _realEnemySpawnChances.Last() : 0));
        }

        Random random = new();

        var randomValue= random.NextDouble();


        for (int i = 0; i < _realEnemySpawnChances.Count(); i++)
        {
            if (randomValue < _realEnemySpawnChances[i])
            {
                return _enemyTypes[i];
            }
        }
        return _enemyTypes.Last();
    }

    private void OnEnemyDeath()
    {
        EnemyDead?.Invoke();
    }

}

