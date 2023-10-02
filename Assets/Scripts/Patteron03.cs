using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patteron03 : MonoBehaviour
{
    [SerializeField] private GameObject waringImage;
    [SerializeField] private Transform boom;
    [SerializeField] private GameObject boomEffect;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        boom.GetComponent<MovingEntity>().Reset();

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // ���� ������ ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        // ��� �̹��� Ȱ��

        waringImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        waringImage.SetActive(false);

        // ��ź ������Ʈ Ȱ��ȭ �� �̵�
        yield return StartCoroutine(nameof(MoveUp));

        // ��ź ����Ʈ Ȱ��
        boomEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        boomEffect.SetActive(false);

        // ���� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    private IEnumerator MoveUp()
    {
        float boomDestinationY = 0;

        boom.gameObject.SetActive(true);

        while (true)
        {
            if(boom.transform.position.y >= boomDestinationY)
            {
                boom.gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
