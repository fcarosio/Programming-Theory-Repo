using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScreenUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI partialTime;
    [SerializeField] TextMeshProUGUI totalTime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTime(int partialTime, int totalTime)
    {
        this.partialTime.text = TimeUtils.ToTime(partialTime);
        this.totalTime.text = TimeUtils.ToTime(totalTime);
    }
}
