using System.Collections.Generic;

namespace gergergergerg.Models.ViewModels
{
    public class InquiryVM
    {

        public InquiryHeader InquiryHeader { get; set; }
        public IEnumerable <InquiryDetails> InquiryDetail { get; set; }
    }
}
