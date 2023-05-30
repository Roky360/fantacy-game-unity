using System;
using UnityEngine;

namespace Creatures.RegeneratableBar
{
    public enum BarDirection
    {
        HorizontalLeftToRight,
        HorizontalRightToLeft,
        VerticalBottomToTop,
        VerticalTopToBottom,
    }

    public class RegeneratableBar : MonoBehaviour
    {
        public GameObject barContents;

        public float value;
        public float maxValue = 100f;
        public float minValue = 0f;
        public BarDirection direction = BarDirection.HorizontalLeftToRight;
        public Action OnBarEmpty;
        public Action OnBarFull;
        public Action OnRegenStarts;
        public Action OnValueReduced;

        public float CurrentValue => value;

        /// How much to regenerate per second. Zero means the bar will not regenerate.
        public float regenPerSecond = 0;

        /// Wait this ling before start regeneration
        public float regenStartDelay = 0;

        /* private fields */
        private float barMinPosition;
        private float barMaxPosition;

        private bool _regenActive;
        private float _regenStartDelayLeft;

        public void SetValue(float newValue)
        {
            value = Math.Clamp(newValue, 0, maxValue);
            if (CurrentValue == 0)
            {
                OnBarEmpty?.Invoke();
            }
            else if (CurrentValue == maxValue)
            {
                OnBarFull?.Invoke();
            }

            // visual update
            float ratio = (value - minValue) / (maxValue - minValue);
            float newPos = barMinPosition + ratio * (barMaxPosition - barMinPosition);
            ChangeBarPosition(new Vector2(newPos, 0));
        }

        public void AddValue(float amount)
        {
            SetValue(CurrentValue + amount);
            // value reduced
            if (amount < 0)
            {
                _regenActive = false;
                _regenStartDelayLeft = regenStartDelay;

                OnValueReduced?.Invoke();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            SetBarPosition();

            value = maxValue;
            _regenActive = true;
            _regenStartDelayLeft = regenStartDelay;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                AddValue(-10);
            }

            if (!_regenActive)
            {
                _regenStartDelayLeft -= Time.deltaTime;
            }
            else if (regenPerSecond != 0 && value < maxValue)
            {
                AddValue(regenPerSecond * Time.deltaTime);
            }

            if (_regenStartDelayLeft <= 0)
            {
                _regenActive = true;
                _regenStartDelayLeft = regenStartDelay;

                OnRegenStarts?.Invoke();
            }
        }

        private void SetBarPosition()
        {
            RectTransform rectTransform = barContents.transform.GetComponent<RectTransform>();
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(rectTransform);

            switch (direction)
            {
                case BarDirection.HorizontalLeftToRight:
                    barMaxPosition = bounds.min.x;
                    barMinPosition = -bounds.max.x;
                    break;
                case BarDirection.HorizontalRightToLeft:
                    barMinPosition = bounds.max.x;
                    barMaxPosition = bounds.min.x;
                    break;
                case BarDirection.VerticalBottomToTop:
                    barMinPosition = bounds.max.y;
                    barMaxPosition = bounds.min.y;
                    break;
                case BarDirection.VerticalTopToBottom:
                    barMinPosition = bounds.min.y;
                    barMaxPosition = bounds.max.y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeBarPosition(Vector2 newPositionInPixels)
        {
            RectTransform imageRectTransform = barContents.GetComponent<RectTransform>();
            imageRectTransform.anchoredPosition = newPositionInPixels;
        }
    }
}