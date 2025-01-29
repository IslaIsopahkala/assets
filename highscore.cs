using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class highscore : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    List<HighscoreEntry> highscoreEntryList;
    List<Transform> highscoreEntryTransformList;

    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(1000, "Hei");
        AddHighscoreEntry(2500, "Juu");

        string jsonString = PlayerPrefs.GetString("highscores");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //sorts the entries 
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int k = i + 1; k < highscores.highscoreEntryList.Count; k++)
            {
                if(highscoreEntryList[k].score > highscores.highscoreEntryList[i].score)
                {
                    //swaps the entries if other one is higher
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[k];
                    highscores.highscoreEntryList[k] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            HighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    //creates the different entries
        void HighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
        {
            float templateHeight = 40f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default: rankString = rank + "th"; break;
                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

            entryTransform.Find("pos").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = highscoreEntry.score;

            entryTransform.Find("score").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = highscoreEntry.name;
            entryTransform.Find("name").GetComponent<TextMeshProUGUI>().text = name;

            transformList.Add(entryTransform);
        }

    void AddHighscoreEntry(int score, string name)
    {
        //create highscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name};

        //load saved highscores
        string jsonString = PlayerPrefs.GetString("highscores");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //add new entry to highscores
        highscores.highscoreEntryList.Add(highscoreEntry); //this one for some reason makes an error??? no idea why

        //save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscores", json);
        PlayerPrefs.Save();
    }
    
    class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    class HighscoreEntry
    {
        public int score;
        public string name;
    }
}