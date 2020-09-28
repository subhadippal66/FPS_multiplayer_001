using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuname)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuname)
            {
                OpenMenu_(menus[i]);
            }
            else if (menus[i].open_)
            {
                menus[i].Close();
            }
        }
    }

    public void OpenMenu_(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open_)
            {
                menus[i].Close();
            }
            menu.Open();
        }
    }
}
