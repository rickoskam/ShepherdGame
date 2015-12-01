using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
    {
    public Text highscoretext;
    private static HighScoreManager instantie;
    private const int LeaderboardLength = 10;
    private string score;

    public void pakdata()
    {
        List<Scores> a = new List<Scores>();
        a = GetHighScore();
       

for(int k=0; k<10; k++)
        {
            
        
        }

    }

    public string toString(string name, int score)
    {
        string res = "Name" + name + "Score" + score;
        return res;
    }

        public static HighScoreManager _instance
        {
            get
            {
                if (instantie == null)
                {
                    instantie = new GameObject("HighScoreManager").AddComponent<HighScoreManager>();
                }
                return instantie;
            }
        }

        void Awake()
        {
            if (instantie == null)
            {
                instantie = this;
            }
            else if (instantie != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public void SaveHighScore(string name, int score)
        {
            List<Scores> HighScores = new List<Scores>();

            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "score"))
            {
                Scores temp = new Scores();
                temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
                temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
                HighScores.Add(temp);
                i++;
            }
            if (HighScores.Count == 0)
            {
                Scores _temp = new Scores();
                _temp.name = name;
                _temp.score = score;
                HighScores.Add(_temp);
            }
            else
            {
                for (i = 1; i <= HighScores.Count && i <= LeaderboardLength; i++)
                {
                    if (score > HighScores[i - 1].score)
                    {
                        Scores _temp = new Scores();
                        _temp.name = name;
                        _temp.score = score;
                        HighScores.Insert(i - 1, _temp);
                        break;
                    }
                    if (i == HighScores.Count && i < LeaderboardLength)
                    {
                        Scores _temp = new Scores();
                        _temp.name = name;
                        _temp.score = score;
                        HighScores.Add(_temp);
                        break;
                    }
                }
            }

            i = 1;
            while (i <= LeaderboardLength && i <= HighScores.Count)
            {
                PlayerPrefs.SetString("HighScore" + i + "name", HighScores[i - 1].name);
                PlayerPrefs.SetInt("HighScore" + i + "score", HighScores[i - 1].score);
                i++;
            }

        }


        public List<Scores> GetHighScore()
        {
            List<Scores> HighScores = new List<Scores>();

            int i = 1;
            while (i <= LeaderboardLength && PlayerPrefs.HasKey("HighScore" + i + "score"))
            {
                Scores temp = new Scores();
                temp.score = PlayerPrefs.GetInt("HighScore" + i + "score");
                temp.name = PlayerPrefs.GetString("HighScore" + i + "name");
                HighScores.Add(temp);
                i++;
            }

            return HighScores;
        }

        public void ClearLeaderBoard()
        {
            //for(int i=0;i<HighScores.
            List<Scores> HighScores = GetHighScore();

            for (int i = 1; i <= HighScores.Count; i++)
            {
                PlayerPrefs.DeleteKey("HighScore" + i + "name");
                PlayerPrefs.DeleteKey("HighScore" + i + "score");
            }
        }

        void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }


    public class Scores
    {
        public int score;
        public string name;

    }


