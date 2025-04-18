namespace API.Common.Application.DTOs.Commands.Autentication
{
    public class MenuPermission
    {
        public Guid Id { get; set; }
        public Guid MenuItemId { get; set; }
        public bool Add { get; set; }
        public bool Read { get; set; }
        public bool Delete { get; set; }
        public bool Update { get; set; }
        public bool Excel { get; set; }
        public bool Pdf { get; set; }
    }
}