using UnityEngine;
using System.Collections;

public class PlayerMovementSwipe : MonoBehaviour
{
    [Header("Move Forward")]
    public float forwardSpeed = 6f;
    public float speedIncrease = 0.1f;
    public float maxSpeed = 12f;

    [Header("Lane System")]
    public float laneDistance = 2f;
    private int targetLane = 0;

    [Header("Jump / Slide")]
    public float jumpForce = 6f;
    public float slideTime = 0.5f;

    [Header("Swipe Detection")]
    public float minSwipeDistance = 50f;

    Rigidbody rb;
    Vector2 startTouch;
    bool swiping = false;
    bool isSliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DetectSwipe();
        SpeedProgression();
    }

    void FixedUpdate()
    {
        MoveForward();
        MoveLane();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.fixedDeltaTime);
    }

    void MoveLane()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = targetLane * laneDistance;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * 10f);
    }

    void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began) startTouch = t.position;
            if (t.phase == TouchPhase.Ended) HandleSwipe(t.position - startTouch);
        }

        // --- Editor ---
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
            swiping = true;
        }
        if (Input.GetMouseButtonUp(0) && swiping)
        {
            Vector2 delta = (Vector2)Input.mousePosition - startTouch;
            HandleSwipe(delta);
            swiping = false;
        }
    }

    void HandleSwipe(Vector2 delta)
    {
        if (delta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0) MoveRight();
            else MoveLeft();
        }
        else
        {
            if (delta.y > 0) Jump();
            else Slide();
        }
    }

    void MoveLeft() { targetLane = Mathf.Max(-1, targetLane - 1); }
    void MoveRight() { targetLane = Mathf.Min(1, targetLane + 1); }

    void Jump()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void Slide()
    {
        if (!isSliding) StartCoroutine(SlideRoutine());
    }

    IEnumerator SlideRoutine()
    {
        isSliding = true;
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x, scale.y / 2, scale.z);

        yield return new WaitForSeconds(slideTime);

        transform.localScale = scale;
        isSliding = false;
    }

    void SpeedProgression()
    {
        if (forwardSpeed < maxSpeed)
            forwardSpeed += speedIncrease * Time.deltaTime;
    }
}
