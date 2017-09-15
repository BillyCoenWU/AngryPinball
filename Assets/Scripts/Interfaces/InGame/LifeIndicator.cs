namespace RGSMS
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class LifeIndicator : MonoBehaviour
    {
        private Text m_lifeText = null;
        private Text lifeText
        {
            get
            {
                if (m_lifeText == null)
                {
                    m_lifeText = GetComponent<Text>();
                }

                return m_lifeText;
            }
        }

        private Transform m_target = null;

        private Vector2 m_screenPos = Vector2.zero;
        private Vector2 m_viewPosition = Vector2.zero;
        
        [SerializeField]
        private RectTransform m_canvas = null;

        [SerializeField]
        private Vector2 m_offset = Vector2.zero;

        [SerializeField]
        private Color m_color = Color.white;
        [SerializeField]
        private Color m_alpha = Color.white;

        private RectTransform m_rectTransform = null;
        private RectTransform rectTransform
        {
            get
            {
                if(m_rectTransform == null)
                {
                    m_rectTransform = GetComponent<RectTransform>();
                }

                return m_rectTransform;
            }
        }

        private void Start()
        {
            m_alpha.a = 0.0f;
            lifeText.color = m_alpha;
        }

        public void SetTarget (Transform newTransform)
        {
            m_target = newTransform;
        }

        public void SetLife (int life)
        {
            if(life == 0)
            {
                gameObject.SetActive(false);
                return;
            }

            lifeText.text = life.ToString();

            StopAllCoroutines();
            StartCoroutine(AlphaColor());
        }

        private void Update()
        {
            if(m_target == null)
            {
                return;
            }

            m_viewPosition = CameraMan.Instance.mainCamera.WorldToViewportPoint(m_target.position);

            m_screenPos.x = (m_viewPosition.x * m_canvas.sizeDelta.x) - (m_canvas.sizeDelta.x * 0.5f);
            m_screenPos.y = ((m_viewPosition.y * m_canvas.sizeDelta.y) - (m_canvas.sizeDelta.y * 0.5f));

            rectTransform.anchoredPosition = m_screenPos + m_offset;
        }

        private IEnumerator AlphaColor ()
        {
            float lerp = 0.0f;
            lifeText.color = m_color;

            while(lerp < 1.0f)
            {
                lerp += Time.deltaTime / 2.5f;
                lifeText.color = Color.Lerp(m_color, m_alpha, lerp);

                yield return null;
            }
        }
    }
}