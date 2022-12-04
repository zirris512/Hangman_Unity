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
    // Start is called before the first frame update
    void Start()
    {
        chosenWord = wordsLocal[Random.Range(0, wordsLocal.Length)];

        for (int i = 0; i < chosenWord.Length; i++)
        {
            char letter = chosenWord[i];
            hiddenWord += char.IsWhiteSpace(letter) ? " " : "_";
            hiddenWord += " ";
        }

        wordToFindField.text = hiddenWord;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeField.text = Mathf.FloorToInt(time).ToString();
    }
}
