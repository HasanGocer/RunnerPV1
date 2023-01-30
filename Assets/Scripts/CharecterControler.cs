using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using DG.Tweening;
public class CharacterControler : MonoBehaviour
{
    Rigidbody rb;
    bool isLevelEnd;
    bool isGameStarted;
    Vector3 moveDirection;
    float lastXDirection;
    float floorWidth = 2.5f;
    public float speed;
    float tempSpeed = 7.2f;
    public bool resetHorizontalMove = true;
    public AnimationClip run, dance, normalWalk, idle, sadFinish;
    public AnimancerComponent anim;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        anim = GetComponent<AnimancerComponent>();
        Idle();
    }

    public void Run()
    {
        AnimationManager.instance.PlayAnim(anim, run, 0.3f, 1.7f);
    }
    public void Idle()
    {
        AnimationManager.instance.PlayAnim(anim, idle, 0.3f, 1.2f);
    }
    public void MovePlayer()
    {
        //Debug.Log("moving");
        speed = tempSpeed;
        Run();
    }
    public void Dance()
    {
        if (GetComponent<ClothChanger>().previousLayer <= 1)
        {
            AnimationManager.instance.PlayAnim(anim, sadFinish, 0.3f, 1.2f);
        }
        else
        {
            AnimationManager.instance.PlayAnim(anim, dance, 0.3f, 1.2f);
        }

    }
    public void NormalWalk()
    {
        AnimationManager.instance.PlayAnim(anim, normalWalk, 0.3f, 1.7f);
    }

    public void SetMove()
    {
        speed = tempSpeed;
    }
    void InputControl()
    {
        moveDirection.z = 1;
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
                moveDirection.x = 0;
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
                moveDirection.x = 0;
                transform.DOMoveX(floorWidth - 0.1f, .01f);
            }
            if (moveDirection.x > 0)
            {
                moveDirection.x = 0;
            }
        }
        else if (isLevelEnd)
        {
            moveDirection.x = 0;
        }
        if (resetHorizontalMove)
        {
            moveDirection.x = 0;
        }
        rb.velocity = moveDirection * speed;

        transform.DORotate(new Vector3(0, moveDirection.x * 36f, 0), .38f);
    }
    void Update()
    {
        InputControl();
    }
}