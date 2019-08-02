using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class script : MonoBehaviour
{
    public static int scoreValue = 0;
    public GUIStyle puanStili;
    private int skor = 0, bonusskor = 0, totalskor = 0, timercontroller = 0, startcontroller = 0, freezecontrol = 1;
    SerialPort sp = new SerialPort("COM3", 9600);
    public float zaman = 5, prevshot = 0, start = 3, farksure = 0, verilen, freeze = 10;
    
    public Text bonus;
    public GameObject bns;
    public Text timing;
    private int counter = 0;

    
    void Start()
    {
        
        sp.Open();
        zaman = 5;
        

    }

   
    void Update()
    {




        if (freezecontrol == 1)
        {
            timing.text = "Baslamak \n icin \n Butona Basiniz";
            freezecontrol = 0;
            bonus.text = bonusskor.ToString();
        }


        if (zaman <= 0)
        {
            timercontroller = 0;
            timing.text = "Sure bitti! \n \nSkor: "+skor;
            freeze -= Time.deltaTime;
            if (freeze <= 0)
            {
                zaman = 5;
                skor = 0;
                timercontroller = 0;
                startcontroller = 0;
                bonusskor = 0;
                start = 3;
                freezecontrol = 1;
            }

        }

        int seriDeger = int.Parse(sp.ReadLine());

        if (timercontroller == 1)
        {
            zaman -= Time.deltaTime;
            if (zaman >= 0)
            {
                timing.text = zaman.ToString("f0");
            }

            if (seriDeger == 1)
            {
                
                skor += 100;
                 bns.SetActive(false);

                farksure = prevshot - zaman;
                if (farksure > 0 && farksure < 1)
                {

                    bns.SetActive(true);
                    bonus.text = "Bonus:+25";
                    skor += 25;
                    
                }
                prevshot = zaman;
                
            }

        }
        if (startcontroller == 1)
        {
            start -= Time.deltaTime;
            timing.text = start.ToString("f0");
            if (start <= 1 && start >= 0)
            {
                timing.text = "Basla!!";
            }
            if (start <= 0)
            {
                timercontroller = 1;
                startcontroller = 0;
            }
        }

        if (seriDeger == 99)
        {
            startcontroller = 1;
        }

    }
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, 100), skor + "", puanStili);

    }
}
