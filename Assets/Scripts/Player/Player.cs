using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //dependacies
    [SerializeField]
    private ColorScriptableObject colorList;

    //modules
    private PlayerInput playerInput;
    private CharacterController2D characterController;

    //components
    private SpriteRenderer sr;
    private CanvasGroup interactionDialogue;

    //properties
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Stack<int> colorHistory;

    private Vector2 movement;
    private bool jump;
    private bool rewind;
    private bool interact;

    private ColorLaserController interactable;

    //public getters
    public int ColorID
    {
        get
        {
            if (this.colorHistory.Count > 1)
                return this.colorHistory.Peek();

            return 0;
        }
    }

    public Color Color
    {
        get
        {
            return this.colorList.colors[this.colorHistory.Peek()];
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        sr = GetComponent<SpriteRenderer>();
        colorHistory = new Stack<int>();
        interactionDialogue = GetComponentInChildren<CanvasGroup>();


        GameEventSystem.Current.RegisterListener<ColorChangerPlayerPushColorInfo>(colorSwapPushHandler);
        GameEventSystem.Current.RegisterListener<PlayerEnterColorLaser>(EnterLaserHandler);
        GameEventSystem.Current.RegisterListener<PlayerExitColorLaser>(ExitLaserHandler);
    }

    private void OnDestroy()
    {
        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.UnregisterListener<ColorChangerPlayerPushColorInfo>(colorSwapPushHandler);
            GameEventSystem.Current.UnregisterListener<PlayerEnterColorLaser>(EnterLaserHandler);
            GameEventSystem.Current.UnregisterListener<PlayerExitColorLaser>(ExitLaserHandler);
        }
    }

    private void Start()
    {
        pushColor(0);
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
            jump = true;

        rewind = Input.GetButtonDown("Rewind");
        interact = Input.GetButtonDown("Interact");

        if (rewind)
            popColor();

        if (interactable != null)
        {
            interactionDialogue.alpha = 1;

            if (interact)
            {
                GameEventSystem.Current.FireEvent(new PlayerInteractWithColorLaser(this, interactable));
                interactable = null;
            }

        }
        else
        {
            interactionDialogue.alpha = 0;
        }

        //TODO: Replace this with a real way to reset the level
        if (Input.GetButtonDown("Reset"))
        {
            GameEventSystem.Current.FireEvent(new EndLevelInfo(this, SceneManager.GetActiveScene().buildIndex));
        }
    }

    private void FixedUpdate()
    {
        characterController.Move(movement.x * movementSpeed * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void colorSwapPushHandler(ColorChangerPlayerPushColorInfo info)
    {
        if (info.Target == this)
        {
            pushColor(info.Color);
        }
    }

    public void EnterLaserHandler(PlayerEnterColorLaser info)
    {
        if (info.Src.Color != colorHistory.Peek())
        {
            interactable = info.Src;
        }
    }

    public void ExitLaserHandler(PlayerExitColorLaser info)
    {
        interactable = null;
    }

    public void pushColor(int colorID)
    {
        if (colorHistory.Count < 1 || colorHistory.Peek() != colorID)
        {
            colorHistory.Push(colorID);
            int i = colorHistory.Peek();
            if (i < colorList.colors.Length)
            {
                sr.color = colorList.colors[i];
            }

            GameEventSystem.Current.FireEvent(new PlayerPushColorInfo(this, colorID));
        }
    }

    public int popColor()
    {
        if (colorHistory.Count > 1)
        {
            int color = colorHistory.Pop();
            int currentColor = colorHistory.Peek();
            if (currentColor < colorList.colors.Length)
            {
                sr.color = colorList.colors[currentColor];
            }


            GameEventSystem.Current.FireEvent(new PlayerPopColorInfo(this, currentColor));

            return color;
        }


        return -1;
    }
}
