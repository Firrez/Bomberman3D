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

    [Header("Char Status")] 
    public TextMeshProUGUI red;
    public TextMeshProUGUI blue;
    public TextMeshProUGUI white;
    public TextMeshProUGUI black;
    
    private void Awake()
    {
        var deployer = GetComponent<BombDeployer>();
        deployer.UIDelegate = UpdateBombUI;
        var movement = GetComponent<PlayerMovement>();
        movement.UIDelegate = UpdateMovementUi;
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
                break;
            case "Black":
                black.text = "DEAD!";
                break;
            case "White":
                white.text = "DEAD!";
                break;
            case "Player":
                blue.text = "DEAD!";
                break;
        }
    }
}
