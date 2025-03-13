using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
    }
}
