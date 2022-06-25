using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject readyPanel;
    [SerializeField] private GameObject menuPanel;
    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;
    [SerializeField] Button readyButton;

    PlayerConfigurationManager myPlayerConfigurationManager;
    private void Awake()
    {
        myPlayerConfigurationManager = FindObjectOfType<PlayerConfigurationManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetPlayerIndex(int index)
    {
        playerIndex = index;
        titleText.SetText("Player " + (index + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public void SetSprite(Sprite sprite)
    {
        if (!inputEnabled) return;
        myPlayerConfigurationManager.SetPlayerSprite(playerIndex, sprite);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) return;
        myPlayerConfigurationManager.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
