using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Pattern04 : MonoBehaviour
{
    [SerializeField] private MovementTransform2D boss;
    [SerializeField] private GameObject bossProjectile;
    [SerializeField] private float attackRate = 1;
    [SerializeField] private int maxFireCount = 5;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        boss.GetComponent<MovingEntity>().Reset();
        boss.ResetSpeed();
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1f);

        // 보스 아래 이동

        yield return StartCoroutine(nameof(MoveDown));

        // 보스 좌우 이동
        StartCoroutine(nameof(MoveLeftAndRight));
        // 보스 공격
        int count = 0;
        while( count < maxFireCount)
        {
            CircleFire();

            count++;

            yield return new WaitForSeconds(attackRate);
        }
        // 보스 오브젝트 비활성화
        boss.gameObject.SetActive(false);

        // 패턴 비활성화
        gameObject.SetActive(false);
    }

    IEnumerator MoveDown()
    {
        float bossDestinationY = 1.5f;

        boss.gameObject.SetActive(true);

        while (true)
        {
            if(boss.transform.position.y <= bossDestinationY)
            {
                boss.MoveTo(Vector3.zero);

                yield break;
            }
            yield return null;
        }
    }

    IEnumerator MoveLeftAndRight()
    {
        float xWeight = 3;

        boss.MoveSpeed(1);
        boss.MoveTo(Vector3.right);

        while (true)
        {
            if (boss.transform.position.x <= Constrants.min.x + xWeight)
            {
                boss.MoveTo(Vector3.right);
            }
            else if(boss.transform.position.x >= Constrants.max.x - xWeight)
            {
                boss.MoveTo(Vector3.left);
            }

            yield return null;
        }
    }

    void CircleFire()
    {
        int count = 30;
        float intravalAngle = 360 / count;

        for(int i=0; i<count; ++i)
        {
            GameObject clone = Instantiate(bossProjectile, boss.transform.position, Quaternion.identity);

            float angle = intravalAngle * i;

            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

            clone.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }
}
