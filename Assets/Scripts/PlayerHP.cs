using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameObject[] imageHp;
    private int currentHp;

    [SerializeField] private float  invincibiliyDuration;
    private bool                    isInvincibility = false;

    private SoundController      soundController;
    private SpriteRenderer       spriteRenderer;

    private Color               originColor;

    private void Awake()
    {
        currentHp = imageHp.Length;
        soundController = GetComponentInChildren<SoundController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originColor = spriteRenderer.color;
    }

    public void ResetStat()
    {
        for (int i = 0; i < imageHp.Length; i++)
        {
            imageHp[i].SetActive(true);
        }
        currentHp = imageHp.Length;
    }

    public bool TakeDamage()
    {
        if (isInvincibility == true) return false;

        if(currentHp > 1)
        {
            soundController.Play(0);
            StartCoroutine(nameof(OnInvincibility));
            currentHp--;
            imageHp[currentHp].SetActive(false);
        }
        else
        {
            return true;
        }

        return false;
    }

    private IEnumerator OnInvincibility()
    {
        isInvincibility = true;

        float current = 0;
        float percent = 0;
        float colorSpeed = 10;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / invincibiliyDuration;

            spriteRenderer.color = Color.Lerp(originColor, Color.red, Mathf.PingPong(Time.time * colorSpeed, 1));

            yield return null;
        }

        spriteRenderer.color = originColor;
        isInvincibility      = false;
    }

    public void RecoveryHP()
    {
        if(currentHp < imageHp.Length)
        {
            soundController.Play(1);
            imageHp[currentHp].SetActive(true);
            currentHp++;
        }
    }
}
