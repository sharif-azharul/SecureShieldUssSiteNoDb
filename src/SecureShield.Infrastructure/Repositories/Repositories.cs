using SecureShield.Application.Interfaces;
using SecureShield.Domain.Entities;
using SecureShield.Infrastructure.Data;

namespace SecureShield.Infrastructure.Repositories;

/// <summary>
/// In-memory repository implementations — no database required.
/// Static data (services, industries, clients, etc.) is read-only and loaded from StaticData.
/// Runtime data (career applications, contact messages) is stored in thread-safe lists
/// and persists for the lifetime of the application process.
/// </summary>

public class ServiceRepository : IServiceRepository
{
    public Task<IEnumerable<Service>> GetAllAsync() =>
        Task.FromResult(StaticData.Services.AsEnumerable());
    public Task<Service?> GetByIdAsync(Guid id) =>
        Task.FromResult(StaticData.Services.FirstOrDefault(s => s.Id == id))!;
    public Task<Service?> GetBySlugAsync(string slug) =>
        Task.FromResult(StaticData.Services.FirstOrDefault(s => s.Slug == slug))!;
    public Task AddAsync(Service s) { StaticData.Services.Add(s); return Task.CompletedTask; }
    public Task UpdateAsync(Service s) { /* no-op for in-memory */ return Task.CompletedTask; }
    public Task DeleteAsync(Guid id) {
        var e = StaticData.Services.FirstOrDefault(s => s.Id == id);
        if (e != null) StaticData.Services.Remove(e);
        return Task.CompletedTask;
    }
}

public class IndustryRepository : IIndustryRepository
{
    public Task<IEnumerable<Industry>> GetAllAsync() =>
        Task.FromResult(StaticData.Industries.AsEnumerable());
    public Task<Industry?> GetByIdAsync(Guid id) =>
        Task.FromResult(StaticData.Industries.FirstOrDefault(s => s.Id == id))!;
    public Task AddAsync(Industry s) { StaticData.Industries.Add(s); return Task.CompletedTask; }
    public Task UpdateAsync(Industry s) => Task.CompletedTask;
    public Task DeleteAsync(Guid id) {
        var e = StaticData.Industries.FirstOrDefault(s => s.Id == id);
        if (e != null) StaticData.Industries.Remove(e);
        return Task.CompletedTask;
    }
}

public class ClientRepository : IClientRepository
{
    public Task<IEnumerable<Client>> GetAllAsync() =>
        Task.FromResult(StaticData.Clients.AsEnumerable());
    public Task<Client?> GetByIdAsync(Guid id) =>
        Task.FromResult(StaticData.Clients.FirstOrDefault(s => s.Id == id))!;
    public Task AddAsync(Client s) { StaticData.Clients.Add(s); return Task.CompletedTask; }
    public Task UpdateAsync(Client s) => Task.CompletedTask;
    public Task DeleteAsync(Guid id) {
        var e = StaticData.Clients.FirstOrDefault(s => s.Id == id);
        if (e != null) StaticData.Clients.Remove(e);
        return Task.CompletedTask;
    }
}

public class TestimonialRepository : ITestimonialRepository
{
    public Task<IEnumerable<Testimonial>> GetAllAsync() =>
        Task.FromResult(StaticData.Testimonials.AsEnumerable());
    public Task<IEnumerable<Testimonial>> GetPublishedAsync() =>
        Task.FromResult(StaticData.Testimonials.Where(t => t.Published).AsEnumerable());
    public Task<Testimonial?> GetByIdAsync(Guid id) =>
        Task.FromResult(StaticData.Testimonials.FirstOrDefault(s => s.Id == id))!;
    public Task AddAsync(Testimonial s) { StaticData.Testimonials.Add(s); return Task.CompletedTask; }
    public Task UpdateAsync(Testimonial s) => Task.CompletedTask;
    public Task DeleteAsync(Guid id) {
        var e = StaticData.Testimonials.FirstOrDefault(s => s.Id == id);
        if (e != null) StaticData.Testimonials.Remove(e);
        return Task.CompletedTask;
    }
}

public class GalleryRepository : IGalleryRepository
{
    public Task<IEnumerable<GalleryItem>> GetAllAsync() =>
        Task.FromResult(StaticData.GalleryItems.AsEnumerable());
    public Task<GalleryItem?> GetByIdAsync(Guid id) =>
        Task.FromResult(StaticData.GalleryItems.FirstOrDefault(s => s.Id == id))!;
    public Task AddAsync(GalleryItem s) { StaticData.GalleryItems.Add(s); return Task.CompletedTask; }
    public Task UpdateAsync(GalleryItem s) => Task.CompletedTask;
    public Task DeleteAsync(Guid id) {
        var e = StaticData.GalleryItems.FirstOrDefault(s => s.Id == id);
        if (e != null) StaticData.GalleryItems.Remove(e);
        return Task.CompletedTask;
    }
}

