using API.Common.Domain.Commons;

namespace API.Common.Application.DTOs.Queries.Customer
{
    public class GetAllCustomerDto:BaseEntity
    {
        public string FirstName { get; set; } // Adı
        public string LastName { get; set; }  // Soyadı
        public string Title { get; set; }     // Ünvanı
        public string CustomerType { get; set; } // Müşteri Tipi
        public string? Note { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        // Checkboxes
        public Guid? CustomerAdditionalDetailsId { get; set; }
        public Guid? CustomerOtherInfoId { get; set; }
        public Guid? CustomerSmsEmailId { get; set; }
        public Guid? CustomerTaxAndIdentityId { get; set; }
        public Guid? CustomerCommissionsId { get; set; }

        public bool IsKvkkApproved { get; set; } // Kvkk
        public bool IsEtkApproved { get; set; }  // Etk
        public bool IsExplicitConsentGiven { get; set; } // Açık Rıza Onayı
        public bool IsMarketingConsentGiven { get; set; } // Pazarlama Onayı
        public bool IsInfoFormAccepted { get; set; } // Bilgilendirme Formu
    }
}
