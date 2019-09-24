using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArayeTestProject.Api.Application.Models.Domain;
using ArayeTestProject.Api.Application.Models.Sales;
using ArayeTestProject.Api.Presistences.Context;
using ArayeTestProject.Api.Presistences.Exceptions;
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
            await context.SaveChangesAsync ();

            await context.BulkInsertAsync (cities);

            return await context.Cities.ToListAsync ();
        }

        public async Task AddSales (List<Sale> sales) {
            await context.BulkInsertAsync (sales);
        }

        public async Task<List<string>> GetNames (string searchKey) {
            return await context.Sales.Where (s => string.IsNullOrEmpty (searchKey) ? true : s.UserName.StartsWith (searchKey)).Select (s => s.UserName).GroupBy (s => s).Select (s => s.FirstOrDefault ()).ToListAsync ();
        }
        public async Task<List<Sale>> GetProductNames (string searchKey) {
            return await context.Sales.Where (s => string.IsNullOrEmpty (searchKey) ? true : (s.ProductName.StartsWith (searchKey) || s.ProductId.StartsWith (searchKey))).GroupBy (s => s.ProductName).Select (s => s.FirstOrDefault ()).ToListAsync ();
        }
        public async Task<List<City>> GetCityNames (string searchKey) {
            return await context.Cities.Where (s => string.IsNullOrEmpty (searchKey) ? true : s.Name.StartsWith (searchKey)).ToListAsync ();
        }
        public async Task<List<Sale>> GetSaleList (SaleListFilterModel filter) {
            return await context.Sales
                .Where (s => (filter.Id > 0 ? s.Id == filter.Id : true) &&
                    (string.IsNullOrEmpty (filter.ProductName) ? true : s.ProductName.Contains (filter.ProductName)) &&
                    (string.IsNullOrEmpty (filter.UserName) ? true : s.UserName.Contains (filter.UserName)) &&
                    (string.IsNullOrEmpty (filter.ProductId) ? true : s.ProductId.Contains (filter.ProductId)) &&
                    (filter.MaxPrice > 0 ? s.Price <= filter.MaxPrice : true) && s.Price > filter.MinPrice)
                .Include (s => s.City).Where (s => string.IsNullOrEmpty (filter.CityName) ? true : s.City.Name.Contains (filter.CityName))
                .OrderByDescending (s => s.Id).Skip (filter.Page * filter.Count).Take (filter.Count).ToListAsync ();
        }
        public async Task UpdateSale (Sale newsale) {
            var sale = await context.Sales.FirstOrDefaultAsync (s => s.Id == newsale.Id);
            if (sale == null)
                throw new SaleNotFoundException ();
            if (sale.Price + (0.15 * sale.Price) < newsale.Price)
                throw new MaximumAmountThresholdException ();
            sale.CityId = newsale.CityId;
            sale.ProductId = newsale.ProductId;
            sale.Price = newsale.Price;
            sale.ProductName = newsale.ProductName;
            sale.UserName = newsale.UserName;
            await context.SaveChangesAsync ();
        }

        public async Task<bool> IsUsertNameExist (string userName) {
            return await context.Sales.AnyAsync (s => s.UserName == userName);
        }

        public async Task<bool> IsProductExist (string productId, string ProductName) {
            return await context.Sales.AnyAsync (s => s.ProductId == productId && s.ProductName == ProductName);
        }
        public async Task<long> IsCityNameExist (string cityName) {
            var city = await context.Cities.FirstOrDefaultAsync (s => s.Name == cityName);
            if (city == null)
                throw new CityNameNotFoundException ();
            return city.Id;
        }
        public async Task<long> GetLastSalePrice (SaleListFilterModel filter) {
            var sale = await context.Sales.Where (s => s.CityId == filter.MaxPrice && s.ProductId == filter.ProductId && s.UserName == filter.UserName).OrderByDescending (s => s.Id).FirstOrDefaultAsync ();
            return sale == null ? 0 : sale.Price;
        }
        public async Task AddSale (Sale sale) {
            context.Sales.Add (sale);
            await context.SaveChangesAsync ();
        }
        public async Task DeleteSale (long SaleId) {
            var sale = await context.Sales.FirstOrDefaultAsync (s => s.Id == SaleId);
            if (sale == null)
                throw new SaleNotFoundException ();
            context.Sales.Remove (sale);
            await context.SaveChangesAsync ();
        }
    }
}