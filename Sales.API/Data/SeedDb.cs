namespace Sales.API.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {

            _context = context;

        }

        public async Task SeedAsync()
        {

            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();

        }

        private async Task CheckCountriesAsync()
        {

            if (!_context.Countries.Any())
            {

                _context.Countries.Add(new Shared.Entities.Country { Name = "México"});
                _context.Countries.Add(new Shared.Entities.Country { Name = "Rusia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "China" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Irán" });

                await _context.SaveChangesAsync();

            }

        }

    }
}
