using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        if (crops?.Count > 0)
        {
            SelectedCrop = crops[0];
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
                    SceneManager.UnloadScene("Crops");
                });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
