using System.Collections.Generic;
using UnityEngine;

namespace HunterAllen.Menus
{
    public class MenuGroup : MonoBehaviour
    {
        public Menu ActiveMenu;

        void Awake()
        {
            if (ActiveMenu != null)
            {
                ActiveMenu._OnEnterMenu();
            }
        }

        public void EnterMenu(Menu menu)
        {
            if (ActiveMenu == menu)
            {
                return;
            }
            
            if (ActiveMenu != null)
            {
                ActiveMenu._OnExitMenu();
            }

            ActiveMenu = menu;

            ActiveMenu._OnEnterMenu();
        }
        public void ExitMenu(Menu menu)
        {
            menu._OnExitMenu();

            if (menu == ActiveMenu)
            {
                ActiveMenu = null;
            }
        }
    }
}