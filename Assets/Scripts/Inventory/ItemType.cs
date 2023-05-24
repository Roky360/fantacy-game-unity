using UnityEngine;

namespace Inventory
{
    public enum InvItemType
    {
        WoodenLog,
        Mushroom,
        EmptyBottle,
        Bone,
        Stone,
    }

    public class InvItem
    {
        public InvItemType Type;
        public GameObject GameObject;

        public InvItem(InvItemType type, GameObject gameObject)
        {
            Type = type;
            GameObject = gameObject;
        }
    }
}