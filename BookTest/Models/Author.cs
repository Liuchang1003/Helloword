using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookTest.Models
{
    public class Author
    {
        [JsonProperty(PropertyName= "id")]
        public int Id { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        [Required]
        [Display(Name = "名")]

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "姓")]
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "biography")]
        public string Biography { get; set; }

        [JsonProperty(PropertyName = "books")]
        public ICollection<Book> Books { get; set; }  
    }
}