public class GuardRepository : IGuardRepository
{
    public Task<IEnumerable<GuardProfile>> GetAllAsync() =>
        Task.FromResult(StaticData.GuardProfiles.AsEnumerable());
    public Task<GuardProfile?> GetByIdAsync(Guid id) =>
        Task.FromResult(StaticData.GuardProfiles.FirstOrDefault(s => s.Id == id))!;
    public Task AddAsync(GuardProfile s) { StaticData.GuardProfiles.Add(s); return Task.CompletedTask; }
    public Task UpdateAsync(GuardProfile s) => Task.CompletedTask;
    public Task DeleteAsync(Guid id) {
        var e = StaticData.GuardProfiles.FirstOrDefault(s => s.Id == id);
        if (e != null) StaticData.GuardProfiles.Remove(e);
        return Task.CompletedTask;
    }
}

public class CareerRepository : ICareerRepository
{
    public Task<IEnumerable<CareerApplication>> GetAllAsync()
    {
        lock (StaticData.Lock)
        {
            return Task.FromResult(StaticData.CareerApplications
                .OrderByDescending(c => c.CreatedAt)
                .ToList()
                .AsEnumerable());
        }
    }

    public Task<CareerApplication?> GetByIdAsync(Guid id)
    {
        lock (StaticData.Lock)
        {
            return Task.FromResult(StaticData.CareerApplications.FirstOrDefault(c => c.Id == id))!;
        }
    }

    public Task AddAsync(CareerApplication s)
    {
        lock (StaticData.Lock)
        {
            StaticData.CareerApplications.Add(s);
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(CareerApplication s) => Task.CompletedTask;

    public Task DeleteAsync(Guid id)
    {
        lock (StaticData.Lock)
        {
            var e = StaticData.CareerApplications.FirstOrDefault(c => c.Id == id);
            if (e != null) StaticData.CareerApplications.Remove(e);
        }
        return Task.CompletedTask;
    }

    public Task<int> CountNewAsync()
    {
        lock (StaticData.Lock)
        {
            return Task.FromResult(StaticData.CareerApplications.Count(c => c.Status == "new"));
        }
    }
}

public class ContactMessageRepository : IContactMessageRepository
{
    public Task<IEnumerable<ContactMessage>> GetAllAsync()
    {
        lock (StaticData.Lock)
        {
            return Task.FromResult(StaticData.ContactMessages
                .OrderByDescending(c => c.CreatedAt)
                .ToList()
                .AsEnumerable());
        }
    }

    public Task<ContactMessage?> GetByIdAsync(Guid id)
    {
        lock (StaticData.Lock)
        {
            return Task.FromResult(StaticData.ContactMessages.FirstOrDefault(c => c.Id == id))!;
        }
    }

    public Task AddAsync(ContactMessage s)
    {
        lock (StaticData.Lock)
        {
            StaticData.ContactMessages.Add(s);
        }
        return Task.CompletedTask;
    }

    public Task MarkReadAsync(Guid id)
    {
        lock (StaticData.Lock)
        {
            var e = StaticData.ContactMessages.FirstOrDefault(c => c.Id == id);
            if (e != null) e.IsRead = true;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        lock (StaticData.Lock)
        {
            var e = StaticData.ContactMessages.FirstOrDefault(c => c.Id == id);
            if (e != null) StaticData.ContactMessages.Remove(e);
        }
        return Task.CompletedTask;
    }

    public Task<int> CountUnreadAsync()
    {
        lock (StaticData.Lock)
        {
            return Task.FromResult(StaticData.ContactMessages.Count(c => !c.IsRead));
        }
    }
}

public class SiteSettingRepository : ISiteSettingRepository
{
    public Task<IEnumerable<SiteSetting>> GetAllAsync() =>
        Task.FromResult(StaticData.SiteSettings.AsEnumerable());

    public Task<string?> GetAsync(string key)
    {
        var s = StaticData.SiteSettings.FirstOrDefault(x => x.Key == key);
        return Task.FromResult(s?.Value);
    }

    public Task UpsertAsync(string key, string value)
    {
        var existing = StaticData.SiteSettings.FirstOrDefault(x => x.Key == key);
        if (existing != null) existing.Value = value;
        else StaticData.SiteSettings.Add(new SiteSetting { Key = key, Value = value });
        return Task.CompletedTask;
    }
}

/// <summary>
/// No-op unit of work — in-memory repositories don't need SaveChanges.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    public Task<int> SaveChangesAsync() => Task.FromResult(0);
}
