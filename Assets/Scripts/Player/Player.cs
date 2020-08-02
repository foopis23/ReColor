using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CharacterController2D))]
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

    //properties
    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private Stack<int> colorHistory;

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
        sr = GetComponent<SpriteRenderer>();
        characterController = GetComponent<CharacterController2D>();
        playerInput = new PlayerInput();
        colorHistory = new Stack<int>();

        GameEventSystem.Current.RegisterListener<ColorChangerPlayerPushColorInfo>(colorSwapPushHandler);
    }

    private void OnDestroy()
    {
        if (GameEventSystem.Current != null)
        {
            GameEventSystem.Current.UnregisterListener<ColorChangerPlayerPushColorInfo>(colorSwapPushHandler);
        }
    }

    private void Start()
    {
        pushColor(0);
    }

    private void Update()
    {
        playerInput.Update();

        if (playerInput.RewindState)
            popColor();
    }

    private void FixedUpdate()
    {
        characterController.Move(playerInput.Movement.x * movementSpeed * Time.fixedDeltaTime, false, playerInput.Jump);
    }

    public void colorSwapPushHandler(ColorChangerPlayerPushColorInfo info)
    {
        if (info.Target == this)
        {
            pushColor(info.Color);
        }
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
        if (colorHistory.Count > 0)
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
