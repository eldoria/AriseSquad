using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leadboard : MonoBehaviour
{
    public string privateCode;
    public string publicCode;
    public int MaxRank = 10;
    public string[] Ranking;
    public static int lenght;
    const string webURL = "http://dreamlo.com/lb/";
    static leadboard instance;
    
    
    // Start is called before the first frame update
    void Start()
    {
        DownloadHighscores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    public static void AddNewHighScores(string username, int score)
    {
        //instance.StartCoroutine(instance.UpLoadNewHighscore(username, score));
    }

    IEnumerable UpLoadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadHighscores();
        }
        else
        {
            print("Error Uploading : " + www.error);
        }
    }
    

    void DownloadHighscores()
    {
        instance.StartCoroutine("DownloadHighScoresFromDataBase");
    }

    IEnumerable DownloadHighscoresFromDataBase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.error);
        }
        else
        {
            print("Error Download " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);

        int ii;
        if (entries.Length < MaxRank)
        {
            ii = entries.Length;
        }
        else
        {
            ii = MaxRank;
        }
        
        Ranking = new string[ii];

        for (int i = 0; i < ii ; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            Ranking[i] = username + " " + score;
        }

        lenght = Ranking.Length;
    }

    public static string GetPosition(int number)
    {
        return (instance.Ranking[number]);
    }
    
}
