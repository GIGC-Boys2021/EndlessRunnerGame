using System.Collections;

using UnityEngine;

public class swipedetection : MonoBehaviour
{
    [SerializeField]
    private float minimumdistance = 0f;
    [SerializeField]
    private float maximumtime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = 0f;
    private inputmanager Inputmanager;

    private Vector2 startposition;

    private float starttime;

    private Vector2 endposition;
    private float endtime;

    private void Awake()
    {
        Inputmanager = inputmanager.Instance;
    }
    private void OnEnable()
    {
        Inputmanager.Onstarttouch += swipestart;
        Inputmanager.Onendtouch += swipeend;
    }

    private void OnDisable()
    {
        Inputmanager.Onstarttouch -= swipestart;
        Inputmanager.Onendtouch -= swipeend;

    }

    private void swipestart(Vector2 position, float time)
    {
        startposition = position;
        starttime = time;


    }
    private void swipeend(Vector2 position, float time)
    {
        endposition = position;

        endtime = time;
        Detectswipe();
    }


    private void Detectswipe()
    {
        if (Vector3.Distance(startposition, endposition) >= minimumdistance &&
                    (endtime - starttime) <= maximumtime)
        {
            Debug.Log("Swipe Detected!");
            Debug.DrawLine(startposition, endposition, Color.red, 0.02f);
            Vector3 direction = endposition - startposition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            Swipedirection(direction2D);
        }

    }
    private void Swipedirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("Swipe up");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe left");
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe right");
        }
    }
}
