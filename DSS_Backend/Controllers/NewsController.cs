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
    public async Task < ActionResult < Article >> CreateArticle(Article article) {
      _context.Articles.Add(article);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetArticleById), new {
        id = article.id
      }, article);
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

    // POST: /article/:articleId/comment
    [HttpPost("article/{id}/comment")]
    public async Task < ActionResult < Comment >> CreateComment(int id, Comment comment) {
      var article = await _context.Articles.FindAsync(id);
      if (article == null) {
        return NotFound();
      }

      comment.articleId = id;
      _context.Comments.Add(comment);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetArticleById), new {
        id = comment.id
      }, comment);
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

    private bool ArticleExists(int id) {
      return _context.Articles.Any(e => e.id == id);
    }

    private bool CommentExists(int id) {
      return _context.Comments.Any(e => e.id == id);
    }
  }
}