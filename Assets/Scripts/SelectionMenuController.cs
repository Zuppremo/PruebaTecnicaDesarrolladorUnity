using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionMenuController : MonoBehaviour
{
    private List<BirdButtonSelect> selectedBirds = new List<BirdButtonSelect>();
    public Action SelectionUpdated;
    private SceneChanger sceneChanger;
    private BirdButtonSelect[] allButtons;

    private void Awake()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
        allButtons = FindObjectsOfType<BirdButtonSelect>();
        foreach (BirdButtonSelect button in allButtons)
            button.OnClick += SelectBird;
    }


    private void SelectBird(BirdButtonSelect birdButton)
    {
        if (selectedBirds.Contains(birdButton))
            selectedBirds.Remove(birdButton);
        else
            selectedBirds.Add(birdButton);

        birdButton.UpdateSelection(selectedBirds.Contains(birdButton));

        if (selectedBirds.Count > 3)
        {
            selectedBirds[selectedBirds.Count - 2].UpdateSelection(false);
            selectedBirds.RemoveAt(selectedBirds.Count - 2);
        }

        PlayerPrefs.SetString("SelectedBirds", string.Join(",", selectedBirds.ConvertAll(b => (int)b.BirdType)));
        PlayerPrefs.Save();
    }

    public void TryContinue() 
    {
        if(selectedBirds.Count == 3)
            sceneChanger.SceneChange("game");
    }

    public void ResetSelection()
    {
        foreach (BirdButtonSelect button in selectedBirds)
            button.UpdateSelection(false);
        selectedBirds.Clear();
    }

    private void OnDestroy()
    {
        foreach (BirdButtonSelect button in allButtons)
            button.OnClick -= SelectBird;
    }
}
