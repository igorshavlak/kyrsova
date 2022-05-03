using gergergergerg.Data;
using gergergergerg.Models;
using gergergergerg.Repository.IRepository;

namespace gergergergerg.Repository
{
    public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
    {
        public readonly ApplicationDbContext _db;
        public InquiryHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(InquiryHeader obj)
        {
            _db.InquiryHeaders.Update(obj);
        }
    }
}
