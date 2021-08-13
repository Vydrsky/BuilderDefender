using UnityEngine;
using System.Collections.Generic;
using System;

public class EnemyWaveManager : MonoBehaviour {

    /************************ FIELDS ************************/

    public event EventHandler OnWaveNumberChanged;

    private enum State {
        WaitingToSpawnNextWave,
        SpawningWave
    }
    [SerializeField] List<Transform> spawnPositionTransformList;
    [SerializeField] Transform nextWaveSpawnPositionTransform;
    State state;
    private float nextWaveSpawnTimer;
    private float nextEnemySpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;
    private int waveNumber;

    /************************ INITIALIZE ************************/
    private void Awake() {
        waveNumber = 0;
    }

    private void Start() {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
    }

    /************************ LOOPING ************************/
    private void Update() {
        switch (state) {
            case State.WaitingToSpawnNextWave: {
                    nextWaveSpawnTimer -= Time.deltaTime;
                    if (nextWaveSpawnTimer <= 0f) {
                        SpawnWave();
                    }
                    break;
                }
            case State.SpawningWave: {
                    if (remainingEnemySpawnAmount > 0) {
                        nextEnemySpawnTimer -= Time.deltaTime;
                        if (nextEnemySpawnTimer <= 0f) {
                            nextEnemySpawnTimer = UnityEngine.Random.Range(0f, 0.2f);
                            Enemy.Create(spawnPosition + Utilities.GetRandomDirection() * UnityEngine.Random.Range(0f, 10f));
                            remainingEnemySpawnAmount--;
                        }
                        if(remainingEnemySpawnAmount <= 0) {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = 10f;
                        }
                    }
                    break;
            }
        }
        
        
    }

    /************************ METHODS ************************/

    private void SpawnWave() {
        
        remainingEnemySpawnAmount = 5 + 3*waveNumber;
        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaveNumber() {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimer() {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition() {
        return spawnPosition;
    }
}