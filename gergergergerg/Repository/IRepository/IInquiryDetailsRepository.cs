using gergergergerg.Models;
using static gergergergerg.Repository.IRepository.IRepository;

namespace gergergergerg.Repository.IRepository
{
    public interface IInquiryDetailsRepository : IRepository<InquiryDetails>
    {
        void Update(InquiryDetails obj);
    }
}