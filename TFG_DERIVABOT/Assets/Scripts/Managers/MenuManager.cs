using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    string lastScene;
    public void ButtonClickedLoadScene(string scene) 
    {
        
        SceneManager.LoadScene(scene);
    }

    public void ExitButtonClicked() =>
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Cierra la aplicación si no es editor.
            Application.Quit();
#endif

}
