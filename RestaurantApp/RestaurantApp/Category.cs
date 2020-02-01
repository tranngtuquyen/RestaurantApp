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
        private static int lastIDNumber = 0;
        public int ID { get; private set; }
        public string Name { get; set; }
        #endregion
        #region Constructors
        public Category()
        {
            ID = ++lastIDNumber;
        }
        #endregion

        #region Methods

        #endregion
    }
}
