using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPage : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        LevelProgress.LoadCurrentLevel().Forget();
    }
}