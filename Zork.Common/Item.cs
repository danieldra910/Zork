
using System.Collections.Generic;

namespace Zork.Common

{
    public class Item
    {
        public string Name { get; }

        public string Description { get; }

        public string InventoryDescription { get; }

        public Item(string name, string description, string inventoryDescription)
        {
            Name = name;
            Description = description;
            InventoryDescription = inventoryDescription;
        }

        public override string ToString()
        {
            return Name;
        }
        public static bool operator ==(Item a, Item b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return string.Compare(a.Name, b.Name, ignoreCase: true) == 0;
        }

        public static bool operator !=(Item a, Item b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Item other && other == this;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}