using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patteron09 : MonoBehaviour
{
    [SerializeField] GameObject waringImage;
    [SerializeField] GameObject unit;
    [SerializeField] Vector3[] spawnPostion;

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

        // 경고 이미지
        waringImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        waringImage.SetActive(false);

        int count = 0;
        while(count < maxCount)
        {
            // 0 : 왼쪽에서 오른쪽, 1: 오른쪽에서 왼쪽

            int spawnType = UnityEngine.Random.Range(0, 2);

            GameObject clone = Instantiate(unit, spawnPostion[spawnType], Quaternion.identity);

            clone.GetComponent<SpriteRenderer>().flipX = spawnType == 0 ? false : true;

            clone.GetComponent<MovementTransform2D>().MoveTo(spawnType == 0 ? Vector3.right : Vector3.left);

            count++;

            yield return new WaitForSeconds(spawnCycle);
        }

        // 패턴 비활성화
        gameObject.SetActive(false);
    }
}
