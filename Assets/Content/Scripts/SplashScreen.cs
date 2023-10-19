using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    private LevelProgressController _levelProgressController;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        _levelProgressController = new LevelProgressController();
    }
}