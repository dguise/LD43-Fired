using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private const float FLOOR_HEIGHT = 1.58f;
    private const int INCOME_PER_GOOD_WORKSTATION = 1;
    private const int INCOME_PER_WORKSTATION = 0;
    public Transform kiosk;

    [SerializeField] private List<Transform> _floors;
    private GameObject[] _floorPrefabs;

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
    }

    void CalculateIncome()
    {
        var workstations = GameObject.FindObjectsOfType<ArbetsplatsScript>();

        int income = 0;
        foreach (var workstation in workstations)
        {
            income = 10;                
            income += (workstation.HasAwesomeWorker)
                ? INCOME_PER_GOOD_WORKSTATION 
                : INCOME_PER_WORKSTATION;
        }

        Money += income;

        if (Money >= ExpansionInterval) {
            ExpansionInterval += (_floors.Count * 100);
            AddFloor();
        }
    }

    void AddFloor()
    {
        Transform v = Instantiate(_floorPrefabs[Random.Range(0, _floorPrefabs.Length - 1)].transform, new Vector3(0f, _floors.Count * FLOOR_HEIGHT, 0f), Quaternion.identity);
        _floors.Add(v);
        kiosk.position += new Vector3(0f, FLOOR_HEIGHT, 0f);
    }
}
