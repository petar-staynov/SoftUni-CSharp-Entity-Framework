using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace XmlProcessingExercise.Models
{
    public class User
    {
        public User()
        {
            this.ProductsSold = new List<Product>();
            this.ProductsSold = new List<Product>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }

        public ICollection<Product> ProductsSold { get; set; }
        public ICollection<Product> ProductsBought { get; set; }

    }
}
