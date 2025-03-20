using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
    }
}
