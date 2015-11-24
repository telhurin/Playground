using UnityEngine;
using System.Collections.Generic;
 
public class BasicUI : MonoBehaviour
{
    void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemsList();
        if (itemList.Count == 0)
        {
            GUI.Box(new Rect(posX, posY, width, height), "No items");
        }

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);

            if (GUI.Button(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image)))
            {
                if (item == "health")
                {
                    Managers.Inventory.ConsumeItem("health");
                    Managers.Player.ChangeHealth(25);
                }
                else
                {
                    Managers.Inventory.EquipItem(item);
                }
            }
            posX += width + buffer;
        }

        string equipped = Managers.Inventory.equippedItem;
        if(equipped != null)
        {
            posX = Screen.width - (width + buffer);
            Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
        }

        posX = 10;
        posY += height + buffer;

        //foreach(string item in itemList)
        //{
        //    if(GUI.Button(new Rect(posX, posY, width, height), "Equip " + item))
        //    {
                
        //    }
        //    posX += width + buffer;
        //}       
    }
}