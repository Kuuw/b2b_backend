using DAL.Abstract;

namespace DAL.Concrete
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
    }
}
