using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsStats : MonoBehaviour
{   
    public Dictionary<string, Dictionary<string, Vector3>> allStats;

    #region Weapons Dictionary's
    public Dictionary<string, Vector3> M4Stats = new Dictionary<string, Vector3>();
    public Dictionary<string, int> M4Bullets = new Dictionary<string, int>();
    public Dictionary<string, Vector3> PistolStats = new Dictionary<string, Vector3>();
    public Dictionary<string, int> PistolBullets = new Dictionary<string, int>();

    #endregion
    //####################################################################################//
    private void InitializeStats()
    {   
        // M4Stats
        M4Stats.Add("Position", new Vector3(0.05555471f, 1.25f, 0f));
        M4Stats.Add("Scale", new Vector3(0.5f, 0.5f, 0f));
        M4Stats.Add("FirePointPos", new Vector3(1.43f, 0.24f, 0f));

        M4Bullets.Add("Comb", 30);
        M4Bullets.Add("Reserve", 120);
        //######################//

        // PistolStats
        PistolStats.Add("Position", new Vector3(0.05555471f, 1.102223f, 0f));
        PistolStats.Add("Scale", new Vector3(0.3333334f, 0.4444447f, 0f));
        PistolStats.Add("FirePointPos", new Vector3(1.16f, 0.54f, 0f));

        PistolBullets.Add("Comb", 8);
        PistolBullets.Add("Reserve", 16);
    }

    private void Awake()
    {   
        allStats = new Dictionary<string, Dictionary<string, Vector3>>
        {
            { "M4", M4Stats },
            { "Pistol", PistolStats }
        };

        InitializeStats();
    }
}
