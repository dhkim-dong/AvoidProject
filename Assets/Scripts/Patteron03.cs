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
        // 패턴 시작전 대기하는 시간
        yield return new WaitForSeconds(1);

        // 경고 이미지 활성

        waringImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        waringImage.SetActive(false);

        // 폭탄 오브젝트 활성화 및 이동
        yield return StartCoroutine(nameof(MoveUp));

        // 폭탄 이팩트 활성
        boomEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        boomEffect.SetActive(false);

        // 패턴 비활성화
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
