using System.Collections;
using UnityEngine;

namespace HunterAllen.Menus
{
    [RequireComponent(typeof(Menu))]
    public class MenuAnimator : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField]
        float _animationLength = 0.5f;

        [SerializeField]
        AnimationCurve _animationCurve;

        [Header("Canvas")]
        [SerializeField]
        float _offAlpha;

        [SerializeField]
        float _onAlpha;

        [Header("Transform")]
        [SerializeField]
        Vector2 _initialPosition;

        [SerializeField]
        Vector2 _positionOffset;

        [Space]
        [SerializeField]
        Vector2 _initialSize;

        [SerializeField]
        Vector2 _sizeOffset;

        Menu _menu;
        RectTransform _transform;
        Coroutine _currentAnimation;

        float elapsedTime;
        
        float _initAlpha;
        Vector2 _initPos;
        Vector2 _initSize;
        
        float _currentAlpha;
        Vector2 _currentPos;
        Vector2 _currentSize;

        float _finalAlpha;
        Vector2 _finalPos;
        Vector2 _finalSize;

        void OnEnable()
        {
            _menu = GetComponent<Menu>();
            _transform = GetComponent<RectTransform>();

            _menu.OnEnterMenu.AddListener(OnEnterMenu);
            _menu.OnExitMenu.AddListener(OnExitMenu);

            _menu.CanvasGroup.alpha = _menu.IsMenuActive ? _onAlpha : _offAlpha;
            _transform.localPosition = _initialPosition + (_menu.IsMenuActive ? _positionOffset : Vector2.zero);
            _transform.localScale = _initialSize + (_menu.IsMenuActive ? _sizeOffset : Vector2.zero);

            _initAlpha = _menu.CanvasGroup.alpha;
            _initPos = _transform.localPosition;
            _initSize = _transform.localScale;
        }
        void OnDisable()
        {
            if (_currentAnimation != null)
            {
                StopCoroutine(_currentAnimation);
            }
            
            _menu.OnEnterMenu.RemoveListener(OnEnterMenu);
            _menu.OnExitMenu.RemoveListener(OnExitMenu);
        }

        void OnEnterMenu()
        {
            if (_currentAnimation != null)
            {
                StopCoroutine(_currentAnimation);
            }

            _currentAnimation = StartCoroutine(AnimateMenu(true));
        }
        void OnExitMenu()
        {
            if (_currentAnimation != null)
            {
                StopCoroutine(_currentAnimation);
            }

            _currentAnimation = StartCoroutine(AnimateMenu(false));
        }

        IEnumerator AnimateMenu(bool isEntering)
        {
            elapsedTime = 0;

            _currentAlpha = _menu.CanvasGroup.alpha;
            _currentPos = _transform.localPosition;
            _currentSize = _transform.localScale;

            _finalAlpha = isEntering ? _onAlpha : _offAlpha;
            _finalPos = isEntering ? _initialPosition + _positionOffset : _initialPosition;
            _finalSize = isEntering ? _initialSize + _sizeOffset : _initialSize;

            while (elapsedTime < _animationLength)
            {
                elapsedTime += Time.deltaTime;

                _menu.CanvasGroup.alpha = Mathf.MoveTowards(_currentAlpha, _finalAlpha, _animationCurve.Evaluate(elapsedTime / _animationLength));
                _transform.localPosition = Vector2.Lerp(_currentPos, _finalPos, _animationCurve.Evaluate(elapsedTime / _animationLength));
                _transform.localScale = Vector2.Lerp(_currentSize, _finalSize, _animationCurve.Evaluate(elapsedTime / _animationLength));

                yield return new WaitForEndOfFrame();
            }
        }
    }
}