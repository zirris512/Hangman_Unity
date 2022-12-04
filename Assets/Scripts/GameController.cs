using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text timeField;
    [SerializeField]
    private Text wordToFindField;
    private float time;
    private string[] wordsLocal = { "MATT", "JOANNE", "ROBERT", "MARY JANE", "DENIS" };
    private string chosenWord;
    private string hiddenWord;

    void Start()
    {
        chosenWord = wordsLocal[Random.Range(0, wordsLocal.Length)];

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
        time += Time.deltaTime;
        timeField.text = Mathf.FloorToInt(time).ToString();
    }

    void OnGUI()
    {
        Event e = Event.current;

        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1)
        {
            string pressedLetter = e.keyCode.ToString();

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
        }
    }
}
