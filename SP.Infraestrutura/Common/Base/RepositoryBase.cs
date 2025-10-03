using Microsoft.EntityFrameworkCore;
using SP.Infraestrutura.Data.Context;
using SP.Infraestrutura.Common.Exceptions;

namespace SP.Infraestrutura.Common.Base
{
    public abstract class RepositoryBase<T>(SPContext context) where T : class
    {
        protected readonly SPContext Context = context;
        protected readonly DbSet<T> DbSet = context.Set<T>();

        // Métodos básicos de consulta
        public virtual async Task<T?> ObterPorIdAsync(int id) =>
            await DbSet.FindAsync(id);

        public virtual async Task<T> ObterPorIdObrigatorioAsync(int id) =>
            await ObterPorIdAsync(id) 
            ?? throw new EntityNotFoundException(typeof(T).Name, id);

        public virtual async Task<List<T>> ObterTodosAsync() =>
            await DbSet.ToListAsync();

        public virtual async Task<List<T>> ObterTodosAtivosAsync()
        {
            var property = typeof(T).GetProperty("Ativo");
            if (property != null)
            {
                return await DbSet.Where(e => EF.Property<bool>(e, "Ativo")).ToListAsync();
            }
            return await ObterTodosAsync();
        }

        public virtual async Task<List<T>> ObterPorIdsAsync(IEnumerable<int> ids)
        {
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                return await DbSet.Where(e => ids.Contains(EF.Property<int>(e, "Id"))).ToListAsync();
            }
            return [];
        }

        // Métodos de manipulação (sem SaveChanges)
        public virtual async Task AdicionarAsync(T entity) =>
            await DbSet.AddAsync(entity);

        public virtual async Task AdicionarRangeAsync(IEnumerable<T> entities) =>
            await DbSet.AddRangeAsync(entities);

        public virtual void Atualizar(T entity) =>
            DbSet.Update(entity);

        public virtual void AtualizarRange(IEnumerable<T> entities) =>
            DbSet.UpdateRange(entities);

        public virtual void Remover(T entity) =>
            DbSet.Remove(entity);

        public virtual void RemoverRange(IEnumerable<T> entities) =>
            DbSet.RemoveRange(entities);

        public virtual async Task<bool> RemoverPorIdAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                return false;

            DbSet.Remove(entity);
            return true;
        }

        public virtual async Task RemoverPorIdsAsync(List<int> ids)
        {
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                List<T> entities = await DbSet.Where(e => ids.Contains(EF.Property<int>(e, "Id"))).ToListAsync();
                if (entities.Count > 0)
                {
                    DbSet.RemoveRange(entities);
                }
            }
        }

        public virtual void RemoverLogico(T entity)
        {
            var property = typeof(T).GetProperty("Ativo");
            if (property != null)
            {
                property.SetValue(entity, false);
                Atualizar(entity);
            }
            else
            {
                Remover(entity);
            }
        }

        // Métodos de validação
        public virtual async Task<bool> ExisteAsync(int id) =>
            await DbSet.FindAsync(id) != null;

        public virtual async Task<bool> ExisteAtivoAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null) return false;

            var property = typeof(T).GetProperty("Ativo");
            if (property != null)
            {
                return (bool)(property.GetValue(entity) ?? false);
            }
            return true;
        }

        // Métodos de contagem
        public virtual async Task<int> ContarAsync() =>
            await DbSet.CountAsync();

        public virtual async Task<int> ContarAtivosAsync()
        {
            var property = typeof(T).GetProperty("Ativo");
            if (property != null)
            {
                return await DbSet.Where(e => EF.Property<bool>(e, "Ativo")).CountAsync();
            }
            return await ContarAsync();
        }
    }
}
