using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public UIDocument UIDocument;
    public int points = 0;

    private float health;
    private Label HealthLabel;
    private Label pointsLabel;
    private Label WaveLabel;
    private Label EnemyLabel;
    private Label getReadyToNextWave;

    private HealthComponent playerhealth;

    private CombatManager combatManager;


    void OnEnable()
    {
        points = 0;
        //pointsLabel.style.color = Color.yellow;  //gabisa
        GameObject player = GameObject.Find("Player");
        playerhealth = player.GetComponent<HealthComponent>();
        health = playerhealth.GetHealth;

        combatManager = FindObjectOfType<CombatManager>();

        var root = UIDocument.rootVisualElement;
        var Tracker1 = root.Q<VisualElement>("Tracker1");
        var Tracker2 = root.Q<VisualElement>("Tracker2");

        HealthLabel = Tracker1.Q<Label>("Health");
        pointsLabel = Tracker1.Q<Label>("Point");
        WaveLabel = Tracker2.Q<Label>("Wave");
        EnemyLabel = Tracker2.Q<Label>("Enemies");
        getReadyToNextWave = Tracker2.Q<Label>("GetReady");
        getReadyToNextWave.style.display = DisplayStyle.None;

        pointsLabel.text = "Points: " + points.ToString();
        HealthLabel.text = "Health: " + health.ToString();
        WaveLabel.text = "Wave: " + combatManager.waveNumber.ToString();
    }

    public void UpdatePoint(int amount)
    {
        points += amount;
        pointsLabel.text = "Points: " + points.ToString();
    }

    public void Update()
    {
        health = playerhealth.GetHealth;
        HealthLabel.text = "Health: " + health.ToString();

        EnemyLabel.text = "Enemies Remaining: " + combatManager.totalEnemies.ToString();
        WaveLabel.text = "Wave: " + (combatManager.waveNumber-1).ToString();

        if(combatManager.totalEnemies == 0)
        {
            getReadyToNextWave.style.display = DisplayStyle.Flex;
            getReadyToNextWave.text = "Next wave in " + Mathf.CeilToInt(combatManager.getInterval()-combatManager.timer).ToString();
        }
        else
        {
            getReadyToNextWave.style.display = DisplayStyle.None;
        }
    }
}