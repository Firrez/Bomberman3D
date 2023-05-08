using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI uIBombCount;
    public TextMeshProUGUI uIBombRadius;
    public TextMeshProUGUI uIMoveSpeed;
    public TextMeshProUGUI uIEndText;
    public Camera playerCam;
    public Camera endCam;

    [Header("Char Status")] 
    public TextMeshProUGUI red;
    public TextMeshProUGUI blue;
    public TextMeshProUGUI white;
    public TextMeshProUGUI black;

    private bool _redDead;
    private bool _blueDead;
    private bool _blackDead;
    private bool _whiteDead;
    
    private void Awake()
    {
        var deployer = GetComponent<BombDeployer>();
        deployer.UIDelegate = UpdateBombUI;
        var movement = GetComponent<PlayerMovement>();
        movement.UIDelegate = UpdateMovementUi;
    }

    private void Start()
    {
        playerCam.enabled = true;
        endCam.enabled = false;
    }

    private void CheckGameStatus()
    {
        if (_redDead && _blackDead && _whiteDead && !_blueDead) uIEndText.text = "YOU WIN!";
        if (_blueDead)
        {
            playerCam.enabled = false;
            endCam.enabled = true;
            uIEndText.text = "GAME OVER!";
        }
    }

    private void UpdateBombUI(int bombCount, float bombRadius)
    {
        uIBombCount.text = bombCount.ToString();
        uIBombRadius.text = bombRadius.ToString(CultureInfo.CurrentCulture);
    }

    private void UpdateMovementUi(float speed)
    {
        uIMoveSpeed.text = speed.ToString(CultureInfo.CurrentCulture);
    }

    public void ReportDead(string charName)
    {
        switch (charName)
        {
            case "Red":
                red.text = "DEAD!";
                _redDead = true;
                break;
            case "Black":
                black.text = "DEAD!";
                _blackDead = true;
                break;
            case "White":
                white.text = "DEAD!";
                _whiteDead = true;
                break;
            case "Player":
                blue.text = "DEAD!";
                _blueDead = true;
                break;
        }
        CheckGameStatus();
    }
}
