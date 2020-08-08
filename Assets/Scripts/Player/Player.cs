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

    [SerializeField]
    private AudioClip jumpClip;

    [SerializeField]
    private AudioClip landClip;

    [SerializeField]
    private AudioClip pushColorClip;

    [SerializeField]
    private AudioClip popColorClip;


    private AudioSource audioSource;

    private Vector2 movement;
    private bool jump;
    private bool rewind;
    private bool interact;

    private ColorLaserController interactable;

    private int colorHistorySize;

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
        audioSource = GetComponent<AudioSource>();


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
        colorHistorySize = GameConstants.Current.getLevelData().historySize;
        pushColor(0);
    }

    private void Update()
    {
        if (!GameConstants.Current.Paused)
        {
            movement.x = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }


            rewind = Input.GetButtonDown("Rewind");
            interact = Input.GetButtonDown("Interact");

            if (rewind)
                popColor();

            //TODO: Replace this with a real way to reset the level
            if (Input.GetButtonDown("Reset"))
            {
                GameEventSystem.Current.FireEvent(new EndLevelInfo(this, SceneManager.GetActiveScene().buildIndex));
            }

            characterController.InvertGravity = (ColorID == 4);
        }

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

    }

    private void FixedUpdate() {

        if (!GameConstants.Current.Paused)
        {
            characterController.Move(movement.x * movementSpeed * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
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
            if (colorHistory.Count + 1 <= colorHistorySize)
            {
                colorHistory.Push(colorID);
                int i = colorHistory.Peek();
                if (i < colorList.colors.Length)
                {
                    sr.color = colorList.colors[i];
                }

                GameEventSystem.Current.FireEvent(new PlayerPushColorInfo(this, colorID));

                audioSource.volume = 0.7f;
                audioSource.clip = pushColorClip;
                StartCoroutine(playAudio());
            }
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

            audioSource.volume = 0.7f;
            audioSource.clip = popColorClip;
            StartCoroutine(playAudio());

            return color;
        }


        return -1;
    }

    public void handleLanding()
    {
        Debug.Log("land");
        audioSource.volume = 0.5f;
        audioSource.clip = landClip;
        StartCoroutine(playAudio());
    }

    public void handleJump()
    {
        Debug.Log("jump");
        audioSource.volume = 0.5f;
        audioSource.clip = jumpClip;
        StartCoroutine(playAudio());
    }

    IEnumerator playAudio()
    {
        if (!GameConstants.Current.Paused)
        {
            yield return new WaitForEndOfFrame();
            audioSource.Play();
        }
    }
}
