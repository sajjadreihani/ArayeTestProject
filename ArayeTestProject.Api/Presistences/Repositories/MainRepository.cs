using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.Context;
using ArayeTestProject.Api.Presistences.IRepositories;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace ArayeTestProject.Api.Presistences.Repositories {
    public class MainRepository : IMainRepository {
        private readonly AppDbContext context;

        public MainRepository (AppDbContext context) {
            this.context = context;
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;

        }

        public async Task<List<City>> AddCities (List<City> cities) {
            context.Database.SetCommandTimeout (1800);
            context.Database.ExecuteSqlCommand ("TRUNCATE TABLE Sales");
            context.Cities.RemoveRange (context.Cities);
            await context.BulkInsertAsync (cities);
            //await context.SaveChangesAsync ();

            return cities;
        }

        public async Task AddSales (List<Sale> sales) {
            await context.BulkInsertAsync (sales);
        }

        public async Task<List<string>> GetNames (string searchKey) {
            return await context.Sales.Where (s => string.IsNullOrEmpty (searchKey) ? true : s.UserName.StartsWith (searchKey)).Select (s => s.UserName).GroupBy (s => s).Select (s => s.FirstOrDefault ()).ToListAsync ();
        }
        public async Task<List<string>> GetProductNames (string searchKey) {
            return await context.Sales.Where (s => string.IsNullOrEmpty (searchKey) ? true : (s.ProductName.StartsWith (searchKey)||s.ProductId.StartsWith(searchKey))).Select (s => s.ProductId + " - " + s.ProductName).GroupBy (s => s).Select (s => s.FirstOrDefault ()).ToListAsync ();
        }
        public async Task<List<string>> GetCityNames (string searchKey) {
            return await context.Cities.Where (s => string.IsNullOrEmpty (searchKey) ? true : s.Name.StartsWith (searchKey)).Select (s => s.Name).GroupBy (s => s).Select (s => s.FirstOrDefault ()).ToListAsync ();
        }
        public async Task<List<Sale>> GetSaleList(SaleListFilterModel filter)
        {
            return await context.Sales.Where(s=>(filter.Id>0?s.Id==filter.Id:true)).ToListAsync();
        }
    }
}