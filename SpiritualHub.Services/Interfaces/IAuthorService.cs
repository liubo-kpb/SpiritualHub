﻿namespace SpiritualHub.Services.Interfaces;

using Client.ViewModels.Author;
using SpiritualHub.Services.Models.Author;
using SpiritualHub.Data.Models;

public interface IAuthorService
{
    Task<FilteredAuthorsServiceModel> GetAllAsync(AllAuthorsQueryModel queryModel);

    Task<IEnumerable<AuthorIndexViewModel>> LastThreeAuthors();

    Task<string> CreateAuthor(AuthorFormModel newAuthor, Publisher publisher);

    Task<IEnumerable<AuthorViewModel>> AllAuthorsByPublisherId(string publisherId);

    Task<IEnumerable<AuthorViewModel>> AllAuthorsByUserId(string userId);

    Task<AuthorDetailsViewModel> GetAuthorDetailsAsync(string authorId);

    Task<bool> Exists(string authorId);

    Task Edit(AuthorFormModel editedAuthor);

    Task<bool> HasConnectedPublisher(string authorId, string userId);

    Task<AuthorFormModel> GetAuthorAsync(string authorId);
}
