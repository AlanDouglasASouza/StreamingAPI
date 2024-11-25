using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StreamingAPI.Models;
using StreamingAPI.Data;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlaylistsController : ControllerBase
{
    private readonly StreamingContext _context;

    public PlaylistsController(StreamingContext context)
    {
        _context = context;
    }

    // Rota para criar uma nova playlist
    [HttpPost]
    public IActionResult Create(Playlist playlist)
    {
        // Verifica se os campos obrigatórios estão presentes
        if (string.IsNullOrEmpty(playlist.Name) || string.IsNullOrEmpty(playlist.Description))
        {
            return BadRequest("Name and Description are required.");
        }

        // Obtém o ID do usuário autenticado
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        // Associa a playlist ao usuário autenticado
        playlist.UserId = userId;

        // Adiciona a nova playlist ao banco de dados
        _context.Playlists.Add(playlist);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = playlist.Id }, playlist);
    }

    // Rota para obter todas as playlists do usuário autenticado
    [HttpGet]
    public IActionResult GetAll()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var playlists = _context.Playlists.Where(p => p.UserId == userId).Include(p => p.UserId)                            
            .Include(p => p.Contents).ToList();
        return Ok(playlists);
    }

    // Rota para obter uma playlist específica por ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var playlist = _context.Playlists.Include(p => p.UserId)
            .Include(p => p.Contents).FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (playlist == null)
        {
            return NotFound("Playlist not found.");
        }

        return Ok(playlist);
    }

    // Rota para atualizar uma playlist (PUT)
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Playlist playlist)
    {
        if (playlist == null)
        {
            return BadRequest("Playlist cannot be null.");
        }

        // Verifica se a Playlist existe
        var existingPlaylist = _context.Playlists
                                        .Include(p => p.Contents) // Inclui os conteúdos
                                        .FirstOrDefault(p => p.Id == id);

        if (existingPlaylist == null)
        {
            return NotFound("Playlist not found.");
        }

        // Atualiza os campos da Playlist, exceto o Id e UserId
        existingPlaylist.Name = playlist.Name;
        existingPlaylist.Description = playlist.Description;

        // Atualiza os Contents
        if (playlist.Contents != null && playlist.Contents.Any())
        {
            // Aqui você pode tratar o relacionamento entre a Playlist e seus Contents
            foreach (var content in playlist.Contents)
            {
                // Caso o conteúdo já exista, apenas atualiza, senão, cria um novo
                var existingContent = _context.Contents.FirstOrDefault(c => c.Id == content.Id);
                if (existingContent != null)
                {
                    // Atualiza os dados do conteúdo existente
                    existingContent.Title = content.Title;
                    existingContent.Type = content.Type;
                    // Atualize outros campos conforme necessário
                }
                else
                {
                    // Caso o conteúdo não exista, cria um novo
                    //content.PlaylistId = existingPlaylist.Id; // Define a relação com a playlist
                    _context.Contents.Add(content); // Adiciona o novo conteúdo
                }
            }
        }

        _context.SaveChanges(); // Salva as alterações no banco de dados

        return Ok(existingPlaylist); // Retorna a playlist atualizada
    }


    // Rota para deletar uma playlist (DELETE)
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var playlist = _context.Playlists.FirstOrDefault(p => p.Id == id && p.UserId == userId);

        if (playlist == null)
        {
            return NotFound("Playlist not found or you do not have access to it.");
        }

        _context.Playlists.Remove(playlist);
        _context.SaveChanges();
        return NoContent(); // Retorna 204 No Content
    }
}
