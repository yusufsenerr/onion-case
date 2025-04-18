using Application.Abstractions.User;
using Application.DTOs.Authentication;
using Application.Interfaces;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using WatchDog;

namespace Persistence.Services.User
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Identity.User> userManager;
        readonly IWriteRepository<Group> groupWriteRepository;
        readonly IWriteRepository<UserGroup> userGroupWriteRepository;
        readonly IReadRepository<Group> readRepository;
        readonly RoleManager<Role> roleManager;

        public UserService(
            UserManager<Domain.Identity.User> userManager, 
            IWriteRepository<Group> groupWriteRepository, 
            IWriteRepository<UserGroup> userGroupWriteRepository,
            RoleManager<Role> roleManager, 
            IReadRepository<Group> readRepository
            )
        {
            this.userManager = userManager;
            this.groupWriteRepository = groupWriteRepository;
            this.userGroupWriteRepository = userGroupWriteRepository;
            this.roleManager = roleManager;
            this.readRepository = readRepository;
        }

        public async Task<BaseResponse> CreateUser(CreateUser model)
        {
            try
            {
                var data = new Domain.Identity.User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                // Rolü kontrol et, yoksa oluştur
                var role = await this.roleManager.FindByNameAsync(model.RoleName);
                if (role == null)
                {
                    role = new Role { Name = model.RoleName };
                    var roleResult = await this.roleManager.CreateAsync(role);
                    if (!roleResult.Succeeded)
                    {
                        WatchLogger.LogError($"Rol oluşturulurken bir hata meydana geldi: {roleResult.Errors}");
                        return new BaseResponse { Succeeded = false, Message = "Rol oluşturulurken bir hata meydana geldi!" };
                    }
                    WatchLogger.Log($"Rol oluşturuldu: {model.RoleName}");
                }

                data.RoleId = role.Id;

                var result = await this.userManager.CreateAsync(data, model.Password);

                if (result.Succeeded)
                {
                    WatchLogger.Log($"Kullanıcı başarıyla oluşturuldu: {model.Username}");

                    // Kullanıcıyı role ata
                    await this.userManager.AddToRoleAsync(data, model.RoleName);
                    WatchLogger.Log($"Kullanıcıya rol atandı: {model.RoleName}");

                    // Kullanıcıyı gruplara ekle
                    foreach (var groupId in model.GroupIds)  // Birden fazla grup için GroupIds listesi kullanılıyor
                    {
                        var group = await this.readRepository.GetByIdAsync(groupId.ToString());
                        if (group != null)
                        {
                            var userGroup = new UserGroup
                            {
                                UserId = data.Id,
                                GroupId = group.Id
                            };
                            await this.userGroupWriteRepository.AddAsync(userGroup);
                            WatchLogger.Log($"Kullanıcı gruba eklendi: {group.Name}");
                        }
                        else
                        {
                            WatchLogger.LogWarning($"Grup bulunamadı: {groupId}");
                        }
                    }

                    BaseResponse responses = new() { Succeeded = result.Succeeded, Message = "Kullanıcı başarılı şekilde kaydedildi!" };
                    return responses;
                }
                else
                {
                    WatchLogger.LogError($"Kullanıcı oluşturulurken bir hata meydana geldi: {result.Errors}");
                    BaseResponse responses = new() { Succeeded = result.Succeeded, Message = "Kullanıcı oluşturulurken bir hata meydana geldi!" };
                    return responses;
                }
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Kullanıcı oluşturulurken bir hata meydana geldi: {ex}");
                return new BaseResponse { Succeeded = false, Message = $"Bir hata meydana geldi: {ex.Message}" };
            }
        }
    }
}
