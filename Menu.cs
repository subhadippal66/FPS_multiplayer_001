using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;
    public bool open_;

    public void Open()
    {
        open_ = true;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        open_ = false;
        gameObject.SetActive(false);
    }
}
