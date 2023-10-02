using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern07 : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject[] waringImages;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int setCount;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

        // ������ Ȱ��ȭ�ϰ� 1�� ���
        ground.SetActive(true);
        yield return new WaitForSeconds(1f);

        // �ϴ� - �ߴ� - ��� ���������� ������ ����
        int[] numbers = new int[3] { 0, 1, 2 };
        yield return StartCoroutine(SpawnPrefabSet(numbers, 0.5f, 1));

        // ? - ? - ? ���� ������ ����

        int count = 0;
        while( count < setCount)
        {
            // 0 ~2 ���� ���ڸ� numbers�� ����
            numbers = Utils.RandomNumbers(3, 3);

            yield return StartCoroutine(SpawnPrefabSet(numbers, 0.5f, 1));

 
            count++;
        }

        ground.SetActive(false);

        // ���� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPrefabWithWaring(int index, float waitTime)
    {
        waringImages[index].SetActive(true);
        yield return new WaitForSeconds(waitTime);
        waringImages[index].SetActive(false);

        audioSource.Play();

        int spawnType = UnityEngine.Random.Range(0, 2);

        int prefabIndex = UnityEngine.Random.Range(0, prefabs.Length);

        Vector3 postion = new Vector3(spawnType == 0 ? Constrants.min.x : Constrants.max.x, waringImages[index].transform.position.y);

        GameObject clone = Instantiate(prefabs[prefabIndex], postion, Quaternion.identity);

        clone.GetComponent<MovementTransform2D>().MoveTo(spawnType == 0 ? Vector3.right : Vector3.left);
    }

    private IEnumerator SpawnPrefabSet(int[] numbers, float delayTime, float endofWaitTime = 1)
    {
        int index = 0;
        while (index < numbers.Length)
        {
            StartCoroutine(SpawnPrefabWithWaring(numbers[index], delayTime));

            yield return new WaitForSeconds(delayTime * 0.5f);

            index++;
        }

        yield return new WaitForSeconds(1f);
    }

}
