using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string playerName;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
