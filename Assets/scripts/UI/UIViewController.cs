using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIViewController : MonoBehaviour
    {
        private static UIViewController _instance;
        public static UIViewController Instance => _instance;

        private Hashtable _UIPages;
        private List<UIPage> _onList;
        private List<UIPage> _offList;

        public UIPage[] UIPages;
        public PageType EntryUIPage;
        [HideInInspector] public PageType CurrentUIPage;

        private void Awake()
        {
            if (Instance == null)
            {
                _instance = this;
                _UIPages = new Hashtable();
                _onList = new List<UIPage>();
                _offList = new List<UIPage>();

                RegisterAllUIPages();

                CurrentUIPage = EntryUIPage;

                if (EntryUIPage != PageType.None)
                {
                    TurnUIPageOn(EntryUIPage);
                }
            }
        }

        public void TurnUIPageOn(PageType type, bool closeCurrentUIPage = false, Action callback = null)
        {
            if (type == PageType.None)
            {
                return;
            }

            if (UIPageExists(type) == false)
            {
                return;
            }

            if (closeCurrentUIPage)
            {
                TurnUIPageOff(CurrentUIPage, type);
            }
            else
            {
                UIPage UIPage = GetUIPage(type);
                UIPage.gameObject.SetActive(true);
                UIPage.SetState(true);
                CurrentUIPage = type;
            }
            callback?.Invoke();
        }

        public void TurnUIPageOff(PageType offType, PageType onType = PageType.None, bool waitForExit = false)
        {
            if (offType == PageType.None)
            {
                return;
            }

            if (UIPageExists(offType) == false)
            {
                return;
            }

            UIPage offUIPage = GetUIPage(offType);

            if (offUIPage.gameObject.activeSelf)
            {
                offUIPage.SetState(false);
            }

            if (waitForExit && offUIPage.UseAnimation)
            {
                UIPage onUIPage = GetUIPage(onType);
                StopCoroutine("WaitForUIPageExit");
                StartCoroutine(WaitForUIPageExit(onUIPage, offUIPage));
            }
            else
            {
                TurnUIPageOn(onType);
            }
        }

        private IEnumerator WaitForUIPageExit(UIPage onUIPage, UIPage offUIPage)
        {
            while (offUIPage.TargetState != UIPage.FLAG_NONE)
            {
                yield return null;
            }
            TurnUIPageOn(onUIPage.Type);
        }

        public bool IsUIPageOn(PageType type)
        {
            if (UIPageExists(type) == false)
            {
                return false;
            }

            return GetUIPage(type).IsOn;
        }

        private UIPage GetUIPage(PageType type)
        {
            if (UIPageExists(type) == false)
            {
                return null;
            }

            return (UIPage)_UIPages[type];
        }

        private bool UIPageExists(PageType type)
        {
            return _UIPages.Contains(type);
        }

        private void RegisterAllUIPages()
        {
            foreach (UIPage UIPage in UIPages)
            {
                RegisterUIPage(UIPage);
            }
        }

        private void RegisterUIPage(UIPage UIPage)
        {
            if (UIPageExists(UIPage.Type))
            {
                return;
            }

            _UIPages.Add(UIPage.Type, UIPage);
            TurnUIPageOff(UIPage.Type);
        }
    }
}
