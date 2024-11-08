using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ease.Data
{
    public class EasyMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? EaseGuid { get; set; }
        
        public string? UserName { get; set; }

        public DateTimeOffset? Expires { get; set; }

        public EasyMetadata(string userName)
        {
            EaseGuid = Guid.NewGuid();
            UserName = userName;
            Expires = DateTime.Now.AddDays(30);
        }

        public EasyMetadata(Guid guid, string userName)
        {
            EaseGuid = guid;
            UserName = userName;
            Expires = DateTime.Now.AddDays(30);
        }

    }
}
