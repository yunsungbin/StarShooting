using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sctiptable : MonoBehaviour
{
    public User user;
    public TempData temp;

    private void Start()
    {
        user.Init();
        temp.InitTempData();
    }
}
