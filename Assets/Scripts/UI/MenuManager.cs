using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button createWorld;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;




    }

    // Update is called once per frame
    void Update()
    {

        createWorld.onClick.AddListener(ButtonClick);
    }


    private void ButtonClick()
    {
        Debug.Log("Painoit nappia");

        // SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
