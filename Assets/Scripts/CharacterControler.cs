using UnityEngine;
using DG.Tweening;

public class CharacterControler : MonoSingleton<CharacterControler>
{
    [SerializeField] Rigidbody rb;
    private float floorWidth = 10;
    public float speed = 7.2f;
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    private float lastXDirection;
    private bool resetHorizontalMove = true;

    void InputControl()
    {
        moveDirection.z = 1;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                moveDirection.x = touch.deltaPosition.x * .05858f;
            }
            else
            {
                moveDirection.x = 0;
            }
        }
        else
        {
            moveDirection.x = 0;
        }

        if (transform.position.x <= -floorWidth)
        {
            moveDirection.x = Mathf.Max(0, moveDirection.x);
        }
        else if (transform.position.x >= floorWidth)
        {
            moveDirection.x = Mathf.Min(0, moveDirection.x);
        }

        if (GameManager.Instance.enumStat != GameManager.GameStat.start || resetHorizontalMove)
        {
            moveDirection.x = 0;
        }

        rb.velocity = moveDirection * speed;

        transform.rotation = Quaternion.Euler(0, moveDirection.x * 36f, 0);
    }

    private void Update()
    {
        if (GameManager.Instance.enumStat == GameManager.GameStat.start)
            InputControl();
    }
}
