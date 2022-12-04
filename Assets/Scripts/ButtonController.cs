using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
