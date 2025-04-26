
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;
[DefaultExecutionOrder(-1)]
public class inputmanager : Singleton<inputmanager>
{
    #region
    public delegate void starttouch(Vector2 position, float time);
    public event starttouch Onstarttouch;
    public delegate void endtouch(Vector2 position, float time);
    public event starttouch Onendtouch;

    #endregion

    private Camera maincamera;

    private Touchcontrols touchcontrols;

    private void Awake()
    {
        touchcontrols = new Touchcontrols();
        maincamera = Camera.main;
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
        touchcontrols.touch.PrimaryContact.started += ctx => Starttouchprimary(ctx);
        touchcontrols.touch.PrimaryContact.canceled += ctx => Endtouchprimary(ctx);

    }
    private void Starttouchprimary(InputAction.CallbackContext context)
    {
        if (Onstarttouch != null)
        {
            Onstarttouch(Utils.ScreenToWorld(maincamera, touchcontrols.touch.PrimaryPostion.ReadValue<Vector2>()), (float)context.startTime);
        }

    }

    private void Endtouchprimary(InputAction.CallbackContext context)
    {
        if (Onendtouch != null)
        {
            Onendtouch(Utils.ScreenToWorld(maincamera, touchcontrols.touch.PrimaryPostion.ReadValue<Vector2>()), (float)context.time);
        }
    }
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(maincamera, touchcontrols.touch.PrimaryPostion.ReadValue<Vector2>());
    }
}
