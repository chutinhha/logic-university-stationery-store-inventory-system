using System;
using System.ComponentModel.DataAnnotations;

namespace SA33.Team12.SSIS.DAL
{
    [MetadataType(typeof(BlackListLogMetaData))]
    public partial class BlacklistLog
    {
    }

    public class BlackListLogMetaData
    {
        [RegularExpression("...", ErrorMessage = "Ha ha, you won't be able to save!")]
        public DateTime DateBlacklisted { get; set; }
    }
}