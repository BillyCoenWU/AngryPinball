namespace RGSMS
{
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;

    public class CanvasControl : Singleton<CanvasControl>
    {
        #region Variables

        [SerializeField]
        private Text[] m_ballsCountText = null;

        [SerializeField]
        private GameObject[] m_buttons = null;

        [SerializeField]
		private GameObject[] m_canvasBalls = null;

		[SerializeField]
		private GameObject[] m_basePlayerInterface = null;

        [SerializeField]
        private RectTransform[] m_timeBars = null;

        [SerializeField]
        private RectTransform[] m_powerBars = null;
        
        [SerializeField]
        private PlayerIndicator[] m_indicators = null;

        [SerializeField]
        private GameObject m_background = null;

        [SerializeField]
        private GameObject m_jogadorOneOrganizando = null;

        [SerializeField]
        private GameObject m_jogadorTwoOrganizando = null;

        [SerializeField]
        private Transform m_centerField = null;

        #endregion

        private void Awake()
        {
            Instance = this;
        }

        public void ActiveCenterField ()
        {
            StartCoroutine(DownCenterField());
        }

        public void ActiveBallsIndicator()
        {
            int count = m_canvasBalls.Length;
            for (int i = 0; i < count; i++)
            {
                m_canvasBalls[i].SetActive(true);
            }
        }

        public void ShowFirstPlayer (int player)
        {
            DeactiveInitialInterface();

            m_indicators[player].Active();
        }

        private void DeactiveInitialInterface ()
        {
            m_background.SetActive(false);
        }

        public void ActiveFinalInterface (int index)
        {
            m_indicators[index].WinActive();
        }

        public void SetActivePlayerOneOrganizador (bool b)
        {
            m_jogadorOneOrganizando.SetActive(b);
        }

        public void SetActivePlayerTwoOrganizador (bool b)
        {
            m_jogadorTwoOrganizando.SetActive(b);
        }

        public void ActiveButtons (int playerId, bool active)
        {
            m_buttons[playerId].SetActive(active);
        }

        public void ActiveFinalButton(int playerId)
        {
            m_buttons[playerId].SetActive(true);
        }

        public void ActiveBaseInterface (int playerId, bool active)
		{
			m_basePlayerInterface[playerId].SetActive(active);
		}

        public void UpdateTimeBar (int playerId, float scale)
        {
			m_timeBars[playerId].localScale = new Vector3(scale, 1.0f, 1.0f);
        }

        public void UpdatePowerBar (int playerId, float scale)
        {
			m_powerBars[playerId].localScale = new Vector3(scale, 1.0f, 1.0f);
        }

        public void SetPlayerBallCount (int playerId, int ballsCount)
        {
            m_ballsCountText[playerId].text = string.Concat(ballsCount.ToString(), "x");
        }
        
        private IEnumerator DownCenterField ()
        {
            float lerp = 0.0f;
            Vector3 startPosition = m_centerField.position;
            Vector3 finalPosition = Vector3.zero;
            
            while(lerp < 1.0f)
            {
                lerp += Time.deltaTime * 5.0f;
                m_centerField.position = Vector3.Lerp(startPosition, finalPosition, lerp);
                yield return null;
            }
        }
    }
}
