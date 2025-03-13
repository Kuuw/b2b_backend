using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
    }
}
