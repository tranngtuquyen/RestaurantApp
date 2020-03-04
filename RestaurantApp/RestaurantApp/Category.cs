using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines a category to categorize different menu items
    /// </summary>
    class Category
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public Menu Menu { get; set; }
        public int MenuID { get; set; }
        #endregion
        
        #region Methods

        #endregion
    }
}
