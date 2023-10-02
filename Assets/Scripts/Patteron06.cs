using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Patteron06 : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject waringImage;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnCycle;
    [SerializeField] private int maxCount;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1f);

        int count = 0;

        while(count < maxCount)
        {
            StartCoroutine(nameof(Spawnprefab));

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }
       

        // 패턴 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator Spawnprefab()
    {
        GameObject waringClone = Instantiate(waringImage, playerTransform.position, Quaternion.identity);
        waringClone.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.5f);

        GameObject prefabClone = Instantiate(prefab, waringClone.transform.position, Quaternion.identity);
        Destroy(waringClone);
        Destroy(prefabClone, 0.5f);
    }
}
