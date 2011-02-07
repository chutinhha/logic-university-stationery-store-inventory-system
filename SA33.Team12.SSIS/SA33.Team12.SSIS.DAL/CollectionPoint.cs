using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(CollectionPointMetaData))]
    public partial class CollectionPoint
    {
    }

    public class CollectionPointMetaData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Collection name is required.")]
        public string Name { get; set; }
    }
}
