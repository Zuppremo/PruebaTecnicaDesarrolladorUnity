using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdButtonSelect : MonoBehaviour
{
    [SerializeField] private Birds birdType;
    [SerializeField] private Image selectionIndicator;
    private SelectionMenuController selectionMenuController;
    private Button button;
    public Birds BirdType => birdType;

    public Action<BirdButtonSelect> OnClick;
    private void Awake()
    {
        selectionIndicator.enabled = false;
        button = GetComponent<Button>();
        selectionMenuController = FindObjectOfType<SelectionMenuController>();
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        OnClick?.Invoke(this);
    }

    public void UpdateSelection(bool isSelected)
    {
        selectionIndicator.enabled = isSelected;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
