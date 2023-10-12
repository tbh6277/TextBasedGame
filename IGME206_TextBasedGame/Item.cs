using System;

namespace IGME206_TextBasedGame
{
	internal class Item
	{
		private string itemName = "";
		private string itemDescription = "";
		private Ghost? owner;

		internal Item(string itemName)
		{
			this.itemName = itemName;
		}

		internal Item(string itemName, string itemDescription)
		{
			this.itemName = itemName;
			this.itemDescription = itemDescription;
		}

		internal string ItemName
		{
			get { return this.itemName; }
		}

		internal Ghost Owner
		{
			set { owner = value; }
			get { return owner; }
		}

		public override string ToString()
		{
			string s = this.itemName;
			if (itemDescription != "")
			{
				s += ": " + itemDescription;
			}
			return s;
		}

		public override bool Equals(object obj)
		{
            return obj != null && (obj as Item).ItemName == this.itemName;
        }

	}
}