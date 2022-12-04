using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text timeField;
    [SerializeField]
    private Text wordToFindField;
    [SerializeField]
    private GameObject[] hangMan = new GameObject[6];
    [SerializeField]
    private Text winText;
    [SerializeField]
    private Text loseText;
    [SerializeField]
    private Text guessedText;
    [SerializeField]
    private GameObject replayButton;


    private float time;
    private string words;
    private List<string> wordsList = new List<string>();
    private string chosenWord;
    private string hiddenWord;
    private List<string> guesses = new List<string>();
    private int fails;
    private bool gameOver = false;

    void Start()
    {
        words = Resources.Load<TextAsset>("Words").text;
        wordsList = words.Split('\n').ToList<string>();
        chosenWord = wordsList[Random.Range(0, wordsList.Count)].Trim().ToUpper();

        Debug.Log(Application.dataPath);

        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            hiddenWord += char.IsWhiteSpace(letter) ? " " : "_";
            hiddenWord += " ";
        }

        Debug.Log("Chosen: " + chosenWord);

        wordToFindField.text = hiddenWord;
    }

    void Update()
    {
        if (!gameOver)
        {
            time += Time.deltaTime;
            timeField.text = Mathf.FloorToInt(time).ToString();
        }
    }

    void OnGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();

            if (guesses.Contains(pressedLetter) || hiddenWord.Contains(pressedLetter))
            {
                return;
            }


            if (chosenWord.Contains(pressedLetter))
            {
                int i = chosenWord.IndexOf(pressedLetter);
                while (i != -1)
                {
                    hiddenWord = hiddenWord.Substring(0, i * 2) + pressedLetter + hiddenWord.Substring(i * 2 + 1);
                    chosenWord = chosenWord.Substring(0, i) + "_" + chosenWord.Substring(i + 1);
                    i = chosenWord.IndexOf(pressedLetter);
                }

                wordToFindField.text = hiddenWord;
            }
            else
            {
                hangMan[fails++].SetActive(true);
                guesses.Add(pressedLetter);
                guessedText.text = string.Join(",", guesses);

            }

            if (fails >= hangMan.Length)
            {
                loseText.enabled = true;
                replayButton.SetActive(true);
                gameOver = true;
            }
            if (!hiddenWord.Contains("_"))
            {
                winText.enabled = true;
                replayButton.SetActive(true);
                gameOver = true;
            }
        }
    }
}
