using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ease.Data
{
    public class EasyMetadata
    {
        [Key]
        public string? Guid { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public string User { get; set; }
    }
}
