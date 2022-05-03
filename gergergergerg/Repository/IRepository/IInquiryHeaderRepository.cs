using gergergergerg.Models;
using static gergergergerg.Repository.IRepository.IRepository;

namespace gergergergerg.Repository.IRepository
{
    public interface IInquiryHeaderRepository : IRepository<InquiryHeader>
    {
        void Update(InquiryHeader obj);
    }
}
