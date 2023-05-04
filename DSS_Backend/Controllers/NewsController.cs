using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSS_Backend.Models;
using DSS_Backend.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Backend.Controllers {
  [ApiController]
  [Route("[controller]")]
  public class NewsController: ControllerBase {
    private readonly DataContext _context;

    public NewsController(DataContext context) {
      _context = context;
    }

    public class ArticleDto
    {
        public string description { get; set; }
        public string title { get; set; }
        public string image { get; set; }
    }

    public class CommentDto
    {
        public string description { get; set; }
        public int articleId { get; set; }
    }


    // GET: /article
    [HttpGet("article")]
    public async Task < ActionResult < IEnumerable < Article >>> GetAllArticles() {
      return await _context.Articles.ToListAsync();
    }

    // GET: /article/:articleId
    [HttpGet("article/{id}")]
    public async Task < ActionResult < Article >> GetArticleById(int id) {
      var article = await _context.Articles.FindAsync(id);

      if (article == null) {
        return NotFound();
      }

      return article;
    }

    // POST: /article
    [HttpPost("article")]
    public async Task<ActionResult<Article>> CreateArticle([FromBody] ArticleDto articleDto)
    {
        var article = new Article
        {
            description = articleDto.description,
            title = articleDto.title,
            image = articleDto.image
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArticleById), new { id = article.id }, article);
    }

    // DELETE: /article/:articleId
    [HttpDelete("article/{id}")]
    public async Task < IActionResult > DeleteArticle(int id) {
      var article = await _context.Articles.FindAsync(id);
      if (article == null) {
        return NotFound();
      }

      _context.Articles.Remove(article);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    // POST: /article/{articleId}/comment
    [HttpPost("article/{articleId}/comment")]
    public async Task<ActionResult<Comment>> CreateComment(int articleId, [FromBody] CommentDto commentDto)
    {
        var article = await _context.Articles.FindAsync(articleId);
        if (article == null)
        {
            return NotFound();
        }

        var comment = new Comment
        {
            description = commentDto.description,
            articleId = commentDto.articleId
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetArticleById), new { id = article.id }, article);
    }


    // DELETE: /article/comment/:commentId
    [HttpDelete("article/comment/{id}")]
    public async Task < IActionResult > DeleteComment(int id) {
      var comment = await _context.Comments.FindAsync(id);
      if (comment == null) {
        return NotFound();
      }

      _context.Comments.Remove(comment);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    // GET: /comment/{id}
    [HttpGet("comment/{id}")]
    public async Task<ActionResult<Comment>> GetCommentById(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        return comment;
    }


    private bool ArticleExists(int id) {
      return _context.Articles.Any(e => e.id == id);
    }

    private bool CommentExists(int id) {
      return _context.Comments.Any(e => e.id == id);
    }
  }
}