using ITI.KDO.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.KDO.WebApp.Services
{
    public class QuantityServices
    {
        readonly ItemQuantityGateway _quantityGateway;

        public QuantityServices(ItemQuantityGateway quantityGateway)
        {
            _quantityGateway = quantityGateway;
        }

        public Result<ItemQuantity> GetById(int quantityId)
        {
            ItemQuantity itemquantity;
            if ((itemquantity = _quantityGateway.FindById(quantityId)) == null) return Result.Failure<ItemQuantity>(Status.NotFound, "ItemQuantity not found.");
            return Result.Success(Status.Ok, itemquantity);
        }

        public Result<IEnumerable<ItemQuantity>> GetByEventId(int eventId)
        {
            IEnumerable<ItemQuantity> itemquantity;
            if ((itemquantity = _quantityGateway.FindByEventId(eventId)) == null) return Result.Failure<IEnumerable<ItemQuantity>>(Status.NotFound, "ItemQuantity not found.");
            return Result.Success(Status.Ok, itemquantity);
        }

        public Result<int> Delete(int quantityId)
        {
            if (_quantityGateway.FindById(quantityId) == null) return Result.Failure<int>(Status.NotFound, "Quantity not found.");
            _quantityGateway.Delete(quantityId);
            return Result.Success(Status.Ok, quantityId);
        }

        public Result<ItemQuantity> UpdateQuantity(int quantityId, int quantity, int recipientId, int nominatorId, int eventId, int presentId)
        {
            ItemQuantity itemQuantity;
            if((itemQuantity = _quantityGateway.FindById(quantityId)) == null)
            {
                return Result.Failure<ItemQuantity>(Status.NotFound, "Quantity not found.");
            }

            _quantityGateway.Update(quantityId, quantity, recipientId, nominatorId, eventId, presentId);
            itemQuantity = _quantityGateway.FindById(quantityId);
            return Result.Success(Status.Ok, itemQuantity);
        }

        public Result<ItemQuantity> CreateQuantity(int quantityId, int quantity, int recipientId, int nominatorId, int eventId, int presentId)
        {
            _quantityGateway.Create(quantity, recipientId, nominatorId, eventId, presentId);
            ItemQuantity itemQuantity = _quantityGateway.FindById(quantityId);
            return Result.Success(Status.Ok, itemQuantity);
        }

        public Result<IEnumerable<ItemPresentQuantity>> GetUserPresentEvent(int userId, int eventId)
        {
            IEnumerable<ItemPresentQuantity> itempresentquantity;
            if ((itempresentquantity = _quantityGateway.GetUserPresentEvent(userId, eventId)) == null) return Result.Failure<IEnumerable<ItemPresentQuantity>>(Status.NotFound, "ItemPresentQuantity not found.");
            return Result.Success(Status.Ok, itempresentquantity);
        }
        
        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);

        bool IsPriceValid(float price)
        {
            if (price <= 0) return false;
            return true;
        }
    }
}
