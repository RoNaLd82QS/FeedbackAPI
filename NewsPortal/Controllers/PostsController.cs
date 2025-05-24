using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models;
using NewsPortal.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FeedbackAPI.Controllers
{
    public class PostsController : Controller
    {
        private readonly JsonPlaceholderService _jsonService;
        private readonly HttpClient _feedbackClient;

        public PostsController(JsonPlaceholderService jsonService, IHttpClientFactory clientFactory)
        {
            _jsonService = jsonService;
            _feedbackClient = clientFactory.CreateClient("feedback");
        }

        public async Task<IActionResult> Index()
            {
            var posts = await _jsonService.GetPostsAsync();
            return View(posts);
            }


        public async Task<IActionResult> Details(int id)
        {
            var post = await _jsonService.GetPostByIdAsync(id);
            if (post == null) return NotFound();

            var author = await _jsonService.GetAuthorAsync(post.UserId);
            var comments = await _jsonService.GetCommentsAsync(post.Id);

            var model = new EnrichedPost
            {
                Post = post,
                Author = author,
                Comments = comments
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(int postId, string sentimiento)
        {
            var feedback = new
            {
                postId = postId,
                sentimiento = sentimiento
            };

            var response = await _feedbackClient.PostAsJsonAsync("/api/feedback", feedback);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
            }

            return RedirectToAction("Details", new { id = postId });
        }
    }
}
