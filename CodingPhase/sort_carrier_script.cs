using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sort_carrier_script : MonoBehaviour {

    private string sort_type;
    
    public void setsort(string name_of_sort)
    {
        sort_type = name_of_sort;
    }

    public string get_sort()
    {
        return sort_type;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public int correct_moves;
    public int wrong_moves;
    public float total_time_taken;
    public float number_of_restarts;

    public float get_final_score()
    {
        return 0.1f;
    }
}
