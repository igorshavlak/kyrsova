using gergergergerg.Data;
using gergergergerg.Models;
using gergergergerg.Repository.IRepository;

namespace gergergergerg.Repository
{
    public class InquiryDetailsRepository : Repository<InquiryDetails>, IInquiryDetailsRepository
    {
        public readonly ApplicationDbContext _db;
        public InquiryDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(InquiryDetails obj)
        {
            _db.InquiryDetail.Update(obj);
        }

       
    }


}
