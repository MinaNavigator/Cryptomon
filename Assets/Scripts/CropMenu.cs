using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CropMenu : MonoBehaviour
{
    public List<CropSo> crops;
    public Button button;
    public GridLayoutGroup panel;
    public static CropSo SelectedCrop;

    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtPrice;
    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtSell;
    public TextMeshProUGUI txtLevel;

    //public static CropSo SelectedCrop;

    public static event Action<CropSo> OnSelectCrop;
    public static event Action OnClose;

    // Start is called before the first frame update
    void Start()
    {
        if (crops?.Count > 0)
        {
            if (SelectedCrop == null)
            {
                SelectedCrop = crops.First();
                SetInfo(SelectedCrop);
            }

            foreach (var c in crops)
            {
                var newButton = Instantiate(button);
                var textUI = newButton.GetComponentInChildren<TextMeshProUGUI>();
                textUI.text = c.seedPrice.ToString("C2");
                var image = newButton.GetComponent<Image>();
                image.sprite = c.presentation;
                newButton.transform.SetParent(panel.transform);
                newButton.onClick.AddListener(() =>
                {
                    SelectedCrop = c;
                    SetInfo(SelectedCrop);
                });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectCrop()
    {
        if (OnSelectCrop != null)
        {
            OnSelectCrop(SelectedCrop);
        }
        SceneManager.UnloadSceneAsync("Crops");
    }
    public void SelectClose()
    {
        if (OnClose != null)
        {
            OnClose();
        }
        SceneManager.UnloadSceneAsync("Crops");
    }


    private void SetInfo(CropSo crop)
    {
        txtName.text = crop.name;
        txtLevel.text = $"Min farm level : {crop.minLevel.ToString()}";
        txtPrice.text = "Seed price : " + crop.seedPrice.ToString("C2");
        txtTime.text = $"Grow time : {crop.growTime.ToString()} s";
        txtSell.text = "Sell price : " + crop.plantPrice.ToString("C2");
    }
}
