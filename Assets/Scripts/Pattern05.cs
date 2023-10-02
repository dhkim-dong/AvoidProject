using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern05 : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject lasor;
    [SerializeField] Collider2D[] lasorCollider2D;
    [SerializeField] private float rotateTime;
    [SerializeField] private int anglePerSeconds;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Process));
    }

    IEnumerator Process()
    {
        yield return new WaitForSeconds(1);

        ground.SetActive(true);
        boss.SetActive(true);
        lasor.SetActive(true);

        for (int i = 0; i < lasorCollider2D.Length; i++) 
        {
            lasorCollider2D[i].enabled = false;
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < lasorCollider2D.Length; i++)
        {
            lasorCollider2D[i].enabled = true;
        }

        float time = 0;
        while( time < rotateTime)
        {
            lasor.transform.Rotate(Vector3.forward * anglePerSeconds * Time.deltaTime);

            time += Time.deltaTime;

            yield return null;
        }

        ground.SetActive(false);
        boss.SetActive(false);
        lasor.SetActive(false);

        gameObject.SetActive(false);
    }
}
