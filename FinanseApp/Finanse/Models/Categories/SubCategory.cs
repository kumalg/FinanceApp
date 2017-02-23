﻿namespace Finanse.Models.Categories {
    public class SubCategory : Category {

        public int BossCategoryId { get; set; }

        public SubCategory(Category category) {
            if (category is SubCategory)
                BossCategoryId = ((SubCategory)category).BossCategoryId;
            else
                BossCategoryId = -1;

            ColorKey = category.ColorKey;
            IconKey = category.IconKey;
            Id = category.Id;
            Name = category.Name;
            VisibleInExpenses = category.VisibleInExpenses;
            VisibleInIncomes = category.VisibleInIncomes;
        }

        public SubCategory() {
        }

        public override int GetHashCode() {
            return Name.GetHashCode() * Id;
        }
        public override bool Equals(object o) {
            if (!(o is SubCategory))
                return false;

            return
                base.Equals((Category)o) &&
                ((SubCategory)o).BossCategoryId == BossCategoryId;
        }
    }
}
