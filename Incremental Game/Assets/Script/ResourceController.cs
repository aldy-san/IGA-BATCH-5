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
    public bool IsUnlocked { get; private set; }
    private int _level = 1;

    private void Start()

    {
        ResourceButton.onClick.AddListener(() =>
        {
            if (IsUnlocked)
            {
                UpgradeLevel();
            }
            else
            {
                UnlockResource();
            }
        });
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
        SetUnlocked(_config.UnlockCost == 0);
    }
    public void UnlockResource()
    {
        double unlockCost = GetUnlockCost();
        if (GameManager.Instance._totalGold < unlockCost)
        {
            return;
        }
        GameManager.Instance.AddGold(-unlockCost);
        SetUnlocked(true);
        GameManager.Instance.ShowNextResource();
        AchievementController.Instance.UnlockAchievement(AchievementType.UnlockResource, _config.Name);
    }

    public void SetUnlocked(bool unlocked)
    {
        IsUnlocked = unlocked;
        ResourceImage.color = IsUnlocked ? Color.white : Color.grey;
        ResourceUnlockCost.gameObject.SetActive(!unlocked);
        ResourceUpgradeCost.gameObject.SetActive(unlocked);
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
