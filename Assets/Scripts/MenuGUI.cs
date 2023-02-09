using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuGUI : MonoBehaviour
{
    public TextMeshProUGUI textItemCount;

    public GameObject menuRoot;

    private bool menuOpened = false;

    public AudioSource sound;

    void Update()
    {
        if (!GameManager.instance.inGame) return;

        if (Input.GetButtonDown("Cancel"))
        {
            menuOpened = !menuOpened;
            menuRoot.SetActive(menuOpened);
            Time.timeScale = menuOpened ? 0 : 1;

            if (menuOpened)
            {
                if (sound.enabled)
                {
                    sound.Stop();
                }
                textItemCount.text = "Pumpkins : " + GameManager.instance.currentItems + "/" + GameManager.instance.maxItems;
            }
            else if (sound.enabled)
            {
                sound.Play();
            }
        }
    }
}
