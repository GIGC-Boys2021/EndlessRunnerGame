using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float mindistance = 0f;
    [SerializeField]
    private float maxtime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThresholdp = 0.1f;
    private inputmanager Inputmanagerp;

    private Vector2 sposition;

    private float stime;

    private Vector2 eposition;
    private float etime;

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane = 1;
    public float laneDistance = 3;
    public float jumpforce;
    public float Gravity = -20;
    public Animator playeranimator;
    public float maxspeed;
    private bool issliding=false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();


    }

    private void Awake()
    {
        Inputmanagerp = inputmanager.Instance;
    }
    private void OnEnable()
    {
        Inputmanagerp.Onstarttouch += swipestartp;
        Inputmanagerp.Onendtouch += swipeendp;
    }

    private void OnDisable()
    {
        Inputmanagerp.Onstarttouch -= swipestartp;
        Inputmanagerp.Onendtouch -= swipeendp;

    }

    private void swipestartp(Vector2 position, float time)
    {
        sposition = position;
        stime = time;


    }
    private void swipeendp(Vector2 position, float time)
    {
        eposition = position;

        etime = time;
        Detectswipep();
    }


    private void Detectswipep()
    {
        if (Vector3.Distance(sposition, eposition) >= mindistance &&
                    (etime - stime) <= maxtime)
        {
            // Debug.Log("Swipe Dertected!");
            //  Debug.DrawLine(sposition, eposition, Color.red, 0.02f);
            Vector3 direction = eposition - sposition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            Swipedirectionp(direction2D);
        }

    }
    private void Swipedirectionp(Vector2 direction)
    {
      
            if (Vector2.Dot(Vector2.up, direction) > directionThresholdp)
        {
            if (controller.isGrounded)
            {
                direction.y = -1;

               // jumpanimator.SetBool("isgrounded", false);

                {
                  

                    Jump();
                   
                }
            }
            else
            {
                direction.y += Gravity * Time.deltaTime;
              // jumpanimator.SetBool("isgrounded", true);
            }
        }

        //  Debug.Log("Swipe up");

        else if (Vector2.Dot(Vector2.down, direction) > directionThresholdp && !issliding)
        {
           StartCoroutine(Slide());
            // Debug.Log("Swipe down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThresholdp)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
            //Debug.Log("Swipe left");

        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThresholdp)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
            // Debug.Log("Swipe right");

        }
    }

    void Update()
    {
        if (forwardSpeed < maxspeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }
        direction.z = forwardSpeed;
        direction.y += Gravity * Time.deltaTime;

    


        if (controller.isGrounded)
        {
            playeranimator.SetBool("isgrounded", true);
        }
        else
        {
            playeranimator.SetBool("isgrounded", false);
        }
       

            /*  if (controller.isGrounded)
              {
                  direction.y = -1;
                  if (Input.GetKeyDown(KeyCode.UpArrow))
                  {
                      Jump();
                  }
              }
              else
              {
                  direction.y += Gravity * Time.deltaTime;
              }




              if (Input.GetKeyDown(KeyCode.RightArrow))
                  {
                      desiredLane++;
                      if (desiredLane == 3)
                          desiredLane = 2;
                  }
                  if (Input.GetKeyDown(KeyCode.LeftArrow))
                  {
                      desiredLane--;
                      if (desiredLane == -1)
                          desiredLane = 0;
                  }*/

            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (desiredLane == 0)
            {
                targetPosition += Vector3.left * laneDistance;
            }
            else if (desiredLane == 2)
            {
                targetPosition += Vector3.right * laneDistance;
            }

        // transform.position = Vector3.Lerp(transform.position, targetPosition, 300 * Time.deltaTime);
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 movedir = diff.normalized*25*Time.deltaTime;
        if (movedir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(movedir);
        }
        else
        {
            controller.Move(diff);
        }

        }
       
        
    

    private void FixedUpdate()
    {
        if (!PlayerManager.isgamestarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpforce;

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
      if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<audiomanager>().PlaySound("gameover");
        }  
    }
    private IEnumerator Slide()
    {
        issliding = true;
        playeranimator.SetBool("issliding", true);
        controller.center =  new Vector3 (0,-0.5f,0);
        controller.height = 1;


        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;


        playeranimator.SetBool("issliding", false);
        issliding = false;

    }

}

