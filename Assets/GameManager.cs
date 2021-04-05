using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static Object locker = new Object();
    public static GameManager instance {
        get
        {
            if(_instance == null)
            {
                lock(locker)
                {
                    if(_instance == null)
                    {
                        _instance = new GameManager();
                        DontDestroyOnLoad(_instance);
                    }
                }
            }
            return _instance;
        }
        private set {}
    }

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState= CursorLockMode.Locked;
    }
}
