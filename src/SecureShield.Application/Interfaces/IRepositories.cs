using SecureShield.Domain.Entities;

namespace SecureShield.Application.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service?> GetByIdAsync(Guid id);
    Task<Service?> GetBySlugAsync(string slug);
    Task AddAsync(Service service);
    Task UpdateAsync(Service service);
    Task DeleteAsync(Guid id);
}

public interface IIndustryRepository
{
    Task<IEnumerable<Industry>> GetAllAsync();
    Task<Industry?> GetByIdAsync(Guid id);
    Task AddAsync(Industry industry);
    Task UpdateAsync(Industry industry);
    Task DeleteAsync(Guid id);
}

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(Guid id);
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteAsync(Guid id);
}

public interface ITestimonialRepository
{
    Task<IEnumerable<Testimonial>> GetAllAsync();
    Task<IEnumerable<Testimonial>> GetPublishedAsync();
    Task<Testimonial?> GetByIdAsync(Guid id);
    Task AddAsync(Testimonial testimonial);
    Task UpdateAsync(Testimonial testimonial);
    Task DeleteAsync(Guid id);
}

public interface IGalleryRepository
{
    Task<IEnumerable<GalleryItem>> GetAllAsync();
    Task<GalleryItem?> GetByIdAsync(Guid id);
    Task AddAsync(GalleryItem item);
    Task UpdateAsync(GalleryItem item);
    Task DeleteAsync(Guid id);
}

public interface IGuardRepository
{
    Task<IEnumerable<GuardProfile>> GetAllAsync();
    Task<GuardProfile?> GetByIdAsync(Guid id);
    Task AddAsync(GuardProfile guard);
    Task UpdateAsync(GuardProfile guard);
    Task DeleteAsync(Guid id);
}

public interface ICareerRepository
{
    Task<IEnumerable<CareerApplication>> GetAllAsync();
    Task<CareerApplication?> GetByIdAsync(Guid id);
    Task AddAsync(CareerApplication application);
    Task UpdateAsync(CareerApplication application);
    Task DeleteAsync(Guid id);
    Task<int> CountNewAsync();
}

public interface IContactMessageRepository
{
    Task<IEnumerable<ContactMessage>> GetAllAsync();
    Task<ContactMessage?> GetByIdAsync(Guid id);
    Task AddAsync(ContactMessage message);
    Task MarkReadAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<int> CountUnreadAsync();
}

public interface ISiteSettingRepository
{
    Task<IEnumerable<SiteSetting>> GetAllAsync();
    Task<string?> GetAsync(string key);
    Task UpsertAsync(string key, string value);
}

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
