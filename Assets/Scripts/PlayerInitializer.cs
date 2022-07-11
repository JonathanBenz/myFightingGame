using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerMovement playerMovement;
    private Block playerBlock;
    [SerializeField] private SpriteRenderer playerSprite;

    private FightingGame controls;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerBlock = GetComponent<Block>();
        controls = new FightingGame();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerSprite.sprite = pc.playerSprite;
        playerConfig.input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.action.name == controls.Player.Move.name) 
        {
            playerMovement.Move(obj);
        }
        if (obj.action.name == controls.Player.Jump.name)
        {
            playerMovement.Jump(obj);
        }
        if (obj.action.name == controls.Player.Attack.name)
        {
            playerMovement.Attack(obj);
        }
        if (obj.action.name == controls.Player.Blocking.name)
        {
            playerBlock.Blocking(obj);
        }
    }
}
