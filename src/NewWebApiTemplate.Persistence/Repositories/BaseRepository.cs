using NewWebApiTemplate.Persistence.Contexts;

namespace NewWebApiTemplate.Persistence.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext context;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
