using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department
    {
    }

    public class DepartmentMetaData
    {
        [RegularExpression(".{1,255}", ErrorMessage = "Department name cannot be longer then 255 character.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Department name is required.")]
        public string Name { get; set; }

        [RegularExpression(".{4}", ErrorMessage = "Department code must be 4 character.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Department code is required.")]
        public string Code { get; set; }

    }
}
