using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
    }
}
