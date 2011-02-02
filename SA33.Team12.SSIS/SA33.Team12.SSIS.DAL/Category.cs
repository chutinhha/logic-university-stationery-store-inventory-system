using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
    }

    public class CategoryMetaData
    {
        [Required(ErrorMessage="Category name cannot be blank!")]
        [StringLength(255, ErrorMessage="Category name cannot be longer than 255 characters")]
        public string Name { get; set; }

    }
}