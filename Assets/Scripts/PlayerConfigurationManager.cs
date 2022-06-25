using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField] private int maxPlayers = 2;
    private void Awake()
    {
        //Singleton
        int numInstances = FindObjectsOfType<PlayerConfigurationManager>().Length;
        if (numInstances > 1)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerSprite(int index, Sprite sprite)
    {
        playerConfigs[index].playerSprite = sprite;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if(playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("FightingScene");
        }
    }

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("Player Joined: " + playerInput.playerIndex);
        if(!playerConfigs.Any(p => p.playerIndex == playerInput.playerIndex))
        {
            playerInput.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(playerInput));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput playerInput)
    {
        input = playerInput;
        playerIndex = playerInput.playerIndex;
    }
    public PlayerInput input;
    public int playerIndex;
    public bool isReady;
    public Sprite playerSprite;
}
