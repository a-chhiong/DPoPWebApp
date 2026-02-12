using Domain.ValueObjects.DummyJson;

namespace Application.Interfaces;

public interface IDummyJsonAdapter
{
    Task<DummyUsers> FetchUser(string query);
    Task<DummyPosts> FetchPosts(string query);
    Task<DummyPosts.Post> AddPost(int userId, string title, string body);
    Task<DummyPosts.Post> UpdatePost(int id, string title, string body);
    Task<DummyPosts.Post> DeletePost(int id);
}