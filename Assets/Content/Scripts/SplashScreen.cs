using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    private LevelProgressController _levelProgressController;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        
        _levelProgressController = new LevelProgressController();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _levelProgressController.Test(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _levelProgressController.Test(1);
    }
}