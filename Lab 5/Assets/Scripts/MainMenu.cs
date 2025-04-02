using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private AudioClip buttonClickSound;

    public void PlayGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void ButtonPressed()
    {
        SoundFXManager.instance.PlayButtonClick(buttonClickSound, transform, 1f);
    }
}
