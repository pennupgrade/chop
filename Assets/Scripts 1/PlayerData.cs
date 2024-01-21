using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string playerName;
    public int score;
    public int outfit;

    private void Awake()
    {
        score = 0;
        DontDestroyOnLoad(this.gameObject);
    }
}
