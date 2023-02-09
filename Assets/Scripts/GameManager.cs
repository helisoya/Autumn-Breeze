using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool inGame;

    public GameObject titleScreen;

    public int maxItems;

    [HideInInspector] public int currentItems;

    public Transform pumpkinsParent;

    public GameObject prefabPumpkin;

    public GameObject endScreenRoot;

    void Awake()
    {
        instance = this;
        inGame = false;

        List<int> indexAvailable = new List<int>();
        for (int i = 0; i < pumpkinsParent.childCount; i++)
        {
            indexAvailable.Add(i);
        }

        for (int i = 0; i < maxItems; i++)
        {
            int indexChosen = Random.Range(0, indexAvailable.Count);
            Instantiate(prefabPumpkin, pumpkinsParent.GetChild(indexAvailable[indexChosen]));
            indexAvailable.RemoveAt(indexChosen);
        }
    }



    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Map");
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        inGame = true;
    }


    public void AddItem()
    {
        currentItems++;
        if (currentItems >= maxItems)
        {
            Camera.main.GetComponent<AudioSource>().enabled = false;
            Time.timeScale = 0;
            endScreenRoot.SetActive(true);
        }
    }
}
