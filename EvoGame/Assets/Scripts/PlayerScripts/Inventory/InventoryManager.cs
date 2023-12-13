using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<PlayerAttackController> attackSlots = new List<PlayerAttackController>(8);
    public int[] attackLevels = new int[8];
    public List<Image> attackUISlots = new List<Image>(8); 
    public List<PassiveItem> passiveSlots = new List<PassiveItem>(8);
    public int[] passiveLevels = new int[8];
    public List<Image> passiveUISlots = new List<Image>(8);


   public void AddAttack(int slotIndex, PlayerAttackController attack)
   {
        attackSlots[slotIndex] = attack;
        attackLevels[slotIndex] = attack.attackData.Level;
        attackUISlots[slotIndex].enabled = true;
        attackUISlots[slotIndex].sprite = attack.attackData.Icon;
   }


   public void AddPassive(int slotIndex, PassiveItem passive)
   {
        passiveSlots[slotIndex] = passive;
        passiveLevels[slotIndex] = passive.passiveItem.Level;
        passiveUISlots[slotIndex].enabled = true;
        passiveUISlots[slotIndex].sprite = passive.passiveItem.Icon;
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
      }
   }
}
