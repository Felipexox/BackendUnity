using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour {
    [SerializeField]
    private List<GameObject> m_pCups;

    [SerializeField]
    private GameObject m_pBall;

    private int currentIndexCupHidden;

    private static float ShowPositionY = 1.1f;

    private static float HiddenPositionY = 0.926f;

    private static float STOPTIME = 0.5f;

    private float timeToHiddenCup = 1f;

    private float timetToShowCup = 1f;

    private float timeToSetBall = 2f;

    private float timeToExchange = 1f;

    private int shuffleCount = 3;

    private bool isBallToMovement = false;

    private void Start()
    {
        currentIndexCupHidden = 1;
        ShowAllCups();
    }

    private void Update()
    {
        Vector3 position = m_pCups[currentIndexCupHidden].transform.position;
        position.y = m_pBall.transform.position.y;

        if (!isBallToMovement)
        {
            m_pBall.transform.position = position;
        }
    }

    IEnumerator ShuffleRunner(int count)
    {
        m_pBall.SetActive(false);
        for(int i = 0; i < count; i++)
        {
            int index1 = -1;
            int index2 = -1;

            do
            {
                index1 = Random.Range(0, 3);
                index2 = Random.Range(0, 3);
            } while (index1 == index2);

            ExchangeCups(m_pCups[index1], m_pCups[index2]);
            yield return new WaitForSeconds(timeToExchange);
        }
        m_pBall.SetActive(true);
        yield return ChoiceCup();
    }

    IEnumerator ChoiceCup()
    {
        int index = -1;
        yield return new WaitUntil(() => ( index = GetIndexByClick()) != -1);

        ShowCup(index);
        yield return new WaitForSeconds(timetToShowCup + timeToHiddenCup);
        if (timeToExchange > 0.25f)
        {
            timeToExchange -= 0.1f;
        }
        shuffleCount++;

        ShowAllCups();
    }

    void ShowCup(int index)
    {
        Vector3 position = m_pCups[index].transform.position;
        position.y = ShowPositionY;

        Hashtable hs = new Hashtable();

        hs.Add("position", position);

        hs.Add("oncompletetarget", gameObject);

        hs.Add("oncomplete", "HiddenCup");

        hs.Add("oncompleteparams", index);

        hs.Add("time", timetToShowCup);

        iTween.MoveTo(m_pCups[index], hs);
    }

    void HiddenCup(int index)
    {

        Vector3 position = m_pCups[index].transform.position;
        position.y = HiddenPositionY;

        Hashtable hs = new Hashtable();

        hs.Add("position", position);

        hs.Add("oncompletetarget", gameObject);

        hs.Add("oncomplete", "HiddenCup(" + index + ")");

        hs.Add("time", timeToHiddenCup);

        iTween.MoveTo(m_pCups[index], hs);
    }

    void SetBallInCup()
    {
        isBallToMovement = true;
        int indexChoice = Random.Range(0, 3);
        Vector3 position = m_pCups[indexChoice].transform.position;

        position.y = m_pBall.transform.position.y;

        Hashtable hs = new Hashtable();

        hs.Add("position", position);

        hs.Add("oncompletetarget", gameObject);

        hs.Add("oncomplete", "HiddenAllCups");

        hs.Add("time", timeToSetBall);

        iTween.MoveTo(m_pBall, hs);
        currentIndexCupHidden = indexChoice;
    }

    void ShowAllCups()
    {
        for (int i = 0; i < m_pCups.Count; i++)
        {
            Vector3 position = m_pCups[i].transform.position;
            position.y = ShowPositionY;

            Hashtable hs = new Hashtable();

            hs.Add("position", position);
            if (i == 0)
            {
                hs.Add("oncompletetarget", gameObject);

                hs.Add("oncomplete", "SetBallInCup");
            }
            hs.Add("time", timetToShowCup);

            iTween.MoveTo(m_pCups[i], hs);
        }
    }

    void HiddenAllCups()
    {
        isBallToMovement = false;
        for (int i = 0; i < m_pCups.Count; i++)
        {
            Vector3 position = m_pCups[i].transform.position;
            position.y = HiddenPositionY;

            Hashtable hs = new Hashtable();

            hs.Add("position", position);


            if (0 == i)
            {
                hs.Add("oncompletetarget", gameObject);
                hs.Add("oncomplete", "Shuffle");
                hs.Add("oncompleteparams", shuffleCount);
            }
            hs.Add("time", timetToShowCup);

            iTween.MoveTo(m_pCups[i], hs);
        }

    }
    void Shuffle(int count)
    {
        StartCoroutine(ShuffleRunner(count));
    }
    void ExchangeCups(GameObject cup1, GameObject cup2)
    {
        Vector3 position1 = cup1.transform.position;
        Vector3 position2 = cup2.transform.position;

        Vector3[] path1 = new Vector3[] {position1 ,new Vector3((position1.x + position2.x)/2, position1.y, -8.24f), position2};
        Vector3[] path2 = new Vector3[] { position2, new Vector3((position1.x + position2.x) / 2, position2.y, -7.88f), position1 };


        Hashtable hs1 = new Hashtable();

        hs1.Add("time", timeToExchange);
        hs1.Add("path", path1);
        hs1.Add("easetype", iTween.EaseType.easeInOutSine);

        Hashtable hs2 = new Hashtable();

        hs2.Add("time", timeToExchange);
        hs2.Add("path", path2);
        hs2.Add("easetype", iTween.EaseType.easeInOutSine);


        iTween.MoveTo(cup1, hs1);
        iTween.MoveTo(cup2, hs2);
    }

    public int CurrentIndexCupHidden
    {
        get
        {
            return currentIndexCupHidden;
        }
    }



    public int GetIndexByClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < m_pCups.Count; i++)
                {
                    if (m_pCups[i] == hit.collider.gameObject)
                    {
                        return i;
                    }
                }
            }
        }
        return -1;
    }
	
}
