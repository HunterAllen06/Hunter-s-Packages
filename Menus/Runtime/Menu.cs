using System;
using UnityEngine;
using UnityEngine.Events;

namespace HunterAllen.Menus
{
    public class Menu : MonoBehaviour
    {
        public MenuGroup ParentMenuGroup;

        public CanvasGroup CanvasGroup;

        public UnityEvent OnEnterMenu;
        public UnityEvent OnExitMenu;

        public bool IsMenuActive { get; private set; }

        public void EnterMenu()
        {
            if (ParentMenuGroup != null)
            {
                ParentMenuGroup.EnterMenu(this);
            }
        }
        public void ExitMenu()
        {
            if (ParentMenuGroup != null)
            {
                ParentMenuGroup.ExitMenu(this);
            }
        }

        public void _OnEnterMenu()
        {
            OnEnterMenu.Invoke();
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
            IsMenuActive = true;
        }
        public void _OnExitMenu()
        {
            OnExitMenu.Invoke();
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
            IsMenuActive = false;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (ParentMenuGroup == null && GetComponentInParent<MenuGroup>() != null)
            {
                ParentMenuGroup = GetComponentInParent<MenuGroup>();
            }
            if (CanvasGroup == null && GetComponent<CanvasGroup>() != null)
            {
                CanvasGroup = GetComponent<CanvasGroup>();
            }
        }
#endif
    }
}