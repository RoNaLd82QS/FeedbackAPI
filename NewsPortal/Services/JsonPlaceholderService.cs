using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsPortal.Models;

namespace NewsPortal.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _httpClient;

        public JsonPlaceholderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com/");
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>("posts");
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Post>($"posts/{id}");
        }

        public async Task<Author> GetAuthorAsync(int userId)
        {
            return await _httpClient.GetFromJsonAsync<Author>($"users/{userId}");
        }

        public async Task<List<Comment>> GetCommentsAsync(int postId)
        {
            return await _httpClient.GetFromJsonAsync<List<Comment>>($"comments?postId={postId}");
        }
    }
}
