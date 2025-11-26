using UnityEngine;

public class PauseManger : MonoBehaviour
{

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f; 
            }
        }
        
    }
}
