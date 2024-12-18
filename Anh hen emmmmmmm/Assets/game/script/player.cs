using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour 
{
    private Rigidbody2D rb;
    public float jforce = 1f;
    public float horizontalJumpForce = 5f;
    public float maxChargeTime = 5f;
    public float maxJumpForce = 15f;
    private bool isground;
    private bool wasInAir = false;
    public float checkR;
    public LayerMask ground;
    public Transform vt;
    private float chargeStartTime;
    private bool isCharging = false;
    private Animator animator;
    private AnimatorStateInfo animStateInfo;

    // New variables for rotation check
    private float rotationCheckTimer = 0f;
    private bool isCheckingRotation = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.rotation.eulerAngles.z > 200) 
        {
            if (!isCheckingRotation)
            {
                isCheckingRotation = true;
                rotationCheckTimer = 0f;
            }
            
            rotationCheckTimer += Time.deltaTime;
            
            if (rotationCheckTimer >= 5f)
            {
                StartCoroutine(Death());
                isCheckingRotation = false;
                rotationCheckTimer = 0f;
            }
        }
        else
        {
            isCheckingRotation = false;
            rotationCheckTimer = 0f;
        }

       
        bool previousGroundState = isground;
        isground = Physics2D.OverlapCircle(vt.position, checkR, ground);
        animator.SetBool("stratjump", isCharging);

        if (!isground && rb.velocity.y < 0)
        {
            wasInAir = true;
        }

        if (isground && wasInAir)
        {
            animator.SetTrigger("down");
            wasInAir = false;
        }

        animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (isground && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            chargeStartTime = Time.time;
            isCharging = true;
        }

        if (isCharging && (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)))
        {
            float chargeTime = Mathf.Min(Time.time - chargeStartTime, maxChargeTime);
            float verticalForce = Mathf.Lerp(jforce, maxJumpForce, chargeTime / maxChargeTime);
            float direction = transform.localScale.x;
            float horizontalForce = horizontalJumpForce * direction;
            rb.velocity = new Vector2(horizontalForce, verticalForce);
            isCharging = false;
            animator.speed = 0;
            StartCoroutine(ResetAnimatorSpeed());
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator ResetAnimatorSpeed()
    {
        yield return new WaitForSeconds(0.1f);
        animator.speed = 1;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.CompareTag("trap"))
        {
            StartCoroutine(Death());
        }
        if(collision2D.gameObject.CompareTag("end"))
        {
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(1f);
        MapLevelManager.Instance.UnlockNextLevel();
        SceneManager.LoadScene("LevelSelect");
    }

    IEnumerator Death()
    {
        animator.SetTrigger("death");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}