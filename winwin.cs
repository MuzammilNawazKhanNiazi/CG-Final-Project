using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class winwin : MonoBehaviour
{

    public Text text;
   

    void Start()
    {
      var text1= text.GetComponent<Text>();
        text1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad == 128)
        {
           var text1 = text.GetComponent<Text>();
            text1.enabled = false;
            Invoke("startGame", 1f);
            text1.enabled = false;
        }
    }

    void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


}
