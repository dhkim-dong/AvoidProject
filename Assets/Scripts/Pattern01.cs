using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern01 : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxCount;
    [SerializeField] private float spawnCycle;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemys());
    }

    private void OnDisable()
    {
        {
            StopCoroutine(nameof(SpawnEnemys));
        }
    }

    private IEnumerator SpawnEnemys()
    {
        float waitTime = 1;
        yield return new WaitForSeconds(waitTime);

        int count = 0;

        while (count < maxCount)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            Vector3 pos = new Vector3(Random.Range(Constrants.min.x, Constrants.max.x), Constrants.max.y, 0);
            Instantiate(enemyPrefab, pos,Quaternion.identity);

            yield return new WaitForSeconds(spawnCycle);

            count++;
        }

        // 패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
