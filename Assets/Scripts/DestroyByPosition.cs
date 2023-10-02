using UnityEngine;

public class DestroyByPosition : MonoBehaviour
{
    private float destoryWeight = 2;

    private void LateUpdate()
    {
        if (transform.position.x < Constrants.min.x - destoryWeight ||
           transform.position.x > Constrants.max.x + destoryWeight ||
           transform.position.y < Constrants.min.y - destoryWeight ||
           transform.position.y > Constrants.max.y + destoryWeight)
        {
            Destroy(gameObject);
        }
    }
}
