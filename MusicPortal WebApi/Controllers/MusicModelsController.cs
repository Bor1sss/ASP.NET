using Microsoft.AspNetCore.Mvc;
using MusicPortal_WebApi.IRepository.GenreF;
using MusicPortal_WebApi.IRepository.Music;
using MusicPortal_WebApi.IRepository.User;
using MusicPortal_WebApi.Models.MusicModel;
using MusicPortal_WebApi.Models.User;

[ApiController]
[Route("api/[controller]")]
public class MusicModelsController : ControllerBase
{
    private readonly IMusicRep repo;
    private readonly IRepositoryUser repoU;
    private readonly IGenreRep genro;

    public MusicModelsController(IMusicRep r, IRepositoryUser u, IGenreRep g)
    {
        repo = r;
        repoU = u;
        genro = g;
    }

    [HttpGet("music")]
    public async Task<ActionResult<IEnumerable<Music>>> GetMusics()
    {
        return await repo.GetMusicList();
    }

    [HttpGet("music/{id}")]
    public async Task<ActionResult<Music>> GetMusic(int id)
    {
        return await repo.GetMusic(id);
    }

    [HttpGet("genre")]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        return await genro.GetGenresList();
    }

    [HttpGet("genre/{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        return await genro.GetGenre(id);
    }

    [HttpGet("user")]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await repoU.GetAllUsers();
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        return await repoU.GetUser(id);
    }

    [HttpDelete("music/{id}")]
    public async Task<IActionResult> DeleteMusic(int id)
    {
        var result = await repo.DeleteMusic(id);
        await repo.Save();
        return Ok(result);
    }

    [HttpDelete("genre/{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        await genro.Delete(id);
        await genro.Save();
        return Ok();
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await repoU.Delete(id);
        await repoU.Save();
        return Ok();
    }



    // PUT: api/Genre
    [HttpPut("genre")]
    public async Task<ActionResult<Genre>> PutGenre(Genre genre)
    {
        var existingGenre = await genro.GetGenre(genre.Id);
        if (existingGenre == null)
        {
            return NotFound();
        }

        genro.Update(genre);
        return Ok(genre);
    }

    // PUT: api/User
    [HttpPut("user")]
    public async Task<ActionResult<User>> PutUser(User user)
    {
        var existingUser = await repoU.GetUser(user.Id);
        if (existingUser == null)
        {
            return NotFound();
        }

       // await repoU.Update(user);
        return Ok(user);
    }

    // PUT: api/Music
    [HttpPut("music")]
    public async Task<ActionResult<Music>> PutMusic(Music music)
    {
        var existingMusic = await repo.GetMusic(music.Id);
        if (existingMusic == null)
        {
            return NotFound();
        }

        repo.Update(music);
        return Ok(music);
    }

    // POST: api/Genre
    [HttpPost("genre")]
    public async Task<ActionResult<Genre>> PostGenre(Genre genre)
    {
        await genro.Create(genre);
        return Ok(genre);
    }

    // POST: api/User
    [HttpPost("user")]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        await repoU.Create(user);
        return Ok(user);
    }

    // POST: api/Music
    [HttpPost("music")]
    public async Task<ActionResult<Music>> PostMusic(Music music)
    {
        await repo.Create(music);
        return Ok(music);
    }


}
