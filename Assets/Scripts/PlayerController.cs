using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [SerializeField] GameController gameController;
    private MovementRigidbody2D movement2D;
    private PlayerHP playerHP;

    private void Awake()
    {
        movement2D= GetComponent<MovementRigidbody2D>();
        playerHP= GetComponent<PlayerHP>();
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return;

        UpdateMove();
        UpdateJump();
    }

    public void Reset()
    {
        GetComponent<Collider2D>().enabled = true;
        movement2D.ResetPos();
        playerHP.ResetStat();
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        movement2D.MoveTo(x);
    }

    private void UpdateJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            movement2D.JumpTo();
        }else if (Input.GetKey(jumpKey))
        {
            movement2D.IsLongJump = true;
        }else if (Input.GetKeyUp(jumpKey))
        {
            movement2D.IsLongJump = false;
        }
    }

    public void LeftMoveDown()
    {
        movement2D.leftMove = true;
    }

    public void LeftMoveUp()
    {
        movement2D.leftMove = false;
    }

    public void RightMoveDown()
    {
        movement2D.rightMove = true;
    }

    public void RightMoveUp()
    {
        movement2D.rightMove = false;
    }

    public void JumpButton()
    {
        movement2D.JumpTo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            bool isDie = playerHP.TakeDamage();
            if(isDie == true)
            {
                GetComponent<Collider2D>().enabled = false;
                gameController.GameOver();
            }
        }
        else if (collision.CompareTag("HPPotion"))
        {
            collision.gameObject.SetActive(false);
            playerHP.RecoveryHP();
        }
    }
}
