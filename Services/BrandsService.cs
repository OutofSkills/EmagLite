using DataAccess.UnitOfWork;
using Models;
using RESTApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class BrandsService : IBrandsService
    {
        private readonly IUnitOfWork unitOfWork;

        public BrandsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddBrand(ProductBrand brand)
        {
            if (brand is null)
                throw new ArgumentNullException(nameof(brand));

            unitOfWork.BrandsRepository.Insert(brand);
            unitOfWork.SaveChanges();
        }

        public void EditBrand(ProductBrand brand)
        {
            if (brand is null)
                throw new ArgumentNullException(nameof(brand));

            unitOfWork.BrandsRepository.Update(brand);
            unitOfWork.SaveChanges();
        }

        public ProductBrand FindBrandAsync(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var brand = unitOfWork.BrandsRepository.Find(name);

            return brand;
        }

        public async Task<IEnumerable<ProductBrand>> GetBrandsAsync()
        {
            var brands = await unitOfWork.BrandsRepository.GetAll();

            return brands;
        }

        public async Task RemoveBrandAsync(int brandId)
        {
            if (brandId is 0)
                throw new ArgumentNullException(nameof(brandId));

            await unitOfWork.BrandsRepository.Delete(brandId);
            unitOfWork.SaveChanges();
        }
    }
}
