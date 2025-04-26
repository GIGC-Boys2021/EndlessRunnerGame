using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static inputmanager;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isgamestarted;
    public GameObject startbutton;
    public Animator animator;

    private Touchcontrols touchcontrols;
    private void Awake()
    {
        touchcontrols = new Touchcontrols();
    }
    private void OnEnable()
    {

        touchcontrols.Enable();
    }
    private void OnDisable()
    {
        touchcontrols.Disable();
    }

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isgamestarted = false;
        touchcontrols.touch.PrimaryContact.started += ctx => Starttouch(ctx);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
       
        
          

   
    }
    private void Starttouch(InputAction.CallbackContext context)
    {
        if (!isgamestarted)
        {
            animator.SetBool("isgamestarted", true);

            isgamestarted = true;
            startbutton.SetActive(false);

        }
    }
}
