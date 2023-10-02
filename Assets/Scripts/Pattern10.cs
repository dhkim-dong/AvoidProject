using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern10 : MonoBehaviour
{
    [SerializeField] private GameObject[] waringImages;
    [SerializeField] GameObject unit;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float spawnCycle;
    [SerializeField] int maxCount;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        unit.GetComponent<MovingEntity>().Reset();
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(1f);

        // 경고 이미지
        waringImages[0].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        waringImages[0].SetActive(false);

        // 위에서 아래로 이동
        yield return StartCoroutine(nameof(MoveDown));


        // 발사체 생성
        yield return StartCoroutine(nameof(SpawnProjectile));

        // 경고이미지
        waringImages[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        waringImages[1].SetActive(false);


        yield return StartCoroutine(nameof(MoveHorizontal));


        // 패턴 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        float destinationY = -3f;

        unit.gameObject.SetActive(true);

        while (true)
        {
            if(unit.transform.position.y <= destinationY)
            {
                unit.GetComponent<MovementTransform2D>().MoveTo(Vector3.zero);

                yield break;
            }
            yield return null;
        }
    }

    IEnumerator SpawnProjectile()
    {
        float minSpeed = 2;
        float maxSpeed = 10;

        int count = 0;
        while( count < maxCount)
        {
            GameObject clone = Instantiate(projectilePrefab, unit.transform.position, Quaternion.identity);
            var moveMent2D = clone.GetComponent<MovementRigidbody2D>();

            moveMent2D.MoveSpeed = Random.Range(minSpeed, maxSpeed);
            moveMent2D.MoveTo(1 - 2 * Random.Range(0, 2));
            moveMent2D.IsLongJump = Random.Range(0, 2) == 0 ? false : true;
            moveMent2D.JumpTo();

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }
    }

    IEnumerator MoveHorizontal()
    {
        Vector3 direction = Random.Range(0, 2) == 0 ? Vector3.right : Vector3.left;
        unit.GetComponent<MovementTransform2D>().MoveTo(direction);

        while (true)
        {
            if(unit.transform.position.x < Constrants.min.x||
                unit.transform.position.x > Constrants.max.x)
            {
                unit.gameObject.SetActive(false);

                yield break;
            }
            yield return null;
        }

    }
}
