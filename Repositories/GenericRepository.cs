using DAW.Data;
using DAW.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Repositories
{
    public class GenericRepository<TEntity>:IGenericRepository<Product> where TEntity : Product
    {
        protected readonly DAWContext _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(DAWContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }
    }
}
