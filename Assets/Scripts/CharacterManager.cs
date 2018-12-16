using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IDamageable
{

    enum PlayerState { Move, Rotate }
    public float speedMovement = 5;
    public float rotationAngle = 80;
    private int health = 100;
    private bool canMove 
    {
        get { return health > 0; }
    }

    public int playerHealth
    {
        get { return health; }
    }


    private Animator animation 
    {
        get { return gameObject.GetComponent<Animator>(); }
    }

    private List<KeyCodeConfig> keyCodes = new List<KeyCodeConfig>(new KeyCodeConfig[] { 
        new KeyCodeConfig(KeyCode.UpArrow, Vector3.right, PlayerState.Move),
        new KeyCodeConfig(KeyCode.DownArrow, Vector3.left, PlayerState.Move),
        new KeyCodeConfig(KeyCode.LeftArrow, Vector3.down, PlayerState.Rotate),
        new KeyCodeConfig(KeyCode.RightArrow, Vector3.up, PlayerState.Rotate)
    });

    void Update()
    {
        GetInputKeys();
    }

    void GetInputKeys()
    {
        foreach (KeyCodeConfig keyCodeConfig in keyCodes)
        {
            if (Input.GetKey(keyCodeConfig.keyCode) && canMove)
            {
                animation.SetBool("Walk", true);

                if (keyCodeConfig.playerState == PlayerState.Move)
                {
                    MoveCharacter(keyCodeConfig.position);
                }
                else
                {
                    RotateCharacter(keyCodeConfig.position);
                }
            }
        }
    }

    void MoveCharacter(Vector3 vector) 
    {
        transform.Translate(vector * speedMovement * Time.deltaTime);
    }

    void RotateCharacter(Vector3 vector) 
    {
        transform.Rotate(vector * rotationAngle * Time.deltaTime);
    }

    public void OnDamage(int value) 
    {
        health -= value;
        Debug.Log(health);
    }

    private class KeyCodeConfig
    {
        public KeyCode keyCode;
        public Vector3 position;
        public PlayerState playerState = PlayerState.Move;

        public KeyCodeConfig(KeyCode keyCode, Vector3 position, PlayerState playerState)
        {
            this.keyCode = keyCode;
            this.position = position;
            this.playerState = playerState;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IWinnable gameControllerObject = collision.collider.gameObject.GetComponent<IWinnable>();
        if (gameControllerObject != null)
        {
            gameControllerObject.OnWinGame();
        }
    }
}


