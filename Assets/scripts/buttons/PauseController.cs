using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false;

    // Método para alternar entre pausado e despausado
    public void TogglePause()
    {
        if (isPaused)
        {
            NoPausado();
        }
        else
        {
            Pausado();
        }
    }

    void Pausado()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    void NoPausado()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}