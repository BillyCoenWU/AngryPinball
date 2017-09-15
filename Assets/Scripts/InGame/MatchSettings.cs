namespace RGSMS
{
    using Sound;
    using UnityEngine;

    public enum MOMENTS
    {
        DEFINE_FIRST_PLAYER,
        SETTINGS = 0,
        BALLS_ROLL,
        MATCH,
        END_MATCH
    }

    public class MatchSettings : Singleton<MatchSettings>
    {
        #region Variables

        [SerializeField]
        private BallData[] m_ballDatas = null;

        [SerializeField]
        private Cannon[] m_cannons = null;

        [SerializeField]
        public ShieldData[] m_shieldDatas = null;
        public int dataCount
        {
            get
            {
                return m_shieldDatas.Length;
            }
        }

        [SerializeField]
        private Shield[] m_playersOneShields = null;
        [SerializeField]
        private Shield[] m_playersTwoShields = null;

        private GameAudioObject m_audioGO = null;

        private int m_times = 0;
        private int m_currentTurn = 0;
        private int m_currentPlayer = 0;
        public int currentPlayer
        {
            get
            {
                return m_currentPlayer;
            }
        }

        private bool m_drawPlayerOne = false;
        private bool m_drawPlayerTwo = false;
        private bool m_playerOneIsReady = false;
        private bool m_playerTwoIsReady = false;
        
        private MOMENTS m_moment = MOMENTS.DEFINE_FIRST_PLAYER;
        public MOMENTS moment
        {
            get
            {
                return m_moment;
            }
        }

        #endregion

        private void Awake ()
        {
            Instance = this;
            m_audioGO = GetComponent<GameAudioObject>();
        }

        private void SettingsUpdate ()
        {
            if(m_playerOneIsReady && m_playerTwoIsReady)
            {
                NewTurn();
            }
        }

        private void UpdatePlayersBalls ()
        {
            CanvasControl.Instance.ActiveBallsIndicator();
            
            int count = m_cannons.Length;
            for(int i = 0; i < count; i++)
            {
                m_cannons[i].UpdateBalls();
                CanvasControl.Instance.SetPlayerBallCount(i, m_currentTurn);
            }

            SetNewMoment(MOMENTS.MATCH);
        }

        private void SetPlayersOneShieldsPosition ()
        {
            int count = m_playersOneShields.Length;
            for (int i = 0; i < count; i++)
            {
                m_playersOneShields[i].transform.position = new Vector3(CameraMan.Instance.minLimitX + 2.5f,  -1.5f + (i * 1.5f), 0.0f);
            }
        }

        private void SetPlayersTwoShieldsPosition ()
        {
            int count = m_playersTwoShields.Length;
            for (int i = 0; i < count; i++)
            {
                m_playersTwoShields[i].transform.position = new Vector3(CameraMan.Instance.maxLimitX - 2.5f, -1.5f + (i * 1.5f), 0.0f);
            }
        }

        public void NewTurn ()
        {
            m_currentTurn++;
            SetNewMoment(MOMENTS.BALLS_ROLL);
        }

        public void ResetGame ()
        {
            BallPool.Instance.RestoreBalls();
            LoadSceneManager.Instance.LoadScene(SCENE.MAIN_MENU);
        }

        public void EndPlayerTurn ()
        {
            m_times++;
            m_cannons[m_currentPlayer].ExcludeUpdate();
            m_currentPlayer = m_currentPlayer == 1 ? 0 : 1;
            
            if (m_times >= 2)
            {
                SoundManager.Instance.gameAudioLists[0].UpdatePitch();
                m_times = 0;
                NewTurn();
                return;
            }

            StartPlayerTurn();
        }

        public void StartPlayerTurn ()
        {
            m_cannons[m_currentPlayer].SetState(States.STATE.IDLE);
            m_cannons[m_currentPlayer].IncludeUpdate();
        }

        public void SetPlayerToDraw (int player)
        {
            if(player == 0)
            {
                m_drawPlayerOne = true;
            }

            if (player == 1)
            {
                m_drawPlayerTwo = true;
            }

            if(m_drawPlayerTwo && m_drawPlayerOne)
            {
                DrawFirstPlayer();
            }
        }

        private void DrawFirstPlayer ()
        {
            System.Random rand = new System.Random(Random.Range(int.MinValue, int.MaxValue));
            
            m_currentPlayer = (rand.Next(0, 100) > 50) ? 1 : 0;

            CanvasControl.Instance.ShowFirstPlayer(m_currentPlayer);
        }

        public void SetPlayerOneDone ()
        {
            m_playerOneIsReady = true;
            CanvasControl.Instance.ActiveButtons(m_currentPlayer, false);
            m_currentPlayer++;
            m_times++;

            if(m_times < 2)
            {
                StartPlayerState();
            }
        }

        public void SetPlayerTwoDone ()
        {
            m_playerTwoIsReady = true;
            CanvasControl.Instance.ActiveButtons(m_currentPlayer, false);
            m_currentPlayer = 0;
            m_times++;

            if (m_times < 2)
            {
                StartPlayerState();
            }
        }

        private void StartPlayerState ()
        {
            CanvasControl.Instance.ActiveButtons(m_currentPlayer, true);

            if (m_currentPlayer == 0)
            {
                CanvasControl.Instance.SetActivePlayerOneOrganizador(true);
                CanvasControl.Instance.SetActivePlayerTwoOrganizador(false);
                SetPlayersOneShieldsPosition();
            }
            else
            {
                CanvasControl.Instance.SetActivePlayerOneOrganizador(false);
                CanvasControl.Instance.SetActivePlayerTwoOrganizador(true);
                SetPlayersTwoShieldsPosition();
            }
        }

        public void Win (int playerindex)
        {
            m_audioGO.Play();
            SetNewMoment(MOMENTS.END_MATCH);
            SoundManager.Instance.gameAudioLists[0].ResetPitch();
            CanvasControl.Instance.ActiveFinalInterface(playerindex);
        }

        public BallData GetRandomBallData ()
        {
            return m_ballDatas[Random.Range(0, m_ballDatas.Length)];
        }

        public BallData GetBallData (int index)
        {
            return m_ballDatas[index];
        }

        public ShieldData GetShieldData (int index)
        {
            return m_shieldDatas[index];
        }

        public void SetNewMoment (MOMENTS momentoAtual)
        {
            switch (m_moment)
            {
                case MOMENTS.SETTINGS:
                    m_times = 0;
                    CanvasControl.Instance.SetActivePlayerOneOrganizador(false);
                    CanvasControl.Instance.SetActivePlayerTwoOrganizador(false);
                    UpdateManager.Instance.updates -= SettingsUpdate;
                    break;

                case MOMENTS.BALLS_ROLL:
                    break;

                case MOMENTS.MATCH:
                    break;

                case MOMENTS.END_MATCH:
                    break;
            }

            m_moment = momentoAtual;

            switch (m_moment)
            {
                case MOMENTS.SETTINGS:
                    CanvasControl.Instance.ActiveCenterField();
                    UpdateManager.Instance.updates += SettingsUpdate;
                    StartPlayerState();
                    break;

                case MOMENTS.BALLS_ROLL:
                    UpdatePlayersBalls();
                    break;

                case MOMENTS.MATCH:
                    StartPlayerTurn();
                    break;

                case MOMENTS.END_MATCH:
                    break;
            }
        } 
    }
}