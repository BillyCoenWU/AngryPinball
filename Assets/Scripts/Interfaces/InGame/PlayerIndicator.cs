namespace RGSMS
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class PlayerIndicator : MonoBehaviour
    {
        [SerializeField]
        private Text m_text = null;

        private bool m_startMoment = true;
        
        [SerializeField]
        private GameObject m_endbutton = null;

        private RectTransform m_rectTransform = null;
        public RectTransform rectTransform
        {
            get
            {
                if (m_rectTransform == null)
                {
                    m_rectTransform = GetComponent<RectTransform>();
                }

                return m_rectTransform;
            }
        }

        private const string PLAY_FIRST = "You will go first !";
        private const string PLAYER_WIN = "YOU WIN !";

        public void Active ()
        {
            rectTransform.localScale = Vector3.zero;

            m_text.text = PLAY_FIRST;

            gameObject.SetActive(true);

            StartCoroutine(ScaleText());
        }

        public void WinActive()
        {
            rectTransform.localScale = Vector3.zero;

            m_text.text = PLAYER_WIN;

            gameObject.SetActive(true);

            StartCoroutine(ScaleText());
        }

        public IEnumerator ScaleText ()
        {
            float lerp = 0.0f;

            while (lerp < 1.0f)
            {
                lerp += Time.unscaledDeltaTime * 5.0f;

                transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(1.2f, 1.2f, 1.2f), lerp);

                yield return null;
            }

            StartCoroutine(AjustImage());
        }

        public IEnumerator AjustImage()
        {
            float lerp = 0.0f;

            while (lerp < 1.0f)
            {
                lerp += Time.unscaledDeltaTime * 5.0f;

                transform.localScale = Vector3.Lerp(new Vector3(1.2f, 1.2f, 1.2f), Vector3.one, lerp);

                yield return null;
            }

            if (m_startMoment)
            {
                m_startMoment = false;
                Invoke("Deactive", 1.5f);
            }
            else
            {
                m_endbutton.SetActive(true);
            }
        }

        private void Deactive ()
        {
            MatchSettings.Instance.SetNewMoment(MOMENTS.SETTINGS);
            gameObject.SetActive(false);
        }
    }
}
