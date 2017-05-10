using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject game_cube;

    /* can be the following values
     * "bubble_sort"
     * "insertion_sort"
     * "selection_sort"
     */
    private string sort_name;
    private int total_steps;
    private int present_step;
    private int swap_index;
    public int[,] steps_array_bubble = new int[29, 2];
    Vector3 high_vector = new Vector3(0, 0.5f, 0);
    Vector3 very_high = new Vector3(0, 1.0f, 0);
    private int[,] steps_array_insertion = new int[8, 2];
    private int[] steps_array_selection = new int[100];
    private bool[] verify_swap = new bool[7];
    private int[] sel_swap_number = new int[7];

    // change into constructor TODO
    public int[] numbers = new int[] {23, 46, 14, 65, 31, 19, 54, 6};

    private bool is_second;
    private bool do_swap;
    private GameObject first_cube;

    private bool is_entering;
    private GameObject previous;
    private GameObject current;
    private GameObject insertion_temp;

    // Use this for initialization
    void Start () {
        Vector3 temp_vector = new Vector3(-5.25f, 0, 0);
        GameObject my_temp_object;
        Transform temp_child;
        for (int i = 0; i < 8; i++)
        {
            my_temp_object = Instantiate(game_cube);

            my_temp_object.transform.position += temp_vector;
            my_temp_object.gameObject.name = i.ToString();
            temp_child = my_temp_object.transform.GetChild(0);
            temp_child.gameObject.GetComponent<TextMesh>().text = numbers[i].ToString();
            temp_vector.x += 1.5f;
        }

        present_step = 0;
        is_entering = true;
        is_second = false;
        do_swap = false;
        GameObject invincible = GameObject.Find("liver");
        sort_carrier_script lscript = invincible.GetComponent<sort_carrier_script>();
        sort_name = lscript.get_sort();
        if (sort_name == "selection_sort")
        {
            selection_steps(numbers);
            total_steps = steps_array_selection[99];
        }
        else if(sort_name == "insertion_sort")
        {
            insertion_steps(numbers);
            total_steps = steps_array_insertion[7, 0];
        }
        else if(sort_name == "bubble_sort")
        {
            bubble_steps(numbers);
            total_steps = steps_array_bubble[28, 0];
        }
    }

    void swap_numbers(GameObject touched_cube)
    {
        string number1, number2;
        string number3, number4;
        bool first = false;
        Transform text_3d_child1, text_3d_child2;
        if (sort_name == "selection_sort")
        {
            if (do_swap == false)
            {
                text_3d_child1 = touched_cube.transform.GetChild(0);
                number1 = text_3d_child1.gameObject.GetComponent<TextMesh>().text;
                number2 = steps_array_selection[present_step].ToString();
                if (number2 == number1)
                {
                    if (is_entering == true)
                    {
                        current = touched_cube;
                        current.transform.position += high_vector;
                        is_entering = false;
                    }
                    else
                    {
                        previous = current;
                        current = touched_cube;
                        current.transform.position += high_vector;
                        previous.transform.position -= high_vector;
                    }
                    present_step++;
                }
            }
            else
            {
                text_3d_child1 = touched_cube.transform.GetChild(0);
                number1 = text_3d_child1.gameObject.GetComponent<TextMesh>().text;
                number2 = sel_swap_number[swap_index].ToString();
                if (number1 == number2)
                {
                    Vector3 temp = touched_cube.transform.position;
                    touched_cube.transform.position = current.transform.position;
                    current.transform.position = temp;
                    do_swap = false;
                    present_step++;
                    if (present_step == total_steps)
                    {
                        SceneManager.LoadScene(2);
                    }
                }
            }
        }
        else if (sort_name == "bubble_sort")
        {
            if (is_second == false)
            {
                first_cube = touched_cube;
                first_cube.transform.position += high_vector;
                is_second = true;
            }
            else
            {
                if(present_step < total_steps)
                {
                    number1 = steps_array_bubble[present_step, 0].ToString();
                    number2 = steps_array_bubble[present_step, 1].ToString();

                    text_3d_child1 = first_cube.transform.GetChild(0);
                    text_3d_child2 = touched_cube.transform.GetChild(0);

                    number3 = text_3d_child1.gameObject.GetComponent<TextMesh>().text;
                    number4 = text_3d_child2.gameObject.GetComponent<TextMesh>().text;

                    if(((number1 == number3) && (number2 == number4)) || ((number1 == number4) && (number2 == number3)))
                    {
                        Vector3 temp = touched_cube.transform.position;
                        touched_cube.transform.position = first_cube.transform.position;
                        first_cube.transform.position = temp;
                        present_step++;
                        first = false;
                    }
                    else
                    {
                        first = true;
                    }

                    if(present_step == total_steps)
                    {
                        SceneManager.LoadScene(2);
                    }
                }
                if (first == false)
                {
                    touched_cube.transform.position -= high_vector;
                }
                else
                {
                    first_cube.transform.position -= high_vector;
                }
                is_second = false;
            }
        }
        else if(sort_name == "insertion_sort")
        {
            if (is_second == false)
            {
                first_cube = touched_cube;
                first_cube.transform.position += very_high;
                is_second = true;
            }
            else
            {
                if (present_step < total_steps)
                {
                    number1 = steps_array_insertion[present_step, 0].ToString();
                    number2 = steps_array_insertion[present_step, 1].ToString();

                    text_3d_child1 = first_cube.transform.GetChild(0);
                    text_3d_child2 = touched_cube.transform.GetChild(0);

                    number3 = text_3d_child1.gameObject.GetComponent<TextMesh>().text;
                    number4 = text_3d_child2.gameObject.GetComponent<TextMesh>().text;

                    if ((number1 == number3) && (number2 == number4))
                    {
                        string from_name = touched_cube.gameObject.name;
                        string to_name = first_cube.gameObject.name;
                        int tempo1 = int.Parse(from_name);
                        int tempo2 = int.Parse(to_name);
                        Vector3 temp = touched_cube.transform.position;
                        first_cube.gameObject.name = "flying";
                        for(int d = tempo2 - 1; d >= tempo1; d--)
                        {
                            insertion_temp = GameObject.Find(d.ToString());
                            insertion_temp.transform.position += new Vector3(1.5f, 0, 0);
                            insertion_temp.name = (d + 1).ToString();
                        }
                        first_cube.transform.position = temp;
                        first_cube.gameObject.name = tempo1.ToString();

                        present_step++;
                        first = false;
                    }
                    else
                    {
                        first = true;
                    }
                    if(first == true)
                    {
                        first_cube.transform.position -= very_high;
                    }

                    if (present_step == total_steps)
                    {
                        SceneManager.LoadScene(2);
                    }
                }
                is_second = false;
            }
        }
    }

    // swaps of bubble sort
    void bubble_steps(int[] real_numbers) 
    {
        // swaps counter
        int swaps_counter = 0;

        // for swapping
        int temp;

        // clone array ends within the function, is a copy of the array 'numbers'.
        int[] clone_numbers = new int[8];
        Array.Copy(real_numbers, clone_numbers, 8);

        for(int i = 7; i > 0; i--)
        {
            for(int j = 0; j < i; j++)
            {
                // steps_array([swaps_counter][0],[swaps_counter][1]) -- before swap -- then increment counter
                if(clone_numbers[j] > clone_numbers[j + 1])
                {
                    steps_array_bubble[swaps_counter, 0] = clone_numbers[j];
                    steps_array_bubble[swaps_counter, 1] = clone_numbers[j + 1];

                    temp = clone_numbers[j];
                    clone_numbers[j] = clone_numbers[j + 1];
                    clone_numbers[j + 1] = temp;

                    swaps_counter++;
                }
            }
        }

        steps_array_bubble[28, 0] = swaps_counter;
    }

    void selection_steps(int[] real_numbers)
    {
        // swaps counter
        int swaps_counter = 0;

        // for swapping
        int temp;

        // clone array ends within the function, is a copy of the array 'numbers'.
        int[] clone_numbers = new int[8];
        Array.Copy(real_numbers, clone_numbers, 8);

        int minimum_temp = 100;
        int minimum_index = 0;
        int minus_swaps = -1;

        for(int i = 0; i < 7; i++)
        {
            for(int j = i; j <= 7; j++)
            {
                if(clone_numbers[j] < minimum_temp)
                {
                    minimum_temp = clone_numbers[j];
                    minimum_index = j;
                    steps_array_selection[swaps_counter] = minimum_temp;
                    swaps_counter++;
                }
            }
            steps_array_selection[swaps_counter] = minus_swaps--;
            swaps_counter++;
            if (i != minimum_index)
            {
                verify_swap[i] = true;
                sel_swap_number[i] = clone_numbers[i];
                temp = clone_numbers[i];
                clone_numbers[i] = clone_numbers[minimum_index];
                clone_numbers[minimum_index] = temp;
            }
            else
            {
                verify_swap[i] = false;
            }
            minimum_index = 0;
            minimum_temp = 100;
        }
        steps_array_selection[99] = swaps_counter;
    }

    // swaps of insertion sort
    void insertion_steps(int[] real_numbers)
    {
        // swaps counter
        int swaps_counter = 0;

        // for swapping
        int temp;

        // clone array ends within the function, is a copy of the array 'numbers'.
        int[] clone_numbers = new int[8];
        Array.Copy(real_numbers, clone_numbers, 8);

        for(int i = 1; i < 8; i++)
        {
            for(int j = i - 1; j >= 0; j--)
            {
                if(clone_numbers[i] > clone_numbers[j])
                {

                    if ((i - 1) != j)
                    {
                        swaps_counter++;
                        temp = clone_numbers[i];
                        for (int h = i - 1; h >= (j + 1); h--)
                        {
                            clone_numbers[h + 1] = clone_numbers[h];
                        }
                        clone_numbers[j + 1] = temp;
                    }
                    break;
                }
                else
                {
                    steps_array_insertion[swaps_counter, 0] = clone_numbers[i];
                    steps_array_insertion[swaps_counter, 1] = clone_numbers[j];
                    if(j == 0)
                    {
                        swaps_counter++;
                        temp = clone_numbers[i];
                        for(int h = i - 1; h >= 0; h--)
                        {
                            clone_numbers[h + 1] = clone_numbers[h];
                        }
                        clone_numbers[0] = temp;
                    }
                }
            }
        }

        steps_array_insertion[7, 0] = swaps_counter;
    }

    public void swapchecker()
    {
        if (steps_array_selection[present_step] < 0)
        {
            swap_index = ((-1) * (steps_array_selection[present_step])) - 1;
            is_entering = true;
            current.transform.position -= high_vector;
            if (verify_swap[swap_index] == true)
            {
                do_swap = true;
            }
            else
            {
                present_step++;
                if(present_step == total_steps)
                {
                    SceneManager.LoadScene(2);
                }
            }
        }
    }
}