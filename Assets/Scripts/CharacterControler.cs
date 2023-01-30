using UnityEngine;
using DG.Tweening;

public class CharacterControler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    private float floorWidth = 2.5f;
    public float speed = 7.2f;
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    private float lastXDirection;
    private bool resetHorizontalMove = true;

    private void InputControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastXDirection = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            moveDirection.x = (Input.mousePosition.x - lastXDirection) * .05858f;
            lastXDirection = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveDirection.x = 0;
        }

        if (transform.position.x <= -floorWidth + 0.25f)
        {
            if (transform.position.x < -floorWidth)
            {
                transform.DOMoveX(-floorWidth + 0.1f, .01f);
            }
            if (moveDirection.x < 0)
            {
                moveDirection.x = 0;
            }
        }
        else if (transform.position.x >= floorWidth - 0.25f)
        {
            if (transform.position.x > floorWidth)
            {
                transform.DOMoveX(floorWidth - 0.1f, .01f);
            }
            if (moveDirection.x > 0)
            {
                moveDirection.x = 0;
            }
        }
        else if (resetHorizontalMove)
        {
            moveDirection.x = 0;
        }

        rb.velocity = moveDirection * speed;
        transform.DORotate(new Vector3(0, moveDirection.x * 36f, 0), .38f);
    }

    private void Update()
    {
        InputControl();
    }
}
