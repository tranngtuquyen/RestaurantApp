using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantApp
{
    /// <summary>
    /// Defines a category to categorize different menu items
    /// </summary>
    public class Category
    {
        #region Properties
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public Menu Menu { get; set; }
        public int MenuID { get; set; }
        public string UserID { get; set; }
        #endregion
        
        #region Methods

        #endregion
    }
}
