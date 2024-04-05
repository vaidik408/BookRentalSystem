using System.Text.Json.Serialization;

namespace BRS.Data
{
    public class BaseEntity
    {
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public int? UpdatedBy { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public DateTime? DeletedAt { get; set; }
        [JsonIgnore]
        public int? DeletedBy { get; set; }
    }
}
