using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Enemy Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public int waveSize = 5;
    public int totalToSpawn = 15;
    private int spawnedCount = 0;
    private int aliveCount = 0;

    [Header("UI Settings")]
    public Text waveStatusText;
    public string uiObjectName = "UI Sample";
    public float sceneChangeDelay = 5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // 파괴 불가 제거됨:
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int spawnThisWave = Mathf.Min(waveSize, totalToSpawn - spawnedCount);
        Debug.Log($"[GameManager] 웨이브 시작 - {spawnThisWave}마리 스폰");

        for (int i = 0; i < spawnThisWave; i++)
        {
            if (spawnedCount >= totalToSpawn) yield break;

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

            spawnedCount++;
            aliveCount++;

            UpdateWaveUI();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void OnEnemyKilled()
    {
        aliveCount--;
        UpdateWaveUI();

        if (spawnedCount < totalToSpawn && aliveCount <= 0)
        {
            StartCoroutine(SpawnWave());
        }

        if (spawnedCount >= totalToSpawn && aliveCount <= 0)
        {
            Debug.Log("[GameManager] 모든 적 처치 완료! 게임 클리어!");
            waveStatusText.text="모든 적을 처치했습니다. 메인으로 돌아갑니다.";
            StartCoroutine(ReturnToStartScene());
        }
    }

    public void OnPlayerCaught(Transform playerRoot)
    {
        Debug.Log("[GameManager] 플레이어 잡힘! UI 처리 중");

        Transform uiSample = FindChildRecursive(playerRoot, uiObjectName);
        if (uiSample != null)
        {
            uiSample.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[GameManager] UI Sample 찾을 수 없음!");
        }

        StartCoroutine(ReturnToStartScene());
    }

    IEnumerator ReturnToStartScene()
    {
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene("Start");
    }

    private Transform FindChildRecursive(Transform parent, string targetName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
                return child;

            Transform result = FindChildRecursive(child, targetName);
            if (result != null)
                return result;
        }
        return null;
    }

    private void UpdateWaveUI()
    {
        if (waveStatusText == null) return;

        int safeWaveSize = Mathf.Max(1, waveSize);
        int safeTotalToSpawn = Mathf.Max(1, totalToSpawn);

        int totalWave = Mathf.CeilToInt((float)safeTotalToSpawn / safeWaveSize);
        int currentWave = spawnedCount / safeWaveSize;

        waveStatusText.text = $"웨이브 {currentWave}/{totalWave}\n남은 거인수: {aliveCount}";
    }
}
