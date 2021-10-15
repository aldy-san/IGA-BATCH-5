using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResourceController : MonoBehaviour
{
    public Button ResourceButton;
    public Image ResourceImage;
    public TextMeshProUGUI ResourceDescription;
    public TextMeshProUGUI ResourceUpgradeCost;
    public TextMeshProUGUI ResourceUnlockCost;
    private GameManager.ResourceConfig _config;
    private int _level = 1;

    private void Start()

    {
        ResourceButton.onClick.AddListener(UpgradeLevel);
    }
    public void UpgradeLevel()
    {
        Debug.Log(GameManager.Instance._totalGold);
        Debug.Log(GetUpgradeCost());
        double upgradeCost = GetUpgradeCost();
        if (GameManager.Instance._totalGold < upgradeCost)
        {
            return;
        }
        GameManager.Instance.AddGold(-upgradeCost);
        _level++;
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";
        ResourceDescription.text = $"{ _config.Name } Lv. { _level }\n+{ GetOutput().ToString("0") }";
    }
    public void SetConfig(GameManager.ResourceConfig config)
    {
        _config = config;

        ResourceDescription.text = $"{ _config.Name } Lv.{ _level }\n+{ GetOutput().ToString("0") }";
        ResourceUnlockCost.text = $"Unlock Cost\n{ _config.UnlockCost }";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ GetUpgradeCost() }";
        //SetUnlocked(_config.UnlockCost == 0);
    }
    public double GetOutput()
    {
        //Debug.Log(_config.Output * _level);
        return _config.Output * _level;
    }

    public double GetUpgradeCost()

    {
        return _config.UpgradeCost * _level;
    }

    public double GetUnlockCost()
    {
        return _config.UnlockCost;
    }
}
