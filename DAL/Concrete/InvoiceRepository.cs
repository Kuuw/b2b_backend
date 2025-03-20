using DAL.Abstract;

namespace DAL.Concrete
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
    }
}
