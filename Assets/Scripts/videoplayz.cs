using UnityEditor.Media;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class videoplayz : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button vP;
    void Start()
    {
        Button button = vP.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TaskOnClick()
    {
        print("CLICK");
        SceneManager.LoadScene(0);
    }



}
