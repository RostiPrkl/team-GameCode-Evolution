using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public List<PlayerAttackController> attackSlots = new List<PlayerAttackController>(8);
    public int[] attackLevels = new int[8];
    public List<Image> attackUISlots = new List<Image>(8); 
    public List<PassiveItem> passiveSlots = new List<PassiveItem>(8);
    public int[] passiveLevels = new int[8];
    public List<Image> passiveUISlots = new List<Image>(8);

    [System.Serializable]
    public class AttackEvolution
    {
      public GameObject initalAttack;
      public PlayerAttackScriptableObject attackData;
    }

    [System.Serializable]
    public class PassiveEvolution
    {
      public GameObject initialPassive;
      public PassiveItemScriptableObject passiveData;
    }

    [System.Serializable]
    public class EvolutionUI
    {
      public TMP_Text evolutionName;
      public TMP_Text evolutionDescription;
      public Image evolutionIcon;
      public Button evolutionButton;
    }

    public List<AttackEvolution> attackEvolutions = new List<AttackEvolution>();
    public List<PassiveEvolution> passiveEvolutions = new List<PassiveEvolution>();
    public List<EvolutionUI> evolutionUIoptions = new List<EvolutionUI>();

	PlayerStats player;


	void Start()
	{
		player = GetComponent<PlayerStats>();
	}


   public void AddAttack(int slotIndex, PlayerAttackController attack)
   {
        attackSlots[slotIndex] = attack;
        attackLevels[slotIndex] = attack.attackData.Level;
        attackUISlots[slotIndex].enabled = true;
        attackUISlots[slotIndex].sprite = attack.attackData.Icon;

		if (GameManager.instance != null && GameManager.instance.chooseUpgrade)
			GameManager.instance.EndEvolution();
   }


   public void AddPassive(int slotIndex, PassiveItem passive)
   {
        passiveSlots[slotIndex] = passive;
        passiveLevels[slotIndex] = passive.passiveItem.Level;
        passiveUISlots[slotIndex].enabled = true;
        passiveUISlots[slotIndex].sprite = passive.passiveItem.Icon;

		if (GameManager.instance != null && GameManager.instance.chooseUpgrade)
			GameManager.instance.EndEvolution();
   }


    public void LvlUpAttack(int slotIndex)
    {
      if (attackSlots.Count > slotIndex)
      {
          PlayerAttackController attack = attackSlots[slotIndex];
          if(!attack.attackData.AttackPrefab)
          {
             Debug.Log("NO NEXT ATTACK LEVEL");
             return;
          }
          GameObject leveledAttack = Instantiate(attack.attackData.NextLevelPrefab, transform.position, Quaternion.identity);
          leveledAttack.transform.SetParent(transform);
          AddAttack(slotIndex, leveledAttack.GetComponent<PlayerAttackController>());
          Destroy(attack.gameObject);
          attackLevels[slotIndex] = leveledAttack.GetComponent<PlayerAttackController>().attackData.Level;

		if (GameManager.instance != null && GameManager.instance.chooseUpgrade)
			GameManager.instance.EndEvolution();
     }
   }


   public void LvlUpPassive(int slotIndex)
   {
     if (passiveSlots.Count > slotIndex)
      {
        PassiveItem playerPassive = passiveSlots[slotIndex];
        if (!playerPassive.passiveItem.NextLevelPrefab)
        {
            Debug.Log("NO NEXT PASSIVE LEVEL");
            return;
        }
        GameObject leveledPassive = Instantiate(playerPassive.passiveItem.NextLevelPrefab, transform.position, Quaternion.identity);
        leveledPassive.transform.SetParent(transform);
        AddPassive(slotIndex, leveledPassive.GetComponent<PassiveItem>());
        Destroy(playerPassive.gameObject);
        passiveLevels[slotIndex] = leveledPassive.GetComponent<PassiveItem>().passiveItem.Level;

		if (GameManager.instance != null && GameManager.instance.chooseUpgrade)
			GameManager.instance.EndEvolution();
      }
   }


   void ApplyEvolution()
   {
	//JFC
      	foreach (var evolutionOption in evolutionUIoptions)
      	{
        	int evolutionType = Random.Range(1,3);
        	if (evolutionType == 1)
        	{
        		AttackEvolution chosenAttackEvolution = attackEvolutions[Random.Range(0, attackEvolutions.Count)];

        		if (chosenAttackEvolution != null)
                {
        			bool newAttack = false;
					for (int i = 0; i < attackSlots.Count; i++)
					{
						if (attackSlots[i] != null && attackSlots[i].attackData == chosenAttackEvolution.attackData)
						{
							newAttack = false;
							if (!newAttack)
							{
								evolutionOption.evolutionButton.onClick.AddListener(() => LvlUpAttack(i));
								evolutionOption.evolutionDescription.text = chosenAttackEvolution.attackData.NextLevelPrefab.GetComponent<PlayerAttackController>().attackData.AttackDescription;
								evolutionOption.evolutionName.text = chosenAttackEvolution.attackData.NextLevelPrefab.GetComponent<PlayerAttackController>().attackData.AttackName;
							}
							break;
						}
						else
							newAttack = true;
					}
					if (newAttack)
					{
						evolutionOption.evolutionButton.onClick.AddListener(() => player.SpawnAttack(chosenAttackEvolution.initalAttack));
						evolutionOption.evolutionDescription.text = chosenAttackEvolution.attackData.AttackDescription;
						evolutionOption.evolutionName.text = chosenAttackEvolution.attackData.AttackName;
					}
					
					evolutionOption.evolutionIcon.sprite = chosenAttackEvolution.attackData.Icon;
				}
        	}
			else if (evolutionType == 2)
			{
				PassiveEvolution chosenPassiveEvolution = passiveEvolutions[Random.Range(0, passiveEvolutions.Count)];

				if (chosenPassiveEvolution != null)
				{
					bool newPassive = false;
					for (int i = 0; i < passiveSlots.Count; i++)
					{
						if (passiveSlots[i] != null && passiveSlots[i].passiveItem == chosenPassiveEvolution.passiveData)
						{
							newPassive = false;
							if (!newPassive)
							{
								evolutionOption.evolutionButton.onClick.AddListener(() => LvlUpPassive(i));
								evolutionOption.evolutionDescription.text = chosenPassiveEvolution.passiveData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItem.PassiveDescription;
								evolutionOption.evolutionName.text = chosenPassiveEvolution.passiveData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItem.PassiveName;
							}
							break;
						}
						else
							newPassive = true;
					}
					if (newPassive)
					{
						evolutionOption.evolutionButton.onClick.AddListener(() => player.SpawnPassive(chosenPassiveEvolution.initialPassive));
						evolutionOption.evolutionDescription.text = chosenPassiveEvolution.passiveData.PassiveDescription;
						evolutionOption.evolutionName.text = chosenPassiveEvolution.passiveData.PassiveName;
					}

					evolutionOption.evolutionIcon.sprite = chosenPassiveEvolution.passiveData.Icon;
				}
			}
      	}
   	}


	void RemoveEvolutions()
	{
		foreach (var evolutionOption in evolutionUIoptions)
		{
			evolutionOption.evolutionButton.onClick.RemoveAllListeners();
		}
	}


	public void ApplyAndRemoveEvolution()
	{
		RemoveEvolutions();
		ApplyEvolution();
	}
}