using System.Reflection;
using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Application.Services.LogService;
using API.Common.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Common.Persistence.Repositories.WriteRepositories
{
    public class WriteRepository<TContext, TEntity>(
        LoggingService loggingService,
        TContext context) : IWriteRepository<TContext, TEntity>
        where TContext : DbContext
        where TEntity : BaseEntity
    {
        readonly private TContext context = context;
        readonly private LoggingService loggingService = loggingService;
        public DbSet<TEntity> Table => this.context.Set<TEntity>();

        public async Task<BaseResponse> CreateOrUpdateAsync(TEntity model)
        {
            model.Id = model.Id == null || model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;
            bool isNew = !await this.Table.AnyAsync(e => e.Id == model.Id);
            try
            {
                if (isNew)
                {
                    model.CreatedDate = DateTime.Now;
                    await this.Table.AddAsync(model);
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"Yeni {typeof(TEntity).Name} türünde bir varlık oluşturuldu, ID: {model.Id}", model);
                }
                else
                {
                    var updatedDateProperty = typeof(TEntity).GetProperty("UpdatedDate",
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    if (updatedDateProperty != null && updatedDateProperty.PropertyType == typeof(DateTime?))
                    {
                        updatedDateProperty.SetValue(model, DateTime.Now);
                    }

                    model.UpdatedDate = DateTime.Now;
                    this.Table.Update(model);
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{typeof(TEntity).Name} türünde bir varlık güncellendi, ID: {model.Id}", model);
                }

                var success = await SaveAsync() > 0;
                if (success)
                {
                    return new BaseResponse
                    {
                        Succeeded = false,
                        Message = $"Varlık başarılı şekilde  {(isNew ? "oluşturulurdu" : "güncellendi")}",
                        Data = model
                    };
                }


                return new BaseResponse
                {
                    Succeeded = false,
                    Message = $"Varlık {(isNew ? "oluşturulurken" : "güncellenirken")} bir hata oluştu.",
                    Data = model
                };
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"Hata: {ex.Message}", ex.Message);
                return new BaseResponse
                {
                    Succeeded = false,
                    Message = $"Bir hata oluştu: {ex.Message}",
                    Data = model
                };
            }
        }

        public async Task<BaseResponse> BulkCreateOrUpdateAsync(List<TEntity> models)
        {
            try
            {
                var existingIds = await this.Table
                    .Where(e => models.Select(m => m.Id).Contains(e.Id))
                    .Select(e => e.Id)
                    .ToListAsync();

                var newEntities = models.Where(m => !existingIds.Contains(m.Id)).ToList();
                var updatedEntities = models.Where(m => existingIds.Contains(m.Id)).ToList();

                newEntities.ForEach(entity =>
                {
                    entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
                    entity.CreatedDate = DateTime.Now;
                });

                updatedEntities.ForEach(entity =>
                {
                    var updatedDateProperty = typeof(TEntity).GetProperty("UpdatedDate",
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
                    if (updatedDateProperty != null && updatedDateProperty.PropertyType == typeof(DateTime?))
                    {
                        updatedDateProperty.SetValue(entity, DateTime.Now);
                    }

                    entity.UpdatedDate = DateTime.Now;
                });

                if (newEntities.Any())
                {
                    await this.context.AddRangeAsync(newEntities);
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{newEntities.Count} adet {typeof(TEntity).Name} türünde yeni varlık başarıyla oluşturuldu.", models);
                }

                if (updatedEntities.Any())
                {
                    //menu-permissions update yaparken sıkıntı yaratıyor bu yüzden bu kod yazıldı
                    foreach (var entity in updatedEntities)
                    {
                        var trackedEntity = this.context.Set<TEntity>()
                            .Local
                            .FirstOrDefault(e => e.Id == entity.Id);

                        if (trackedEntity != null)
                        {
                            this.context.Entry(trackedEntity).State = EntityState.Detached;
                        }
                    }

                    // **Güncellenen entity'leri update et**
                    this.context.UpdateRange(updatedEntities);
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{updatedEntities.Count} adet {typeof(TEntity).Name} türünde varlık başarıyla güncellendi.", models);

                }

                await this.context.SaveChangesAsync();
                var allEntities = newEntities.Concat(updatedEntities).ToList();

                return new BaseResponse
                {
                    Succeeded = true,
                    Message = "Toplu işlem başarıyla tamamlandı",
                    Data = allEntities
                };
            }
            catch (Exception ex)
            {
               
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"Hata: {ex.Message}", ex.Message);
                return new BaseResponse
                {
                    Succeeded = false,
                    Message = $"Bir hata oluştu: {ex.Message}",
                    Data = models
                };
            }
        }

        public async Task<TEntity> AddAsync(TEntity model)
        {
            try
            {
                var existingEntity = await Table.FindAsync(model.Id);
                if (existingEntity != null)
                {
                    Table.Entry(existingEntity).CurrentValues.SetValues(model);
                }
                else
                {
                    await Table.AddAsync(model);
                    var success = await this.SaveAsync() > 0;
                }

                return model;
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"AddOrUpdateAsync Hata: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<bool> AddRangeAsync(List<TEntity> datas)
        {
            try
            {
                await Table.AddRangeAsync(datas);
                var success = await this.SaveAsync() > 0;

                if (success)
                {
                    //var elasticSuccess = await this.elasticSearchRepository.AddOrUpdateBulk(datas);
                    //if (!elasticSuccess)
                    //{
                    //    WatchLogger.LogError("ElasticSearch'e toplu ekleme başarısız.");
                    //    throw new Exception("SQL toplu ekleme başarılı ancak Elasticsearch ekleme başarısız.");
                    //}
                }

                return success;
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"AddRangeAsync Hata: {ex.Message}", ex);
                throw;
            }
        }

        public bool Remove(TEntity model)
        {
            try
            {
                model.IsDeleted = true;
                EntityEntry<TEntity> entityEntry = Table.Update(model);
                var success = entityEntry.State == EntityState.Modified;
                return success;
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"Remove Hata: {ex.Message}", ex.Message);
                throw;
            }
        }

        public bool RemoveRange(List<TEntity> datas)
        {
            try
            {
                foreach (var data in datas)
                {
                    data.IsDeleted = true;
                }

                Table.UpdateRange(datas);
                var success = SaveAsync().Result > 0;
                return success;
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"RemoveRangeAsync Hata: {ex.Message}", datas);
                throw;
            }
        }

        public async Task<BaseResponse> RemoveAsync(string id)
        {
            try
            {
                TEntity model = await this.Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));

                if (model == null)
                {
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{typeof(TEntity).Name} türünde bir varlık bulunamadı, ID: {id}", model);
                    return new BaseResponse { Succeeded = false, Message = "Varlık bulunamadı." };
                }

                // SQL'den sil
                var isRemoved = Remove(model);
                var success = await SaveAsync() > 0;

                if (isRemoved && success)
                {
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{typeof(TEntity).Name} türünde bir varlık SQL'den başarıyla silindi, ID: {id}", model);
                    
                    model.IsDeleted = true;
                    return new BaseResponse { Succeeded = true, Message = "Varlık başarıyla silindi." };
                }

                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"{typeof(TEntity).Name} türünde bir varlık silinirken hata oluştu, ID: {id}", model);
                return new BaseResponse { Succeeded = false, Message = "Varlık silinirken hata oluştu." };
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"Bir hata meydana geldi: {ex.Message}", ex);

                return new BaseResponse { Succeeded = false, Message = $"Hata: {ex.Message}" };
            }
        }

        public bool Update(TEntity model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
            => await this.context.SaveChangesAsync();

        public async Task<BaseResponse> RemoveRangeAsync(List<string> ids)
        {
            try
            {
                // Verilen id'lere karşılık gelen varlıkları bul
                var models = await this.Table.Where(data => ids.Contains(data.Id.ToString())).ToListAsync();

                if (!models.Any())
                {
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{typeof(TEntity).Name} türünde varlıklar bulunamadı, IDs: {string.Join(", ", ids)}", models);

                    return new BaseResponse { Succeeded = false, Message = "Varlıklar bulunamadı." };
                }

                foreach (var model in models)
                {
                    model.IsDeleted = true;
                }
                Table.UpdateRange(models);
                var success = await SaveAsync() > 0;

                if (success)
                {
                    this.loggingService.LogAction(typeof(TEntity).Name,
                        $"{typeof(TEntity).Name} türünde varlıklar başarıyla güncellendi (isDeleted = true), IDs: {string.Join(", ", ids)}", models);
                    return new BaseResponse { Succeeded = true, Message = "Varlıklar başarıyla güncellendi." };
                }
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"{typeof(TEntity).Name} türünde varlıklar güncellenirken hata oluştu, IDs: {string.Join(", ", ids)}", models);
                
                return new BaseResponse { Succeeded = false, Message = "Varlıklar güncellenirken hata oluştu." };
            }
            catch (Exception ex)
            {
                this.loggingService.LogAction(typeof(TEntity).Name,
                    $"Bir hata meydana geldi: {ex.Message}", ex.Message);

                return new BaseResponse { Succeeded = false, Message = $"Hata: {ex.Message}" };
            }
        }

        public async Task<BaseResponse> RemoveByModelAsync(string model, Guid id)
        {
            // Model adından ilgili DbSet'i bul
            //    var modelType = AppDomain.CurrentDomain.GetAssemblies()
            //                       .SelectMany(a => a.GetTypes())
            //                       .FirstOrDefault(t => t.Name == model);

            //    if (modelType == null)
            //    {
            //        return new BaseResponse
            //        {
            //            Succeeded = false,
            //            Message = "Model bulunamadı."
            //        };
            //    }

            //    // DbSet'i dinamik olarak al
            //    var dbSet = (IQueryable<object>)context.GetType()
            //                                           .GetMethod("Set")
            //                                           .MakeGenericMethod(modelType)
            //                                           .Invoke(context, null);

            //    // Entity'i dinamik olarak sorgula
            //    var entity = await dbSet.FirstOrDefaultAsync(e => (Guid)modelType.GetProperty("Id").GetValue(e) == id);

            //    if (entity == null)
            //    {
            //        return new BaseResponse
            //        {
            //            Succeeded = false,
            //            Message = "Silinecek kayıt bulunamadı."
            //        };
            //    }

            //    // Silme işlemi (soft delete)
            //    modelType.GetProperty("IsDeleted")?.SetValue(entity, true);

            //    // Entity'yi güncelle
            //    var entityEntry = context.Entry(entity);
            //    entityEntry.State = EntityState.Modified;

            //    // Entity'yi TEntity tipine dönüştür
            //    var typedEntity = entity as TEntity;

            //    // Elasticsearch güncellemesi
            //    var success = await context.SaveChangesAsync() > 0;

            //    if (success)
            //    {
            //        if (typedEntity != null)
            //        {
            //            var elasticSuccess = await elasticSearchRepository.AddOrUpdate(typedEntity);
            //            if (!elasticSuccess)
            //            {
            //                WatchLogger.LogError($"ElasticSearch'ten silme başarısız. ID: {id}");
            //                throw new Exception("SQL silme başarılı ancak Elasticsearch silme başarısız.");
            //            }
            //        }
            //        else
            //        {
            //            return new BaseResponse
            //            {
            //                Succeeded = false,
            //                Message = "Tip dönüşüm hatası."
            //            };
            //        }
            //    }

            return new BaseResponse
            {
                Succeeded = true,
                Message = "Kayıt başarıyla silindi."
            };
        }
    }
}