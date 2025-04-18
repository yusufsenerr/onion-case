namespace API.Common.Application.DTOs.Queries.Permission
{
    public class PermissionUserDto
    {
        public Guid PermissionId { get; set; }          // İzin ID
        public bool Create { get; set; }                // Create yetkisi
        public bool Read { get; set; }                  // Read yetkisi
        public bool Update { get; set; }                // Update yetkisi
        public bool Delete { get; set; }                // Delete yetkisi
        public bool Pdf { get; set; }                   // PDF yetkisi
        public bool Excel { get; set; }
    }
}
