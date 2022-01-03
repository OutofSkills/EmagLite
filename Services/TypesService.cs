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
    public class TypesService : ITypesService
    {
        private readonly IUnitOfWork unitOfWork;

        public TypesService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddType(ProductType type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            unitOfWork.TypesRepository.Insert(type);
            unitOfWork.SaveChanges();
        }

        public void EditType(ProductType type)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            unitOfWork.TypesRepository.Update(type);
            unitOfWork.SaveChanges();
        }

        public ProductType FindTypeAsync(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var type = unitOfWork.TypesRepository.Find(name);

            return type;
        }

        public async Task<IEnumerable<ProductType>> GetTypesAsync()
        {
            var types = await unitOfWork.TypesRepository.GetAll();

            return types;
        }

        public async Task RemoveTypeAsync(int typeId)
        {
            if (typeId is 0)
                throw new ArgumentNullException(nameof(typeId));

            await unitOfWork.TypesRepository.Delete(typeId);
            unitOfWork.SaveChanges();
        }
    }
}
