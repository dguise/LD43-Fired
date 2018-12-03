using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private const float FLOOR_HEIGHT = 1.58f;
    private int ExpansionInterval = 100;
    public Transform kiosk;

    [SerializeField] private List<Transform> _floors;
    private GameObject[] _floorPrefabs;
    private GameObject tarp;
    private GameObject currentTarp;
    private UIScript _guiManager;
    private int _money = 0;
    private float duration = 1.0f; // duration in seconds
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _guiManager.ScoreText = "$" + value.ToString() + " / " + ExpansionInterval;
        }
    }

    private void Start()
    {
        _guiManager = GameObject.FindObjectOfType<UIScript>();
        InvokeRepeating("CalculateIncome", 0, 1.0f);
        _floorPrefabs = Resources.LoadAll<GameObject>("Floors");
        tarp = GameObject.Find("ConstructionTarp");
    }

    void CalculateIncome()
    {
        var workstations = GameObject.FindObjectsOfType<ArbetsplatsScript>();

        int income = 0;
        foreach (var workstation in workstations)
        {
            income += (workstation.HasAwesomeWorker)
                ? Settings.INCOME_PER_GOOD_WORKSTATION
                : Settings.INCOME_PER_WORKSTATION;
        }
        // Uncomment for debugging
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.M))
            Money += 10;
#endif
        Money += income;

        if (Money >= ExpansionInterval)
        {
            ExpansionInterval += ExpansionInterval;
            AddFloor();
        }
    }
   
    void AddFloor()
    {
        Transform v = Instantiate(_floorPrefabs[Random.Range(0, _floorPrefabs.Length - 1)].transform, new Vector3(0f, _floors.Count * FLOOR_HEIGHT, 0f), Quaternion.identity);
        v.gameObject.SetActive(false);
        currentTarp = GameObject.Instantiate(tarp, new Vector3(0f, _floors.Count * FLOOR_HEIGHT - (FLOOR_HEIGHT / 2)), Quaternion.identity);
        _floors.Add(v);
        StartCoroutine(Tarp(currentTarp));

        var waves = GameObject.FindObjectsOfType<Vagstigning>();
        foreach(var wave in waves) {
            wave.shouldRise = true;
        }
    }
    IEnumerator Tarp(GameObject tarp)
    {
        AudioManager.Instance.PlayRandomize(AudioManager.enumSoundType.Construction);
        currentTarp.transform.localScale = new Vector3(1, 0, 1);
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            if (tarp != null)
                currentTarp.transform.localScale = Vector3.Lerp(currentTarp.transform.localScale, new Vector3(1, 1, 1), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        kiosk.position += new Vector3(0f, FLOOR_HEIGHT, 0f);
        _floors[_floors.Count - 1].gameObject.SetActive(true);

        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            if (tarp != null)
                currentTarp.transform.localScale = Vector3.Lerp(currentTarp.transform.localScale, new Vector3(1, 0, 1), (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameObject.Destroy(currentTarp);
    }
}
