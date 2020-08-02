using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        colorHistory.Push(0);
    }

    private void Update()
    {
        int i = colorHistory.Peek();
        if (i < colorList.colors.Length)
        {
            sr.color = colorList.colors[i];
        }

        playerInput.Update();
        characterController.Move(playerInput.Movement.x * movementSpeed * Time.deltaTime, false, playerInput.Jump);
    }

    public void pushColor(int colorID)
    {
        if (colorHistory.Peek() != colorID)
            colorHistory.Push(colorID);
    }
}
