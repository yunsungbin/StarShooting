using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public Ranking unitPrefab;

    public Transform unitParent;
    public List<Ranking> unitList = new List<Ranking>();

    void Start()
    {
        var data = User.Instance.GetFiveTopRankers();

        foreach (var item in data)
        {
            var unit = Instantiate(unitPrefab, unitParent);
            unit.InitUnit(item);
            unitList.Add(unit);
        }

        unitPrefab.gameObject.SetActive(false);
    }
}
