using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private const float FLOOR_HEIGHT = 1.58f;
    public Transform kiosk;

    [SerializeField] private List<Transform> _floors;
    private GameObject[] _floorPrefabs;
    private GameObject tarp;
    private GameObject currentTarp;
    private bool _tarping = false;
    private UIScript _guiManager;
    private int _money = 0;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _guiManager.ScoreText = "$" + value.ToString() + " / " + ExpansionInterval;
        }
    }

    private int ExpansionInterval = 100;

    private void Start()
    {
        _guiManager = GameObject.FindObjectOfType<UIScript>();
        InvokeRepeating("CalculateIncome", 0, 1.0f);
        _floorPrefabs = Resources.LoadAll<GameObject>("Floors");
        tarp = GameObject.Find("ConstructionTarp");
    }

    void Update() {
        var diff = ExpansionInterval - Money;
        currentTarp.transform.localScale = new  Vector3(
            currentTarp.transform.localScale.x, 
            diff / Settings.RangeForStartingTarp, 
            currentTarp.transform.localScale.z);
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

        Money += income;

        if (Money >= ExpansionInterval) {
            ExpansionInterval += ExpansionInterval;
            AddFloor();
            _tarping = false;
        }
        else if (Money > ExpansionInterval - _rangeForStartingTarp && !_tarping)
        {
            _tarping = true;
            currentTarp = GameObject.Instantiate(tarp, new Vector3(0f, _floors.Count * FLOOR_HEIGHT, 0f), Quaternion.identity);
        }
    }
   
    void AddFloor()
    {
        Transform v = Instantiate(_floorPrefabs[Random.Range(0, _floorPrefabs.Length - 1)].transform, new Vector3(0f, _floors.Count * FLOOR_HEIGHT, 0f), Quaternion.identity);
        _floors.Add(v);
        kiosk.position += new Vector3(0f, FLOOR_HEIGHT, 0f);
    }
}
