﻿namespace SpiritualHub.Data.Repository.Interface;

using SpiritualHub.Data.Models;

public interface IAuthorRepository : IRepository<Author>
{
    Task<IEnumerable<Author>?>  LastThreeAuthors();
    
    Task<Author?>               GetAuthorDetailsByIdAsync(string id);
    
    Task<Author?>               GetAuthorByIdWithAvatar(string id);
    
    Task<Author?>               GetAuthorWithPublishersAsync(string id);
    
    Task<Author?>               GetAuthorWithSubscriptionsAndSubscribersAsync(string id);
    
    Task<Author?>               GetAuthorWithSubscriptionsAsync(string id);
    
    Task<Author?>               GetAuthorWithFollowersAsync(string id);

    Task<Author?>               GetAuthorWithEntitiesAsync<TEntityType>(string id, string propertyName);
}
