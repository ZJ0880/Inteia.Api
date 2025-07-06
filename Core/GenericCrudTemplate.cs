using Inteia.Api.Core;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using MongoDB.Bson;


namespace Inteia.Api.Core;
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}

public interface IGenericService<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}

public class GenericService<T> : IGenericService<T> where T : BaseEntity
{
    private readonly IRepository<T> _repository;
    public GenericService(IRepository<T> repository) => _repository = repository;

    protected virtual Expression<Func<T,bool>> BuildSearchExpression(string? text) => _ => true;

public async Task<T?> GetByIdAsync(string id) => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _repository.FindAsync(predicate);

    public async Task<T> AddAsync(T entity) => await _repository.AddAsync(entity);

    public async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

    public async Task DeleteAsync(string id) => await _repository.DeleteAsync(id);
}

[ApiController]
[Route("api/[controller]")]
public abstract class GenericController<T> : ControllerBase where T : BaseEntity
{
    protected readonly IGenericService<T> _service;

    public GenericController(IGenericService<T> service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<T>>> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<T>> Get(string id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] T entity)
    {
    // Generar ID solo si no se envía
    if (string.IsNullOrEmpty(entity.Id))
    {
        entity.Id = ObjectId.GenerateNewId().ToString();
    }

    await _service.AddAsync(entity);
    return Created(string.Empty, entity);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, T entity)
    {
        if (id != Guid.Parse(entity.Id)) return BadRequest();
        await _service.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
